/************************************************************************************

Copyright: Copyright(c) 2015-2017 Deepoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

namespace dpn
{
    public class ButtonMask
    {
        public const ulong System = (1ul << (int)EVRButtonId.k_EButton_System); // reserved
        public const ulong ApplicationMenu = (1ul << (int)EVRButtonId.k_EButton_ApplicationMenu);
        public const ulong Grip = (1ul << (int)EVRButtonId.k_EButton_Grip);
        public const ulong Axis0 = (1ul << (int)EVRButtonId.k_EButton_Axis0);
        public const ulong Axis1 = (1ul << (int)EVRButtonId.k_EButton_Axis1);
        public const ulong Axis2 = (1ul << (int)EVRButtonId.k_EButton_Axis2);
        public const ulong Axis3 = (1ul << (int)EVRButtonId.k_EButton_Axis3);
        public const ulong Axis4 = (1ul << (int)EVRButtonId.k_EButton_Axis4);
        public const ulong Touchpad = (1ul << (int)EVRButtonId.k_EButton_SteamVR_Touchpad);
        public const ulong Trigger = (1ul << (int)EVRButtonId.k_EButton_SteamVR_Trigger);
    }

    public enum EVRButtonId
    {
        k_EButton_System = 0,
        k_EButton_ApplicationMenu = 1,
        k_EButton_Grip = 2,
        k_EButton_DPad_Left = 3,
        k_EButton_DPad_Up = 4,
        k_EButton_DPad_Right = 5,
        k_EButton_DPad_Down = 6,
        k_EButton_A = 7,
        k_EButton_ProximitySensor = 31,
        k_EButton_Axis0 = 32,
        k_EButton_Axis1 = 33,
        k_EButton_Axis2 = 34,
        k_EButton_Axis3 = 35,
        k_EButton_Axis4 = 36,
        k_EButton_SteamVR_Touchpad = 32,
        k_EButton_SteamVR_Trigger = 33,
        k_EButton_Dashboard_Back = 2,
        k_EButton_Max = 64,
    }

    public class DPVR_STEAM_Controller : MonoBehaviour
    {
        private DPVR_TrackedController trackedController;

        public uint index { get { return (uint)trackedController.device_type; } }

        public bool connected { get { return trackedController.DpnpGetSteamControllerPose().bDeviceIsConnected; } }
        public bool hasTracking { get { return trackedController.DpnpGetSteamControllerPose().bPoseIsValid; } }

        // These values are only accurate for the last controller state change (e.g. trigger release), and by definition, will always lag behind
        // the predicted visual poses that drive SteamVR_TrackedObjects since they are sync'd to the input timestamp that caused them to update.
        public Vector3 velocity { get { return new Vector3(trackedController.DpnpGetSteamControllerPose().vVelocity.x, trackedController.DpnpGetSteamControllerPose().vVelocity.y, trackedController.DpnpGetSteamControllerPose().vVelocity.z); } }
        public Vector3 angularVelocity { get { return new Vector3(-trackedController.DpnpGetSteamControllerPose().vAngularVelocity.x, -trackedController.DpnpGetSteamControllerPose().vAngularVelocity.y, trackedController.DpnpGetSteamControllerPose().vAngularVelocity.z); } }

        public VRControllerState_t GetState() { return trackedController.DpnpGetSteamControllerCurrentStatus(); }
        public VRControllerState_t GetPrevState() { return trackedController.DpnpGetSteamControllerPrevStatus(); }
        public TrackedDevicePose_t GetPose() { return trackedController.DpnpGetSteamControllerPose(); }

        public bool triggerPressed = false;
        public bool steamPressed = false;
        public bool menuPressed = false;
        public bool padPressed = false;
        public bool padTouched = false;
        public bool gripped = false;

        public bool GetPress(ulong buttonMask) { return (trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonPressed & buttonMask) != 0; }
        public bool GetPressDown(ulong buttonMask) { return (trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonPressed & buttonMask) != 0 && (trackedController.DpnpGetSteamControllerPrevStatus().ulButtonPressed & buttonMask) == 0; }
        public bool GetPressUp(ulong buttonMask) { return (trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonPressed & buttonMask) == 0 && (trackedController.DpnpGetSteamControllerPrevStatus().ulButtonPressed & buttonMask) != 0; }

        public bool GetPress(EVRButtonId buttonId) { return GetPress(1ul << (int)buttonId); }
        public bool GetPressDown(EVRButtonId buttonId) { return GetPressDown(1ul << (int)buttonId); }
        public bool GetPressUp(EVRButtonId buttonId) { return GetPressUp(1ul << (int)buttonId); }

        public bool GetTouch(ulong buttonMask) { return (trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonTouched & buttonMask) != 0; }
        public bool GetTouchDown(ulong buttonMask) { return (trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonTouched & buttonMask) != 0 && (trackedController.DpnpGetSteamControllerPrevStatus().ulButtonTouched & buttonMask) == 0; }
        public bool GetTouchUp(ulong buttonMask) { return (trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonTouched & buttonMask) == 0 && (trackedController.DpnpGetSteamControllerPrevStatus().ulButtonTouched & buttonMask) != 0; }

        public bool GetTouch(EVRButtonId buttonId) { return GetTouch(1ul << (int)buttonId); }
        public bool GetTouchDown(EVRButtonId buttonId) { return GetTouchDown(1ul << (int)buttonId); }
        public bool GetTouchUp(EVRButtonId buttonId) { return GetTouchUp(1ul << (int)buttonId); }

        public Vector2 GetAxis(EVRButtonId buttonId = EVRButtonId.k_EButton_SteamVR_Touchpad)
        {
            var axisId = (uint)buttonId - (uint)EVRButtonId.k_EButton_Axis0;
            switch (axisId)
            {
                case 0: return new Vector2(trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.x, trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.y);
                case 1: return new Vector2(trackedController.DpnpGetSteamControllerCurrentStatus().rAxis1.x, trackedController.DpnpGetSteamControllerCurrentStatus().rAxis1.y);
                case 2: return new Vector2(trackedController.DpnpGetSteamControllerCurrentStatus().rAxis2.x, trackedController.DpnpGetSteamControllerCurrentStatus().rAxis2.y);
                case 3: return new Vector2(trackedController.DpnpGetSteamControllerCurrentStatus().rAxis3.x, trackedController.DpnpGetSteamControllerCurrentStatus().rAxis3.y);
                case 4: return new Vector2(trackedController.DpnpGetSteamControllerCurrentStatus().rAxis4.x, trackedController.DpnpGetSteamControllerCurrentStatus().rAxis4.y);
            }
            return Vector2.zero;
        }

        public void TriggerHapticPulse(ushort durationMicroSec = 500, EVRButtonId buttonId = EVRButtonId.k_EButton_SteamVR_Touchpad)
        {
            trackedController.peripheral.DpnupWriteDeviceFeedback(DPNP_VALUE_TYPE.DPNP_VALUE_TYPE_FEEDBACK_MOTOR - DPNP_VALUE_TYPE.DPNP_VALUE_TYPE_FEEDBACK, durationMicroSec / 1000.0f);
        }

        public event ClickedEventHandler MenuButtonClicked;
        public event ClickedEventHandler MenuButtonUnclicked;
        public event ClickedEventHandler TriggerClicked;
        public event ClickedEventHandler TriggerUnclicked;
        public event ClickedEventHandler SteamClicked;
        public event ClickedEventHandler PadClicked;
        public event ClickedEventHandler PadUnclicked;
        public event ClickedEventHandler PadTouched;
        public event ClickedEventHandler PadUntouched;
        public event ClickedEventHandler Gripped;
        public event ClickedEventHandler Ungripped;

        public virtual void OnTriggerClicked(DPVRClickedEventArgs e)
        {
            if (TriggerClicked != null)
                TriggerClicked(this, e);
        }

        public virtual void OnTriggerUnclicked(DPVRClickedEventArgs e)
        {
            if (TriggerUnclicked != null)
                TriggerUnclicked(this, e);
        }

        public virtual void OnMenuClicked(DPVRClickedEventArgs e)
        {
            if (MenuButtonClicked != null)
                MenuButtonClicked(this, e);
        }

        public virtual void OnMenuUnclicked(DPVRClickedEventArgs e)
        {
            if (MenuButtonUnclicked != null)
                MenuButtonUnclicked(this, e);
        }

        public virtual void OnSteamClicked(DPVRClickedEventArgs e)
        {
            if (SteamClicked != null)
                SteamClicked(this, e);
        }

        public virtual void OnPadClicked(DPVRClickedEventArgs e)
        {
            if (PadClicked != null)
                PadClicked(this, e);
        }

        public virtual void OnPadUnclicked(DPVRClickedEventArgs e)
        {
            if (PadUnclicked != null)
                PadUnclicked(this, e);
        }

        public virtual void OnPadTouched(DPVRClickedEventArgs e)
        {
            if (PadTouched != null)
                PadTouched(this, e);
        }

        public virtual void OnPadUntouched(DPVRClickedEventArgs e)
        {
            if (PadUntouched != null)
                PadUntouched(this, e);
        }

        public virtual void OnGripped(DPVRClickedEventArgs e)
        {
            if (Gripped != null)
                Gripped(this, e);
        }

        public virtual void OnUngripped(DPVRClickedEventArgs e)
        {
            if (Ungripped != null)
                Ungripped(this, e);
        }

        protected virtual void Start()
        {
            if (this.GetComponent<DPVR_TrackedController>() == null)
            {
                trackedController = gameObject.AddComponent<DPVR_TrackedController>();
            }
            else
            {
                trackedController = this.GetComponent<DPVR_TrackedController>();
            }
        }

        // Update is called once per frame
        public void Update()
        {
            ulong trigger = trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonPressed & (1UL << ((int)EVRButtonId.k_EButton_SteamVR_Trigger));
            if (trigger > 0L && !triggerPressed)
            {
                triggerPressed = true;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedController.device_type;
                e.flags = (uint)trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonPressed;
                e.padX = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.x;
                e.padY = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.y;
                OnTriggerClicked(e);

            }
            else if (trigger == 0L && triggerPressed)
            {
                triggerPressed = false;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedController.device_type;
                e.flags = (uint)trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonPressed;
                e.padX = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.x;
                e.padY = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.y;
                OnTriggerClicked(e);
            }

            ulong grip = trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonPressed & (1UL << ((int)EVRButtonId.k_EButton_Grip));
            if (grip > 0L && !gripped)
            {
                gripped = true;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedController.device_type;
                e.flags = (uint)trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonPressed;
                e.padX = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.x;
                e.padY = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.y;
                OnTriggerClicked(e);

            }
            else if (grip == 0L && gripped)
            {
                gripped = false;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedController.device_type;
                e.flags = (uint)trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonPressed;
                e.padX = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.x;
                e.padY = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.y;
                OnTriggerClicked(e);
            }

            ulong pad = trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonPressed & (1UL << ((int)EVRButtonId.k_EButton_SteamVR_Touchpad));
            if (pad > 0L && !padPressed)
            {
                padPressed = true;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedController.device_type;
                e.flags = (uint)trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonPressed;
                e.padX = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.x;
                e.padY = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.y;
                OnTriggerClicked(e);
            }
            else if (pad == 0L && padPressed)
            {
                padPressed = false;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedController.device_type;
                e.flags = (uint)trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonPressed;
                e.padX = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.x;
                e.padY = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.y;
                OnTriggerClicked(e);
            }

            ulong menu = trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonPressed & (1UL << ((int)EVRButtonId.k_EButton_ApplicationMenu));
            if (menu > 0L && !menuPressed)
            {
                menuPressed = true;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedController.device_type;
                e.flags = (uint)trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonPressed;
                e.padX = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.x;
                e.padY = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.y;
                OnTriggerClicked(e);
            }
            else if (menu == 0L && menuPressed)
            {
                menuPressed = false;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedController.device_type;
                e.flags = (uint)trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonPressed;
                e.padX = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.x;
                e.padY = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.y;
                OnTriggerClicked(e);
            }

            pad = trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonTouched & (1UL << ((int)EVRButtonId.k_EButton_SteamVR_Touchpad));
            if (pad > 0L && !padTouched)
            {
                padTouched = true;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedController.device_type;
                e.flags = (uint)trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonPressed;
                e.padX = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.x;
                e.padY = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.y;
                OnTriggerClicked(e);

            }
            else if (pad == 0L && padTouched)
            {
                padTouched = false;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedController.device_type;
                e.flags = (uint)trackedController.DpnpGetSteamControllerCurrentStatus().ulButtonPressed;
                e.padX = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.x;
                e.padY = trackedController.DpnpGetSteamControllerCurrentStatus().rAxis0.y;
                OnTriggerClicked(e);
            }
        }

    }
}
