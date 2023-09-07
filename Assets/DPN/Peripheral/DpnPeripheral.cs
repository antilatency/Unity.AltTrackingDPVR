/************************************************************************************

Copyright: Copyright(c) 2015-2017 Deepoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/

using UnityEngine;
using System;
using System.Threading;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dpn
{
    public class PeripheralInfo
    {
        public int pose_count;
        public int position_count;
        public int time_count;
        public int quaternion_count;
        public int vector_count;
        public int axis_count;
        public int button_count;
        public int attribute_count;
        public int feedback_count;
    }
    public class Peripheralstatus
    {
        public ReaderWriterLockSlim cacheLock = new ReaderWriterLockSlim();
        
        public int device_status;
        public float[][] pose_state;
        public float[][] position_state;
        public double[][] time_state;
        public float[][] quaternion_state;
        public float[][] vector_state;
        public float[][] axis_state;
        public int[][] button_state;

        public void CopyFrom(Peripheralstatus other)
        {
            if (other == null || this == null)
            {
                return;
            }
            other.cacheLock.EnterReadLock();
            this.cacheLock.EnterWriteLock();
            try
            {
                device_status = other.device_status;
                if (other.pose_state != null)
                {
                    if (pose_state == null)
                    {
                        pose_state = new float[other.pose_state.Length][];
                        for (int i = 0; i < other.pose_state.Length; i++)
                        {
                            pose_state[i] = new float[DpnpUnity.DPNP_VALUE_TYPE_SIZE_POSE];
                        }
                    }
                    for (int i = 0; i < other.pose_state.Length; i++)
                    {
                        other.pose_state[i].CopyTo(pose_state[i], 0);
                    }
                }
                else
                {
                    pose_state = null;
                }
                if (other.position_state != null)
                {
                    if (position_state == null)
                    {
                        position_state = new float[other.position_state.Length][];
                        for (int i = 0; i < other.position_state.Length; i++)
                        {
                            position_state[i] = new float[DpnpUnity.DPNP_VALUE_TYPE_SIZE_POSITION];
                        }
                    }
                    for (int i = 0; i < other.position_state.Length; i++)
                    {
                        other.position_state[i].CopyTo(position_state[i], 0);
                    }
                }
                else
                {
                    position_state = null;
                }
                if (other.time_state != null)
                {
                    if (time_state == null)
                    {
                        time_state = new double[other.time_state.Length][];
                        for (int i = 0; i < other.time_state.Length; i++)
                        {
                            time_state[i] = new double[DpnpUnity.DPNP_VALUE_TYPE_SIZE_TIME];
                        }
                    }
                    for (int i = 0; i < other.time_state.Length; i++)
                    {
                        other.time_state[i].CopyTo(time_state[i], 0);
                    }
                }
                else
                {
                    time_state = null;
                }
                if (other.quaternion_state != null)
                {
                    if (quaternion_state == null)
                    {
                        quaternion_state = new float[other.quaternion_state.Length][];
                        for (int i = 0; i < other.quaternion_state.Length; i++)
                        {
                            quaternion_state[i] = new float[DpnpUnity.DPNP_VALUE_TYPE_SIZE_QUATERNION];
                        }
                    }
                    for (int i = 0; i < other.quaternion_state.Length; i++)
                    {
                        other.quaternion_state[i].CopyTo(quaternion_state[i], 0);
                    }
                }
                else
                {
                    quaternion_state = null;
                }
                if (other.vector_state != null)
                {
                    if (vector_state == null)
                    {
                        vector_state = new float[other.vector_state.Length][];
                        for (int i = 0; i < other.vector_state.Length; i++)
                        {
                            vector_state[i] = new float[DpnpUnity.DPNP_VALUE_TYPE_SIZE_VECTOR];
                        }
                    }
                    for (int i = 0; i < other.vector_state.Length; i++)
                    {
                        other.vector_state[i].CopyTo(vector_state[i], 0);
                    }
                }
                else
                {
                    vector_state = null;
                }
                if (other.axis_state != null)
                {
                    if (axis_state == null)
                    {
                        axis_state = new float[other.axis_state.Length][];
                        for (int i = 0; i < other.axis_state.Length; i++)
                        {
                            axis_state[i] = new float[DpnpUnity.DPNP_VALUE_TYPE_SIZE_AXIS];
                        }
                    }
                    for (int i = 0; i < other.axis_state.Length; i++)
                    {
                        other.axis_state[i].CopyTo(axis_state[i], 0);
                    }
                }
                else
                {
                    axis_state = null;
                }
                if (other.button_state != null)
                {
                    if (button_state == null)
                    {
                        button_state = new int[other.button_state.Length][];
                        for (int i = 0; i < other.button_state.Length; i++)
                        {
                            button_state[i] = new int[DpnpUnity.DPNP_VALUE_TYPE_SIZE_BUTTON];
                        }
                    }
                    for (int i = 0; i < other.button_state.Length; i++)
                    {
                        other.button_state[i].CopyTo(button_state[i], 0);
                    }

                }
                else
                {
                    button_state = null;
                }
            }
            finally
            {
                other.cacheLock.ExitReadLock();
                this.cacheLock.ExitWriteLock();
            }
        }
    }

    public class DpnPeripheral
    {
        private DpnPeripheral(IntPtr device, string deviceId) { _device = device; _deviceId = deviceId; }

        public IntPtr _device
        {
            get;
            private set;
        }

        public string _deviceId
        {
            get;
            private set;
        }

        public PeripheralInfo peripheralInfo = null;

        public Peripheralstatus prevperipheralstatus = null;

        public Peripheralstatus peripheralstatus = null;
        
        public void DpnupGetDeviceInfo ()
        {
            if (_device == IntPtr.Zero)
                return;
            peripheralInfo = new PeripheralInfo();
            peripheralInfo.pose_count = DpnupGetDevicePoseCount();
            peripheralInfo.position_count = DpnupGetDevicePositionCount();
            peripheralInfo.time_count = DpnupGetDeviceTimeCount();
            peripheralInfo.quaternion_count = DpnupGetDeviceQuaternionCount();
            peripheralInfo.vector_count = DpnupGetDeviceVectorCount();
            peripheralInfo.axis_count = DpnupGetDeviceAxisCount();
            peripheralInfo.button_count = DpnupGetDeviceButtonCount();

            peripheralstatus = new Peripheralstatus();
            peripheralstatus.device_status = 0;
            if (peripheralInfo.pose_count != 0)
            {
                peripheralstatus.pose_state = new float[peripheralInfo.pose_count][];
                for (int i = 0; i < peripheralInfo.pose_count; i++)
                {
                    peripheralstatus.pose_state[i] = new float[DpnpUnity.DPNP_VALUE_TYPE_SIZE_POSE];
                }
            }
            else
            {
                peripheralstatus.pose_state = null;
            }

            if (peripheralInfo.position_count != 0)
            {
                peripheralstatus.position_state = new float[peripheralInfo.position_count][];
                for (int i = 0; i < peripheralInfo.position_count; i++)
                {
                    peripheralstatus.position_state[i] = new float[DpnpUnity.DPNP_VALUE_TYPE_SIZE_POSITION];
                }
            }
            else
            {
                peripheralstatus.position_state = null;
            }

            if (peripheralInfo.time_count != 0)
            {
                peripheralstatus.time_state = new double[peripheralInfo.time_count][];
                for (int i = 0; i < peripheralInfo.time_count; i++)
                {
                    peripheralstatus.time_state[i] = new double[DpnpUnity.DPNP_VALUE_TYPE_SIZE_TIME];
                }
            }
            else
            {
                peripheralstatus.time_state = null;
            }

            if (peripheralInfo.quaternion_count != 0)
            {
                peripheralstatus.quaternion_state = new float[peripheralInfo.quaternion_count][];
                for (int i = 0; i < peripheralInfo.quaternion_count; i++)
                {
                    peripheralstatus.quaternion_state[i] = new float[DpnpUnity.DPNP_VALUE_TYPE_SIZE_QUATERNION];
                }
            }
            else
            {
                peripheralstatus.quaternion_state = null;
            }

            if (peripheralInfo.vector_count != 0)
            {
                peripheralstatus.vector_state = new float[peripheralInfo.vector_count][];
                for (int i = 0; i < peripheralInfo.vector_count; i++)
                {
                    peripheralstatus.vector_state[i] = new float[DpnpUnity.DPNP_VALUE_TYPE_SIZE_VECTOR];
                }
            }
            else
            {
                peripheralstatus.vector_state = null;
            }

            if (peripheralInfo.axis_count != 0)
            {
                peripheralstatus.axis_state = new float[peripheralInfo.axis_count][];
                for (int i = 0; i < peripheralInfo.axis_count; i++)
                {
                    peripheralstatus.axis_state[i] = new float[DpnpUnity.DPNP_VALUE_TYPE_SIZE_AXIS];
                }
            }
            else
            {
                peripheralstatus.axis_state = null;
            }

            if (peripheralInfo.button_count != 0)
            {
                Debug.Log("button number " + peripheralInfo.button_count);
                peripheralstatus.button_state = new int[peripheralInfo.button_count][];
                for (int i = 0; i < peripheralInfo.button_count; i++)
                {
                    peripheralstatus.button_state[i] = new int[DpnpUnity.DPNP_VALUE_TYPE_SIZE_BUTTON];
                }
            }
            else
            {
                peripheralstatus.button_state = null;
            }
            prevperipheralstatus = new Peripheralstatus();
            prevperipheralstatus.CopyFrom(peripheralstatus);
        }

        private int FrameNum = -1;
        public void DpnupUpdateDeviceState()
        {
            if (_device == IntPtr.Zero)
            {
                return;
            }
            if (Time.frameCount == FrameNum)
            {
                return;
            }
            else
            {
                FrameNum = Time.frameCount;
            }

            prevperipheralstatus.CopyFrom(peripheralstatus);
            peripheralstatus.cacheLock.EnterWriteLock();
            try
            {
                IntPtr temp = Marshal.AllocHGlobal(sizeof(int));
                DpnupReadDeviceAttribute(DPNP_VALUE_TYPE.DPNP_VALUE_TYPE_ATTRIBUTE_DEVICE_STATUS - DPNP_VALUE_TYPE.DPNP_VALUE_TYPE_ATTRIBUTE
                    , temp, sizeof(int));
                peripheralstatus.device_status = Marshal.ReadInt32(temp);
                Marshal.FreeHGlobal(temp);
                if (peripheralInfo.pose_count > 0)
                {
                    for (int i = 0; i < peripheralInfo.pose_count; i++)
                    {
                        float[] pose = peripheralstatus.pose_state[i];
                        DpnupReadDevicePose(i, pose);
                    }
                }
                if (peripheralInfo.position_count > 0)
                {
                    for (int i = 0; i < peripheralInfo.position_count; i++)
                    {
                        float[] position = peripheralstatus.position_state[i];
                        DpnupReadDevicePosition(i, position);
                    }
                }

                if (peripheralInfo.time_count > 0)
                {
                    for (int i = 0; i < peripheralInfo.time_count; i++)
                    {
                        double[] time = peripheralstatus.time_state[i];
                        time[0] = DpnupReadDeviceTime(i);
                    }
                }

                if (peripheralInfo.quaternion_count > 0)
                {
                    for (int i = 0; i < peripheralInfo.quaternion_count; i++)
                    {
                        float[] quaternion = peripheralstatus.quaternion_state[i];
                        DpnupReadDeviceQuaternion(i, quaternion);
                    }
                }

                if (peripheralInfo.vector_count > 0)
                {
                    for (int i = 0; i < peripheralInfo.vector_count; i++)
                    {
                        float[] vector = peripheralstatus.vector_state[i];
                        DpnupReadDeviceVector(i, vector);
                    }
                }

                if (peripheralInfo.axis_count > 0)
                {
                    for (int i = 0; i < peripheralInfo.axis_count; i++)
                    {
                        float[] axis = peripheralstatus.axis_state[i];
                        axis[0] = DpnupReadDeviceAxis(i);
                    }
                }

                if (peripheralInfo.button_count > 0)
                {
                    for (int i = 0; i < peripheralInfo.button_count; i++)
                    {
                        int[] button = peripheralstatus.button_state[i];
                        button[0] = DpnupReadDeviceButton(i);
                    }
                }
            }
            finally
            {
                peripheralstatus.cacheLock.ExitWriteLock();
            }
        }

        public static void DpnpHandleDeviceEvent(IntPtr device, int event_mask, IntPtr user_data)
        {
            DpnPeripheral dev = null;
            foreach (KeyValuePair<string, PeripheralList> i in DpnDevice.Instance.GetPeripherals())
            {
                if(i.Value.peripheral._device == device)
                {
                    dev = i.Value.peripheral;
                    break;
                }
            }

            dev.peripheralstatus.cacheLock.EnterWriteLock();
            try
            {
                if ((event_mask & ((int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_CONNECT | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_DISCONNECT | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_USB_PLUGIN |
                (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_USB_UNPLUG | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_HDMI_PLUGIN | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_HDMI_UNPLUG |
                (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BASE_A_TRACK | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BASE_A_UNTRACK | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BASE_B_TRACK |
                (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BASE_B_UNTRACK)) != 0x0)
                {
                    // set device connection state
                    //
                    if ((event_mask & (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_HDMI_PLUGIN) != 0x0)
                    {
                        dev.peripheralstatus.device_status |= (int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_HDMI_PLUGIN;
                        dev.peripheralstatus.device_status &= ~(int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_HDMI_UNPLUG;
                    }

                    if ((event_mask & (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_USB_PLUGIN) != 0x0)
                    {
                        dev.peripheralstatus.device_status |= (int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_USB_PLUGIN;
                        dev.peripheralstatus.device_status &= ~(int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_USB_UNPLUG;
                    }

                    if ((event_mask & (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_HDMI_UNPLUG) != 0x0)
                    {
                        dev.peripheralstatus.device_status |= (int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_HDMI_UNPLUG;
                        dev.peripheralstatus.device_status &= ~(int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_HDMI_PLUGIN;
                    }

                    if ((event_mask & (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_USB_UNPLUG) != 0x0)
                    {
                        dev.peripheralstatus.device_status |= (int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_USB_UNPLUG;
                        dev.peripheralstatus.device_status &= ~(int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_USB_PLUGIN;
                    }

                    if ((event_mask & (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BASE_A_TRACK) != 0x0)
                    {
                        dev.peripheralstatus.device_status |= (int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_BASE_A_TRACK;
                        dev.peripheralstatus.device_status &= ~(int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_BASE_A_UNTRACK;
                    }

                    if ((event_mask & (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BASE_A_UNTRACK) != 0x0)
                    {
                        dev.peripheralstatus.device_status |= (int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_BASE_A_UNTRACK;
                        dev.peripheralstatus.device_status &= ~(int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_BASE_A_TRACK;
                    }

                    if ((event_mask & (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BASE_B_TRACK) != 0x0)
                    {
                        dev.peripheralstatus.device_status |= (int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_BASE_B_TRACK;
                        dev.peripheralstatus.device_status &= ~(int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_BASE_B_UNTRACK;
                    }

                    if ((event_mask & (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BASE_B_UNTRACK) != 0x0)
                    {
                        dev.peripheralstatus.device_status |= (int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_BASE_B_UNTRACK;
                        dev.peripheralstatus.device_status &= ~(int)DPNP_DEVICE_STATUS.DPNP_DEVICE_STATUS_BASE_B_TRACK;
                    }
                }
                if (((event_mask & (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_POSE_UPDATE) != 0) && (dev.peripheralInfo.pose_count > 0))
                {
                    for (int i = 0; i < dev.peripheralInfo.pose_count; i++)
                    {
                        float[] pose = dev.peripheralstatus.pose_state[i];
                        dev.DpnupReadDevicePose(i, pose);
                    }
                }

                if (((event_mask & (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_POSITION_UPDATE) != 0) && (dev.peripheralInfo.position_count > 0))
                {
                    for (int i = 0; i < dev.peripheralInfo.position_count; i++)
                    {
                        float[] position = dev.peripheralstatus.position_state[i];
                        dev.DpnupReadDevicePosition(i, position);
                    }
                }

                if (((event_mask & (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_TIME_UPDATE) != 0) && (dev.peripheralInfo.time_count > 0))
                {
                    for (int i = 0; i < dev.peripheralInfo.time_count; i++)
                    {
                        double[] time = dev.peripheralstatus.time_state[i];
                        time[0] = dev.DpnupReadDeviceTime(i);
                    }
                }

                if (((event_mask & (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_QUATERNION_UPDATE) != 0) && (dev.peripheralInfo.quaternion_count > 0))
                {
                    for (int i = 0; i < dev.peripheralInfo.quaternion_count; i++)
                    {
                        float[] quaternion = dev.peripheralstatus.quaternion_state[i];
                        dev.DpnupReadDeviceQuaternion(i, quaternion);
                    }
                }

                if (((event_mask & (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_VECTOR_UPDATE) != 0) && (dev.peripheralInfo.vector_count > 0))
                {
                    for (int i = 0; i < dev.peripheralInfo.vector_count; i++)
                    {
                        float[] vector = dev.peripheralstatus.vector_state[i];
                        dev.DpnupReadDeviceVector(i, vector);
                    }
                }

                if (((event_mask & (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_AXIS_UPDATE) != 0) && (dev.peripheralInfo.axis_count > 0))
                {
                    for (int i = 0; i < dev.peripheralInfo.axis_count; i++)
                    {
                        float[] axis = dev.peripheralstatus.axis_state[i];
                        axis[0] = dev.DpnupReadDeviceAxis(i);
                    }
                }

                if (((event_mask & (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BUTTON_UPDATE) != 0) && (dev.peripheralInfo.button_count > 0))
                {
                    for (int i = 0; i < dev.peripheralInfo.button_count; i++)
                    {
                        int[] button = dev.peripheralstatus.button_state[i];
                        button[0] = dev.DpnupReadDeviceButton(i);
                    }
                }
            }
            finally
            {
                dev.peripheralstatus.cacheLock.ExitWriteLock();
            }
        }

        public static int DpnupQueryDeviceCount(DPNP_DEVICE_TYPE type)
        {
            return DpnpUnity.DpnupQueryDeviceCount(type);
        }
        public static string DpnupGetDeviceId(DPNP_DEVICE_TYPE type, int index)
        {
            return Marshal.PtrToStringAnsi(DpnpUnity.DpnupGetDeviceId(type, index));
        }
        public static DpnPeripheral DpnupOpenDevice(string deviceId)
        {
            IntPtr device = IntPtr.Zero;
            if (deviceId == null)
            {
                return null;
            }
            if (deviceId != string.Empty)
            {
                IntPtr tempPtr = Marshal.StringToHGlobalAnsi(deviceId);
                device = DpnpUnity.DpnupOpenDevice(tempPtr);
                Marshal.FreeHGlobal(tempPtr);

                return device != IntPtr.Zero ? new DpnPeripheral(device, deviceId) : null;
            }
            else
            {
                return new DpnPeripheral(device, deviceId);
            }
        }
        [MarshalAs(UnmanagedType.FunctionPtr)]
        public static DpnpUnity.DpnpHandleEventFunc event_callback = new DpnpUnity.DpnpHandleEventFunc(DpnpHandleDeviceEvent);
        public static DpnPeripheral OpenPeripheralDevice(string deviceId)
        {
            DpnPeripheral peripheral = DpnPeripheral.DpnupOpenDevice(deviceId);
            if (peripheral != null)
            {
                if (deviceId != string.Empty)
                {
                    Debug.Log("OpenDevice " + deviceId + " successes.");
                }
                peripheral.DpnupGetDeviceInfo();
                peripheral.DpnupUpdateDeviceState();
                //int event_mask = (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_POSE_UPDATE | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_POSITION_UPDATE | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_TIME_UPDATE |
   //(int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_AXIS_UPDATE | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BUTTON_UPDATE | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_CONNECT | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_DISCONNECT |
    //(int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_HDMI_PLUGIN | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_HDMI_UNPLUG | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_USB_PLUGIN | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_USB_PLUGIN |
    //(int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_USB_UNPLUG | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BASE_A_TRACK |
    //(int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BASE_A_UNTRACK | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BASE_B_TRACK | (int)DPNP_EVENT_TYPE.DPNP_EVENT_TYPE_BASE_B_UNTRACK;
                //peripheral.DpnupRegisterEventNotificationFunction(null, event_mask, IntPtr.Zero);
                return peripheral;
            }
            else
            {
                Debug.Log("OpenDevice " + deviceId + " fail!");
                return null;
            }
        }
        public static void ClosePeripheralDevice(DpnPeripheral peripheral)
        {
            //peripheral.DpnupRegisterEventNotificationFunction(null, 0, IntPtr.Zero);
            peripheral.DpnupCloseDevice();
        }
        public void DpnupResume()
	    {
            if (_device != IntPtr.Zero)
            {
                DpnpUnity.DpnupResume(_device);
            }
	    }
	    public void DpnupPause()
	    {
            if (_device != IntPtr.Zero)
            {
                DpnpUnity.DpnupPause(_device);
            }
	    }
	    public void DpnupCloseDevice()
	    {
            if (_device != IntPtr.Zero)
            {
                DpnpUnity.DpnupCloseDevice(_device);
                _device = IntPtr.Zero;
            }
        }
        public int DpnupGetDeviceButtonCount()
	    {
		    if (_device != IntPtr.Zero)
			    return DpnpUnity.DpnupGetDeviceButtonCount(_device);
		    return 0;
	    }
	    public int DpnupGetDeviceAxisCount()
	    {
		    if(_device!=IntPtr.Zero)
			    return DpnpUnity.DpnupGetDeviceAxisCount (_device);
		    return 0;
	    }
	    public int DpnupGetDevicePoseCount()
	    {
		    if(_device!=IntPtr.Zero)
			    return DpnpUnity.DpnupGetDevicePoseCount (_device);
		    return 0;
	    }
	    public int DpnupGetDevicePositionCount()
	    {
		    if(_device!=IntPtr.Zero)
			    return DpnpUnity.DpnupGetDevicePositionCount (_device);
		    return 0;
	    }
	    public int DpnupGetDeviceTimeCount()
	    {
		    if (_device != IntPtr.Zero)
			    return DpnpUnity.DpnupGetDeviceTimeCount(_device);
		    return 0;
	    }
	    public int DpnupGetDeviceQuaternionCount()
	    {
		    if (_device != IntPtr.Zero)
			    return DpnpUnity.DpnupGetDeviceQuaternionCount(_device);
		    return 0;
	    }
	    public int DpnupGetDeviceVectorCount()
	    {
		    if (_device != IntPtr.Zero)
			    return DpnpUnity.DpnupGetDeviceVectorCount(_device);
		    return 0;
	    }
	    public int DpnupGetDeviceAttributeCount()
	    {
		    if (_device != IntPtr.Zero)
			    return DpnpUnity.DpnupGetDeviceAttributeCount(_device);
		    return 0;
	    }
	    public int DpnupGetDeviceFeedbackCount()
	    {
		    if(_device!=IntPtr.Zero)
			    return DpnpUnity.DpnupGetDeviceFeedbackCount (_device);
		    return 0;
	    }
	    public string DpnupReadDeviceId()
	    {
		    if(_device!=IntPtr.Zero)
			    return Marshal.PtrToStringAnsi(DpnpUnity.DpnupReadDeviceId (_device));
		    return "";
	    }
	    public string DpnupGetAssociatedDevice(DPNP_DEVICE_TYPE type)
	    {
		    if(_device!=IntPtr.Zero)
			    return Marshal.PtrToStringAnsi(DpnpUnity.DpnupGetAssociatedDevice (_device,type));
		    return "";
	    }
	    public bool DpnupReadDevicePose(int index, float[] pose)
	    {
		    if (pose.Length < DpnpUnity.DPNP_VALUE_TYPE_SIZE_POSE) return false;
		    if (_device != IntPtr.Zero)
			    return DpnpUnity.DpnupReadDevicePose(_device, index, pose);
		    return false;
	    }
	    public bool DpnupReadDevicePosition(int index, float[] position)
	    {
		    if (position.Length < DpnpUnity.DPNP_VALUE_TYPE_SIZE_POSITION) return false;
		    if (_device != IntPtr.Zero)
		    {
			    return DpnpUnity.DpnupReadDevicePosition(_device, index, position);
		    }
		    return false;
	    }
	    public double DpnupReadDeviceTime(int index)
	    {
		    if (_device != IntPtr.Zero)
			    return DpnpUnity.DpnupReadDeviceTime(_device, index);
		    return 0;
	    }
	    public bool DpnupReadDeviceQuaternion(int index, float[] quaternion)
	    {
		    if (quaternion.Length < DpnpUnity.DPNP_VALUE_TYPE_SIZE_QUATERNION) return false;
		    if (_device != IntPtr.Zero)
			    return DpnpUnity.DpnupReadDeviceQuaternion(_device, index, quaternion);
		    return false;
	    }
	    public bool DpnupReadDeviceVector(int index, float[] vector)
	    {
		    if (vector.Length < DpnpUnity.DPNP_VALUE_TYPE_SIZE_VECTOR) return false;
		    if (_device != IntPtr.Zero)
			    return DpnpUnity.DpnupReadDeviceVector(_device, index, vector);
		    return false;
	    }
	    public float DpnupReadDeviceAxis(int index)
	    {
		    if (_device != IntPtr.Zero)
			    return DpnpUnity.DpnupReadDeviceAxis(_device, index);
		    return 0.0f;
	    }
	    public int DpnupReadDeviceButton(int index)
	    {
		    if (_device != IntPtr.Zero)
			    return DpnpUnity.DpnupReadDeviceButton (_device, index);
		    return 0;
	    }
	    public int DpnupReadDeviceAttribute(int index, IntPtr buffer, int buffer_length)
	    {
		    if (_device != IntPtr.Zero)
			    return DpnpUnity.DpnupReadDeviceAttribute(_device, index, buffer, buffer_length);
		    return 0;
	    }
	    public int DpnupSetDeviceAttribute(int index, IntPtr buffer, int buffer_length)
	    {
		    if (_device != IntPtr.Zero)
			    return DpnpUnity.DpnupSetDeviceAttribute(_device, index, buffer, buffer_length);
		    return 0;
	    }
	    public void DpnupResetDevicePose(int index, float[] pose)
	    {
		    if (pose.Length < DpnpUnity.DPNP_VALUE_TYPE_SIZE_POSE) return;
		    if (_device != IntPtr.Zero) 
		    {
			    DpnpUnity.DpnupResetDevicePose(_device, index, pose);
		    }
	    }
	    public void DpnupResetDevicePosition(int index, float[] position)
	    {
		    if (position.Length < DpnpUnity.DPNP_VALUE_TYPE_SIZE_POSITION) return;
		    if (_device != IntPtr.Zero) 
		    {
			    DpnpUnity.DpnupResetDevicePosition(_device, index, position);
		    }
	    }
	    public void DpnupRegisterReferencePoseFunction(int index,IntPtr callback,IntPtr userData)
	    {
		    if (_device != IntPtr.Zero) 
			    DpnpUnity.DpnupRegisterReferencePoseFunction (_device,index,callback,userData);
	    }
	    public void DpnupRegisterRererencePositionFunction(int index,IntPtr callback,IntPtr userData)
	    {
		    if (_device != IntPtr.Zero) 
			    DpnpUnity.DpnupRegisterRererencePositionFunction (_device,index,callback,userData);
	    }
	    public void DpnupRegisterEventNotificationFunction(DpnpUnity.DpnpHandleEventFunc callback, int event_mask, IntPtr userData)
	    {
		    if (_device != IntPtr.Zero)
			    DpnpUnity.DpnupRegisterEventNotificationFunction(_device, callback, event_mask, userData);
	    }
	    public void DpnupWriteDeviceFeedback(int index,float value)
	    {
		    if (_device != IntPtr.Zero) 
			    DpnpUnity.DpnupWriteDeviceFeedback (_device,index,value);
	    }
        public void DpnupHandleControllerData(int hand, ref DpnnControllerStateRecordOriginal src, ref DpnnControllerStateRecord dest)
        {
            if (_device != IntPtr.Zero)
                DpnpUnity.DpnupHandleControllerData(_device, hand, ref src, ref dest);
        }

        public void DpnupBootPair(bool value)
        {
            if (_device != IntPtr.Zero)
                DpnpUnity.DpnupBootPair(_device, value);
        }

        public void DpnupSetUnbindDevice()
        {
            if(_device != IntPtr.Zero)
            {
                DpnpUnity.DpnupSetUnbindDevice(_device);
            }
        }

        public string DpnupGetBondDeviceName()
        {
            if (_device != IntPtr.Zero)
            {
                byte[] name = new byte[1024];
                name[0] = 0;
                DpnpUnity.DpnupGetBondDeviceName(_device, ref name[0]);
                return System.Text.Encoding.Default.GetString(name, 0, name.Length);
            }
            return "";
        }
    }
}
