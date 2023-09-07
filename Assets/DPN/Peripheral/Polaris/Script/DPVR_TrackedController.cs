/************************************************************************************

Copyright   :   Copyright 2015-2017 DeePoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace dpn
{
    public enum DPVRControllerType
    {
        DPVR_CONTROLLER_NONE = -1,
        DPVR_CONTROLLER_LEFT,
        DPVR_CONTROLLER_RIGHT,
        DPVR_CONTROLLER_MR
    }


    public struct DPVRClickedEventArgs
    {
        public DPVRControllerType DeviceType;
        public uint flags;
        public float padX, padY;
    }

    public delegate void ClickedEventHandler(object sender, DPVRClickedEventArgs e);

    public class DPVR_TrackedController : DPVRBaseSteamController
    {
        [Tooltip("If not set, relative to parent")]
        public Transform origin;

        public Transform Model;

        private DPVR_Controller _dpvrController = null;
        private DPVR_STEAM_Controller _dpvrSteamController = null;

        public void OnEnable()
        {
            if (DpnManager.peripheral != DPVRPeripheral.Polaris)
            {
                this.gameObject.SetActive(false);
                return;
            }
            DPNP_DEVICE_TYPE type = DPNP_DEVICE_TYPE.DPNP_DEVICE_TYPE_NONE;
            switch (device_type)
            {
                case DPVRControllerType.DPVR_CONTROLLER_LEFT:
                    type = DPNP_DEVICE_TYPE.DPNP_DEVICE_TYPE_LEFT_CONTROLLER;
                    break;
                case DPVRControllerType.DPVR_CONTROLLER_RIGHT:
                    type = DPNP_DEVICE_TYPE.DPNP_DEVICE_TYPE_RIGHT_CONTROLLER;
                    break;
                case DPVRControllerType.DPVR_CONTROLLER_MR:
                    type = DPNP_DEVICE_TYPE.DPNP_DEVICE_TYPE_MR_CONTROLLER;
                    break;
            }
            if (!OpenPeripheral(type, device_index))
            {
                Debug.Log("Open Peripheral type: " + type.ToString() + " index:" + device_index.ToString() + " fails.");
                return;
            }

            switch (DpnManager.controllerKeyMode)
            {
                case DPVRKeyMode.DPVR:
                {
                    _dpvrController = this.GetComponent<DPVR_Controller>();
                    if (_dpvrController == null)
                    {
                         _dpvrController = this.gameObject.AddComponent<DPVR_Controller>();
                    }
                    if (this.GetComponent<DPVR_STEAM_Controller>() != null)
                    {
                        Destroy(this.GetComponent<DPVR_STEAM_Controller>());
                    }
                    break;
                }
                case DPVRKeyMode.STEAM:
                    {
                        _dpvrSteamController = this.GetComponent<DPVR_STEAM_Controller>();
                        if (_dpvrSteamController == null)
                        {
                            _dpvrSteamController = this.gameObject.AddComponent<DPVR_STEAM_Controller>();
                        }
                        if (this.GetComponent<DPVR_Controller>() != null)
                        {
                            Destroy(this.GetComponent<DPVR_Controller>());
                        }
                        break;
                    }
            }

            if (peripheral != null)
            {
                int event_mask = (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BASE_A_TRACK 
                    | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BASE_A_UNTRACK 
                    | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BASE_B_TRACK 
                    | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BASE_B_UNTRACK;
                peripheral.DpnupRegisterEventNotificationFunction(null, event_mask, IntPtr.Zero);
            }

            EnableInternalObjects(false);
        }

        public void SetModelVisible(bool visible)
        {
            if (Model == null)
            {
                return;
            }
            Model.gameObject.SetActive(visible);
        }

        public override bool IsActivating()
        {
            bool ret = false;
            switch(DpnManager.controllerKeyMode)
            {
                case DPVRKeyMode.DPVR:
                    {
                        if (_dpvrController != null)
                        {
                            ret = _dpvrController.APressed 
                                | _dpvrController.BPressed
                                | _dpvrController.TrigerPressed
                                | _dpvrController.AxisPressed
                                | _dpvrController.HomePressed
                                | _dpvrController.GripPressed;
                        }
                        break;
                    }
                case DPVRKeyMode.STEAM:
                    {
                        if(_dpvrSteamController != null)
                        {
                            ret = _dpvrSteamController.triggerPressed
                                | _dpvrSteamController.steamPressed
                                | _dpvrSteamController.menuPressed
                                | _dpvrSteamController.padPressed
                                | _dpvrSteamController.padTouched
                                | _dpvrSteamController.gripped;
                        }
                    }
                    break;
            }
            return ret;
        }
        public override void DpnpUpdate()
        {
            base.DpnpUpdate();
            UpdatePoses();
        }

        void UpdatePoses()
        {
            if (device_type == DPVRControllerType.DPVR_CONTROLLER_NONE)
                return;


            int device_state = DpnpGetDeviceCurrentStatus().device_status;

            if ((device_state & ((int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_BASE_A_TRACK | (int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_BASE_B_TRACK)) == 0)
            {
                if (_isValid)
                {
                    _isValid = false;
                    OnDisconnected();
                }
                return;
            }
            else
            {
                if (!_isValid)
                    OnConnected();

                _isValid = true;
            }

            float[] pose = DpnpGetDeviceCurrentStatus().pose_state[0];
            float[] postion = DpnpGetDeviceCurrentStatus().position_state[0];

            Vector3 pos = new Vector3(postion[0], postion[1], -postion[2]) * DpnManager.worldScale;
            Quaternion rot = new Quaternion(pose[1], pose[2], -pose[3], -pose[0]);

            if (origin != null)
            {
                transform.position = origin.transform.TransformPoint(pos);
                transform.rotation = origin.rotation * rot;
            }
            else
            {
                transform.localPosition = pos;
                transform.localRotation = rot;
            }
        }

        public void SetDeviceIndex(DPVRControllerType device_type, int device_index = 0)
        {
            this.device_type = device_type;
            this.device_index = device_index;
        }

        public override void EnableInternalObjects(bool enabled)
        {
            EnableModel(enabled);
        }

        override public void EnableModel(bool enabled)
        {
            if (Model && Model.gameObject)
                Model.gameObject.SetActive(enabled);
        }

        void OnDisconnected()
        {
            SendMessageUpwards("OnControllerDisconnected", this);
        }

        void OnConnected()
        {
            SendMessageUpwards("OnControllerConnected", this);
        }

        void CheckConnectedState()
        {
            int device_state = DpnpGetDeviceCurrentStatus().device_status;

            if ((device_state & ((int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_BASE_A_TRACK | (int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_BASE_B_TRACK)) == 0)
            {
                OnDisconnected();
            }
        }

        private void Start()
        {
            if (peripheral == null)
            {
                OnDisconnected();
                this.gameObject.SetActive(false);
                return;
            }
            CheckConnectedState();
        }
        public override void DpnpResume()
        {
            base.DpnpResume();
            CheckConnectedState();
        }

    }
}