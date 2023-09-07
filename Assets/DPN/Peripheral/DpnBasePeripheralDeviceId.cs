using UnityEngine;
using System.Collections;

namespace dpn
{
    public class DpnBasePeripheralDeviceId : DpnBasePeripheral
    {
        public string deviceId = null;
        public virtual void OnEnable()
        {
            if (deviceId != null)
            {
                OpenPeripheral(deviceId);
            }
        }
    }
}
