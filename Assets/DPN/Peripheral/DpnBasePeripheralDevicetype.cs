/************************************************************************************

Copyright   :   Copyright 2015-2017 DeePoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/

using UnityEngine;
using System.Collections;

namespace dpn
{
    public class DpnBasePeripheralDevicetype : DpnBasePeripheral
    {
        public DPNP_DEVICE_TYPE device_type = DPNP_DEVICE_TYPE.DPNP_DEVICE_TYPE_NONE;
        public int device_index = 0;

        public virtual void OnEnable()
        {
            if (device_type != DPNP_DEVICE_TYPE.DPNP_DEVICE_TYPE_NONE)
            {
                OpenPeripheral(device_type, device_index);
            }
        }
    }
}
