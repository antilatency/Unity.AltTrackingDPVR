using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace dpn
{
    public class DPVRBaseSteamController : DpnBasePeripheral
    {
        private static Dictionary<int, DPVR_Steam_Controller_Peripheral> SteamController = new Dictionary<int,DPVR_Steam_Controller_Peripheral>();
        private DPVR_Steam_Controller_Peripheral SteamControllerPeripheral = null;

        public DPVRControllerType device_type = DPVRControllerType.DPVR_CONTROLLER_NONE;
        protected int device_index = 0;

        public override bool OpenPeripheral(DPNP_DEVICE_TYPE type, int index)
        {
            bool ret = base.OpenPeripheral(type, index);
            if (ret && (DpnManager.controllerKeyMode == DPVRKeyMode.STEAM))
            {
                if (!SteamController.ContainsKey(index) || SteamController[index] == null)
                {
                    SteamControllerPeripheral = new DPVR_Steam_Controller_Peripheral(peripheral, device_type);
                    SteamController.Add(index, SteamControllerPeripheral);
                }
                else
                {
                    SteamControllerPeripheral = SteamController[index];
                }
            }
            return ret;
        }

        public override void DpnpUpdate()
        {
            base.DpnpUpdate();
            if (SteamControllerPeripheral != null)
            {
                SteamControllerPeripheral.DeviceUpdate();
            }
        }

        public virtual VRControllerState_t DpnpGetSteamControllerCurrentStatus ()
        {
            if (SteamControllerPeripheral == null)
            {
                return new VRControllerState_t();
            }
            return SteamControllerPeripheral.state;
        }

        public virtual VRControllerState_t DpnpGetSteamControllerPrevStatus()
        {
            if (SteamControllerPeripheral == null)
            {
                return new VRControllerState_t();
            }
            return SteamControllerPeripheral.prevState;
        }

        public virtual TrackedDevicePose_t DpnpGetSteamControllerPose()
        {
            if (SteamControllerPeripheral == null)
            {
                return new TrackedDevicePose_t();
            }
            return SteamControllerPeripheral.pose;
        }
    }
}
