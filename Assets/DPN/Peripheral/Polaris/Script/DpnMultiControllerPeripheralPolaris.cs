


using System;
using System.Collections.Generic;
using UnityEngine;

namespace dpn
{
    public class DpnMultiControllerPeripheralPolaris :DpnMultiControllerPeripheral
    {
        static string[] s_controllerNames = { "controller(left)", "controller(right)" };

        public DpnMultiControllerPeripheralPolaris()
        {
            PeripheralType = DPVRPeripheral.Polaris;
        }

        public void Awake()
        {
            _controllerNames = s_controllerNames;

            _controllers = new DpnBasePeripheral[2];
        }

        public override DpnBasePeripheral GetController(string controllerName)
        {
            return GetDPVRTrackedController(controllerName);
        }

        DPVR_TrackedController GetDPVRTrackedController(string controllerName)
        {
            DPVR_TrackedController ret = null;
            Transform transform = gameObject.transform.Find(controllerName);
            if (transform != null)
            {
                ret = transform.GetComponent<DPVR_TrackedController>();
            }
            return ret;
        }

    }
}