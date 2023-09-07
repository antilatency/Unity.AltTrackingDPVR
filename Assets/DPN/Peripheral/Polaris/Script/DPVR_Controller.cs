/************************************************************************************

Copyright: Copyright(c) 2015-2017 Deepoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/
using UnityEngine;
using System.Collections;

namespace dpn
{
    public class DPVR_Controller : MonoBehaviour
    {
        private DPVR_TrackedController trackedObject;

        public bool BPressed = false;
        public bool APressed = false;
        public bool GripPressed = false;
        public bool HomePressed = false;
        public bool TrigerPressed = false;
        public bool AxisPressed = false;

        public bool GetPress(ulong buttonMask) { return (trackedObject.DpnpGetDeviceCurrentStatus() != null)? (((uint)trackedObject.DpnpGetDeviceCurrentStatus().button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0] & buttonMask) != 0) : false; }
        public bool GetPressDown(ulong buttonMask) { return (trackedObject.DpnpGetDeviceCurrentStatus() != null) ? ((uint)trackedObject.DpnpGetDeviceCurrentStatus().button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0] & buttonMask) != 0 && ((uint)trackedObject.DpnpGetDevicePrevStatus().button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0] & buttonMask) == 0 : false; }
        public bool GetPressUp(ulong buttonMask) { return (trackedObject.DpnpGetDeviceCurrentStatus() != null) ? ((uint)trackedObject.DpnpGetDeviceCurrentStatus().button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0] & buttonMask) == 0 && ((uint)trackedObject.DpnpGetDevicePrevStatus().button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0] & buttonMask) != 0 : false; }

        public bool GetPress(DPNP_POLARIS_BUTTONMASK buttonId) { return (trackedObject.DpnpGetDeviceCurrentStatus() != null) ? GetPress(1ul << (int)buttonId) : false; }
        public bool GetPressDown(DPNP_POLARIS_BUTTONMASK buttonId) { return (trackedObject.DpnpGetDeviceCurrentStatus() != null) ? GetPressDown(1ul << (int)buttonId) : false; }
        public bool GetPressUp(DPNP_POLARIS_BUTTONMASK buttonId) { return (trackedObject.DpnpGetDeviceCurrentStatus() != null) ? GetPressUp(1ul << (int)buttonId) : false; }

        public Vector2 GetAxis() { return (trackedObject.DpnpGetDeviceCurrentStatus() != null) ? new Vector2(trackedObject.DpnpGetDeviceCurrentStatus().axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_X][0] / 1000, trackedObject.DpnpGetDeviceCurrentStatus().axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_Y][0] / 1000) : new Vector2(0, 0); }

        public event ClickedEventHandler BButtonClicked;
        public event ClickedEventHandler BButtonUnclicked;
        public event ClickedEventHandler AButtonClicked;
        public event ClickedEventHandler AButtonUnclicked;
        public event ClickedEventHandler GrippedClicked;
        public event ClickedEventHandler UngrippedUnclicked;
        public event ClickedEventHandler HomeButtonClicked;
        public event ClickedEventHandler HomeButtonUnclicked;
        public event ClickedEventHandler TriggerClicked;
        public event ClickedEventHandler TriggerUnclicked;
        public event ClickedEventHandler AxisClicked;      // x > 150 or y > 150
        public event ClickedEventHandler AxisUnclicked;



        public virtual void OnBButtonClicked(DPVRClickedEventArgs e)
        {
            if (BButtonClicked != null)
                BButtonClicked(this, e);
        }

        public virtual void OnBButtonUnclicked(DPVRClickedEventArgs e)
        {
            if (BButtonUnclicked != null)
                BButtonUnclicked(this, e);
        }

        public virtual void OnAButtonClicked(DPVRClickedEventArgs e)
        {
            if (AButtonClicked != null)
                AButtonClicked(this, e);
        }

        public virtual void OnAButtonUnclicked(DPVRClickedEventArgs e)
        {
            if (AButtonUnclicked != null)
                AButtonUnclicked(this, e);
        }

        public virtual void OnGripped(DPVRClickedEventArgs e)
        {
            if (GrippedClicked != null)
                GrippedClicked(this, e);
        }

        public virtual void OnUngripped(DPVRClickedEventArgs e)
        {
            if (UngrippedUnclicked != null)
                UngrippedUnclicked(this, e);
        }

        public virtual void OnHomeButtonClicked(DPVRClickedEventArgs e)
        {
            if (HomeButtonClicked != null)
                HomeButtonClicked(this, e);
        }

        public virtual void OnHomeButtonUnclicked(DPVRClickedEventArgs e)
        {
            if (HomeButtonUnclicked != null)
                HomeButtonUnclicked(this, e);
        }

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

        public virtual void OnAxisClicked(DPVRClickedEventArgs e)
        {
            if (AxisClicked != null)
                AxisClicked(this, e);
        }

        public virtual void OnAxisUnclicked(DPVRClickedEventArgs e)
        {
            if (AxisUnclicked != null)
                AxisUnclicked(this, e);
        }

        protected virtual void Start()
        {
            if (this.GetComponent<DPVR_TrackedController>() == null)
            {
                trackedObject = gameObject.AddComponent<DPVR_TrackedController>();
            }
            else
            {
                trackedObject = this.GetComponent<DPVR_TrackedController>();
            }
        }

        // Update is called once per frame
        public void Update()
        {
            Peripheralstatus status = trackedObject.DpnpGetDeviceCurrentStatus();
            if (trackedObject.DpnpGetDeviceCurrentStatus() == null)
            {
                return;
            }
            bool bbutton = ((status.button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0] & (int)DPNP_POLARIS_BUTTONMASK.DPNP_POLARIS_BUTTONMASK_B) != 0);
            if (bbutton && !BPressed)
            {
                BPressed = true;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedObject.device_type;
                e.flags = (uint)status.button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0];
                e.padX = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_X][0] / 1000;
                e.padY = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_Y][0] / 1000;
                OnBButtonClicked(e);

            }
            else if (!bbutton && BPressed)
            {
                BPressed = false;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedObject.device_type;
                e.flags = (uint)status.button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0];
                e.padX = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_X][0] / 1000;
                e.padY = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_Y][0] / 1000;
                OnBButtonUnclicked(e);
            }

            bool abutton = ((status.button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0] & (int)DPNP_POLARIS_BUTTONMASK.DPNP_POLARIS_BUTTONMASK_A) != 0);
            if (abutton && !APressed)
            {
                APressed = true;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedObject.device_type;
                e.flags = (uint)status.button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0];
                e.padX = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_X][0] / 1000;
                e.padY = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_Y][0] / 1000;
                OnAButtonClicked(e);

            }
            else if (!abutton && APressed)
            {
                APressed = false;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedObject.device_type;
                e.flags = (uint)status.button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0];
                e.padX = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_X][0] / 1000;
                e.padY = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_Y][0] / 1000;
                OnAButtonUnclicked(e);
            }

            bool trigger = ((status.button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0] & (int)DPNP_POLARIS_BUTTONMASK.DPNP_POLARIS_BUTTONMASK_TRIGER) != 0);
            if (trigger && !TrigerPressed)
            {
                TrigerPressed = true;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedObject.device_type;
                e.flags = (uint)status.button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0];
                e.padX = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_X][0] / 1000;
                e.padY = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_Y][0] / 1000;
                OnTriggerClicked(e);

            }
            else if (!trigger && TrigerPressed)
            {
                TrigerPressed = false;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedObject.device_type;
                e.flags = (uint)status.button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0];
                e.padX = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_X][0] / 1000;
                e.padY = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_Y][0] / 1000;
                OnTriggerUnclicked(e);
            }

            bool grip = ((status.button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0] & (int)DPNP_POLARIS_BUTTONMASK.DPNP_POLARIS_BUTTONMASK_GRIP) != 0);
            if (grip && !GripPressed)
            {
                GripPressed = true;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedObject.device_type;
                e.flags = (uint)status.button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0];
                e.padX = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_X][0] / 1000;
                e.padY = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_Y][0] / 1000;
                OnGripped(e);

            }
            else if (!grip && GripPressed)
            {
                GripPressed = false;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedObject.device_type;
                e.flags = (uint)status.button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0];
                e.padX = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_X][0] / 1000;
                e.padY = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_Y][0] / 1000;
                OnUngripped(e);
            }

            bool home = ((status.button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0] & (int)DPNP_POLARIS_BUTTONMASK.DPNP_POLARIS_BUTTONMASK_HOME) != 0);
            if (home && !HomePressed)
            {
                HomePressed = true;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedObject.device_type;
                e.flags = (uint)status.button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0];
                e.padX = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_X][0] / 1000;
                e.padY = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_Y][0] / 1000;
                OnHomeButtonClicked(e);
            }
            else if (!home && HomePressed)
            {
                HomePressed = false;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedObject.device_type;
                e.flags = (uint)status.button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0];
                e.padX = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_X][0] / 1000;
                e.padY = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_Y][0] / 1000;
                OnHomeButtonUnclicked(e);
            }

            bool axis = (Mathf.Abs(status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_X][0]) > 150) || (Mathf.Abs(status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_Y][0]) > 150);
            if (axis && !AxisPressed)
            {
                AxisPressed = true;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedObject.device_type;
                e.flags = (uint)status.button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0];
                e.padX = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_X][0] / 1000;
                e.padY = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_Y][0] / 1000;
                OnAxisClicked(e);
            }
            else if (!axis && AxisPressed)
            {
                AxisPressed = false;
                DPVRClickedEventArgs e;
                e.DeviceType = trackedObject.device_type;
                e.flags = (uint)status.button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0];
                e.padX = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_X][0] / 1000;
                e.padY = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_Y][0] / 1000;
                OnAxisUnclicked(e);
            }
        }

    }
}