/************************************************************************************

Copyright: Copyright(c) 2015-2017 Deepoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/
using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace dpn
{
    public class DpnpUnity
    {
        public const string LibDpn = "DpnUnity";
        public const int DPNP_VALUE_TYPE_SIZE_POSE = 10;
        public const int DPNP_VALUE_TYPE_SIZE_POSITION = 9;
        public const int DPNP_VALUE_TYPE_SIZE_TIME = 1;
        public const int DPNP_VALUE_TYPE_SIZE_QUATERNION = 4;
        public const int DPNP_VALUE_TYPE_SIZE_VECTOR = 3;
        public const int DPNP_VALUE_TYPE_SIZE_AXIS = 1;
        public const int DPNP_VALUE_TYPE_SIZE_BUTTON = 1;
        public const int DPNP_VALUE_TYPE_SIZE_ATTRIBUTE = 1;
        public const int DPNP_VALUE_TYPE_SIZE_FEEDBACK = 1;
        public const int DPNP_VALUE_TYPE_SIZE_SENSOR = 11;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void DpnpReadPoseFunc(IntPtr device, IntPtr userData, float[] pose);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void DpnpReadPositionFunc(IntPtr device, IntPtr userData, float[] position);
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void DpnpHandleEventFunc(IntPtr device, int event_mask, IntPtr userData);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static void DpnupResume(IntPtr device);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static void DpnupPause(IntPtr device);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static int DpnupQueryDeviceCount(DPNP_DEVICE_TYPE type);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr DpnupGetDeviceId(DPNP_DEVICE_TYPE type, int index);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr DpnupOpenDevice(IntPtr deviceId);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static void DpnupCloseDevice(IntPtr device);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static int DpnupGetDeviceButtonCount(IntPtr device);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static int DpnupGetDeviceAxisCount(IntPtr device);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static int DpnupGetDevicePoseCount(IntPtr device);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static int DpnupGetDevicePositionCount(IntPtr device);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static int DpnupGetDeviceTimeCount(IntPtr device);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static int DpnupGetDeviceQuaternionCount(IntPtr device);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static int DpnupGetDeviceVectorCount(IntPtr device);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static int DpnupGetDeviceAttributeCount(IntPtr device);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static int DpnupGetDeviceFeedbackCount(IntPtr device);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr DpnupReadDeviceId(IntPtr device);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr DpnupGetAssociatedDevice(IntPtr device, DPNP_DEVICE_TYPE type);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool DpnupReadDevicePose(IntPtr device, int index, float[] pose);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool DpnupReadDevicePosition(IntPtr device, int index, float[] position);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static double DpnupReadDeviceTime(IntPtr device, int index);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool DpnupReadDeviceQuaternion(IntPtr device, int index, float[] quaternion);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool DpnupReadDeviceVector(IntPtr device, int index, float[] vector);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static float DpnupReadDeviceAxis(IntPtr device, int index);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static int DpnupReadDeviceButton(IntPtr device, int index);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static int DpnupReadDeviceAttribute(IntPtr device, int index, IntPtr buffer, int buffer_length);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static int DpnupSetDeviceAttribute(IntPtr device, int index, IntPtr buffer, int buffer_length);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static void DpnupResetDevicePose(IntPtr device, int index, float[] pose);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static void DpnupResetDevicePosition(IntPtr device, int index, float[] position);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static void DpnupRegisterReferencePoseFunction(IntPtr device, int index, IntPtr callback, IntPtr userData);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static void DpnupRegisterRererencePositionFunction(IntPtr device, int index, IntPtr callback, IntPtr userData);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static void DpnupRegisterEventNotificationFunction(IntPtr device, DpnpHandleEventFunc callback, int event_mask, IntPtr userData);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static void DpnupWriteDeviceFeedback(IntPtr device, int index, float value);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static void DpnupHandleControllerData(IntPtr device, int hand, ref DpnnControllerStateRecordOriginal src, ref DpnnControllerStateRecord dest);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static void DpnupBootPair(IntPtr device, bool value);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static void DpnupSetUnbindDevice(IntPtr device);

        [DllImport(LibDpn, CallingConvention = CallingConvention.Cdecl)]
        public extern static void DpnupGetBondDeviceName(IntPtr device, ref byte name);
        
    }

    public enum DPNP_DEVICE_TYPE
    {
        DPNP_DEVICE_TYPE_NONE = -1,
        DPNP_DEVICE_TYPE_JOYSTICK = 0,
        DPNP_DEVICE_TYPE_LEFT_HAND = 1,
        DPNP_DEVICE_TYPE_RIGHT_HAND = 2,
        DPNP_DEVICE_TYPE_HEAD_TRACKER = 3,
        DPNP_DEVICE_TYPE_LEFT_CONTROLLER = 4,
        DPNP_DEVICE_TYPE_RIGHT_CONTROLLER = 5,
        DPNP_DEVICE_TYPE_LEFT_JOYSTICK = 6,
        DPNP_DEVICE_TYPE_RIGHT_JOYSTICK = 7,
        DPNP_DEVICE_TYPE_MOUSE_KEYBOARD = 8,
        DPNP_DEVICE_TYPE_TRACKER = 9,
        DPNP_DEVICE_TYPE_DEBUG_CONTROL = 10,
        DPNP_DEVICE_TYPE_BASE_MASTER = 11,
        DPNP_DEVICE_TYPE_BASE_SLAVE = 12,
        DPNP_DEVICE_TYPE_MR_CONTROLLER = 13,

        // This should always be the last one.
        DPNP_DEVICE_TYPE_NUM,
    };

    public enum DPNP_EVENT_TYPE
    {
        DPNP_EVENT_TYPE_CONNECT = 0x0001,
        DPNP_EVENT_TYPE_DISCONNECT = 0x0002,
        DPNP_EVENT_TYPE_USB_PLUGIN = 0x0004,
        DPNP_EVENT_TYPE_USB_UNPLUG = 0x0008,
        DPNP_EVENT_TYPE_HDMI_PLUGIN = 0x0010,
        DPNP_EVENT_TYPE_HDMI_UNPLUG = 0x0020,
        DPNP_EVENT_TYPE_POSE_UPDATE = 0x0040,
        DPNP_EVENT_TYPE_POSITION_UPDATE = 0x0080,
        DPNP_EVENT_TYPE_TIME_UPDATE = 0x0100,
        DPNP_EVENT_TYPE_QUATERNION_UPDATE = 0x0200,
        DPNP_EVENT_TYPE_VECTOR_UPDATE = 0x0400,
        DPNP_EVENT_TYPE_AXIS_UPDATE = 0x0800,
        DPNP_EVENT_TYPE_BUTTON_UPDATE = 0x1000,
        DPNP_EVENT_TYPE_ATTRIBUTE_UPDATE = 0x2000,
        DPNP_EVENT_TYPE_PERFORMANCE_LOW = 0x8000,// 0x4000 used by p-sensor
        DPNP_EVENT_TYPE_BASE_A_TRACK = 0x10000,
        DPNP_EVENT_TYPE_BASE_A_UNTRACK = 0x20000,
        DPNP_EVENT_TYPE_BASE_B_TRACK = 0x40000,
        DPNP_EVENT_TYPE_BASE_B_UNTRACK = 0x80000,
        DPNP_EVENT_TYPE_FILE_STREAM_END = 0x100000,
        DPNP_EVENT_TYPE_VSYNC = 0x200000
    };

    public enum DPNP_DEVICE_STATUS
    {
        DPNP_DEVICE_STATUS_CONNECT = 0x0001,
        DPNP_DEVICE_STATUS_DISCONNECT = 0x0002,
        DPNP_DEVICE_STATUS_USB_PLUGIN = 0x0004,
        DPNP_DEVICE_STATUS_USB_UNPLUG = 0x0008,
        DPNP_DEVICE_STATUS_HDMI_PLUGIN = 0x0010,
        DPNP_DEVICE_STATUS_HDMI_UNPLUG = 0x0020,
        DPNP_DEVICE_STATUS_BASE_A_TRACK = 0x0040,
        DPNP_DEVICE_STATUS_BASE_A_UNTRACK = 0x0080,
        DPNP_DEVICE_STATUS_BASE_B_TRACK = 0x0100,
        DPNP_DEVICE_STATUS_BASE_B_UNTRACK = 0x0200,
    };

    public enum DPNP_VALUE_TYPE
    {
        DPNP_VALUE_TYPE_POSE_COUNT = 0,
        DPNP_VALUE_TYPE_POSITION_COUNT,
        DPNP_VALUE_TYPE_TIME_COUNT,
        DPNP_VALUE_TYPE_QUATERNION_COUNT,
        DPNP_VALUE_TYPE_VECTOR_COUNT,
        DPNP_VALUE_TYPE_AXIS_COUNT,
        DPNP_VALUE_TYPE_BUTTON_COUNT,
        DPNP_VALUE_TYPE_ATTRIBUTE_COUNT,
        DPNP_VALUE_TYPE_FEEDBACK_COUNT,

        DPNP_VALUE_TYPE_POSE = 0x100,                // Size = 10, type: float
                                                        // POSE0
        DPNP_VALUE_TYPE_POSE_W = DPNP_VALUE_TYPE_POSE,
        DPNP_VALUE_TYPE_POSE_I,
        DPNP_VALUE_TYPE_POSE_J,
        DPNP_VALUE_TYPE_POSE_K,
        DPNP_VALUE_TYPE_ANGULAR_VELOCITY_X,
        DPNP_VALUE_TYPE_ANGULAR_VELOCITY_Y,
        DPNP_VALUE_TYPE_ANGULAR_VELOCITY_Z,
        DPNP_VALUE_TYPE_ANGULAR_ACCELERATION_X,
        DPNP_VALUE_TYPE_ANGULAR_ACCELERATION_Y,
        DPNP_VALUE_TYPE_ANGULAR_ACCELERATION_Z,
        // POSE1

        DPNP_VALUE_TYPE_POSITION = 0x200,           // Size = 9, type: float
                                                    // POSITION0
        DPNP_VALUE_TYPE_POSITION_X = DPNP_VALUE_TYPE_POSITION,
        DPNP_VALUE_TYPE_POSITION_Y,
        DPNP_VALUE_TYPE_POSITION_Z,
        DPNP_VALUE_TYPE_POSITION_VELOCITY_X,
        DPNP_VALUE_TYPE_POSITION_VELOCITY_Y,
        DPNP_VALUE_TYPE_POSITION_VELOCITY_Z,
        DPNP_VALUE_TYPE_POSITION_ACCELERATION_X,
        DPNP_VALUE_TYPE_POSITION_ACCELERATION_Y,
        DPNP_VALUE_TYPE_POSITION_ACCELERATION_Z,
        // POSITION1

        DPNP_VALUE_TYPE_TIME = 0x300,               //Size = 1, type: double
                                                    // TIME_CURRENT
        DPNP_VALUE_TYPE_TIME_CURRENT = DPNP_VALUE_TYPE_TIME,
        /*    TIME_POSE_0
                        .
                        .
                        .
                TIME_POSE_N
                TIME_POSITION_0
                            .
                            .
                            .
                TIME_POSITION_N
                    others         */


        DPNP_VALUE_TYPE_QUATERNION = 0x400,         //Size = 4, type: float
                                                    // QUATERNION0
        DPNP_VALUE_TYPE_QUATERNION_W = DPNP_VALUE_TYPE_QUATERNION,
        DPNP_VALUE_TYPE_QUATERNION_I,
        DPNP_VALUE_TYPE_QUATERNION_J,
        DPNP_VALUE_TYPE_QUATERNION_K,
        // QUATERNION1

        DPNP_VALUE_TYPE_VECTOR = 0x500,             //Size = 3, type: float
                                                    // VECTOR0
        DPNP_VALUE_TYPE_VECTOR_X = DPNP_VALUE_TYPE_VECTOR,
        DPNP_VALUE_TYPE_VECTOR_Y,
        DPNP_VALUE_TYPE_VECTOR_Z,
        // VECTOR1

        DPNP_VALUE_TYPE_AXIS = 0x600,               //Size = 1, type: float
        DPNP_VALUE_TYPE_AXIS_X = DPNP_VALUE_TYPE_AXIS,
        DPNP_VALUE_TYPE_AXIS_Y,

        DPNP_VALUE_TYPE_BUTTON = 0x700,             //Size = 1, type: int
        DPNP_VALUE_TYPE_KEYBOARD,

        DPNP_VALUE_TYPE_ATTRIBUTE = 0x800,          //Size = 1, type: void *
        DPNP_VALUE_TYPE_ATTRIBUTE_DEVICE_STATUS = DPNP_VALUE_TYPE_ATTRIBUTE,
        DPNP_VALUE_TYPE_ATTRIBUTE_DEVICE_NAME,
        DPNP_VALUE_TYPE_ATTRIBUTE_DEVICE_SERIAL_NUMBER,
        DPNP_VALUE_TYPE_ATTRIBUTE_DEVICE_VID,
        DPNP_VALUE_TYPE_ATTRIBUTE_DEVICE_PID,
        DPNP_VALUE_TYPE_ATTRIBUTE_FIRMWARE_VERSION,
        DPNP_VALUE_TYPE_ATTRIBUTE_HARDWARE_VERSION,
        DPNP_VALUE_TYPE_ATTRIBUTE_BACK_LIGHT,
        DPNP_VALUE_TYPE_ATTRIBUTE_P_SENSOR,
        DPNP_VALUE_TYPE_ATTRIBUTE_BATTERY_POWER,
        DPNP_VALUE_TYPE_ATTRIBUTE_RF_VERSION,
        DPNP_VALUE_TYPE_ATTRIBUTE_FPGA_VERSION,
        DPNP_VALUE_TYPE_ATTRIBUTE_RECENTER_GLOBAL,
        DPNP_VALUE_TYPE_ATTRIBUTE_FIX_POSE,     // always makes hmd pose at (0,0,0,1.0)
        DPNP_VALUE_TYPE_ATTRIBUTE_SCREEN, // On/Off
        DPNP_VALUE_TYPE_ATTRIBUTE_RESET_SENSOR_FUSION,
        DPNP_VALUE_TYPE_ATTRIBUTE_DDK_LOG_PATH_NAME, // void type, passing char * strings
        DPNP_VALUE_TYPE_ATTRIBUTE_DDK_LOG_MASK, // int type
        DPNP_VALUE_TYPE_ATTRIBUTE_DDK_UDP_PORT, // int type
        DPNP_VALUE_TYPE_ATTRIBUTE_DDK_UDP_INJECT, // int type 0 disable, 1 enable
        DPNP_VALUE_TYPE_ATTRIBUTE_DDK_UDP_LOG, // int type 0 disable, 1 enable
        DPNP_VALUE_TYPE_ATTRIBUTE_DDK_SRC_FILENAME, // void type, passing char * strings
        DPNP_VALUE_TYPE_ATTRIBUTE_DDK_FILE_SRC_EN, // int type 0 disable, 1 enable
        DPNP_VALUE_TYPE_ATTRIBUTE_WRONG_BASE_USE,
        DPNP_VALUE_TYPE_ATTRIBUTE_HMD_EEPROM_ERROR,
        DPNP_VALUE_TYPE_ATTRIBUTE_REBOOT,
        DPNP_VALUE_TYPE_ATTRIBUTE_HMD_ONLY,// int type 0 disable, 1 enable
        DPNP_VALUE_TYPE_ATTRIBUTE_INVERT_YAW,// int type false disable, true enable
        DPNP_VALUE_TYPE_ATTRIBUTE_DEVICE_MODE,
        DPNP_VALUE_TYPE_ATTRIBUTE_EYE_OFFSET,
        DPNP_VALUE_TYPE_ATTRIBUTE_AUTOCALIB_STATUS,//polaris1.1 autoCalibration status
        DPNP_VALUE_TYPE_ATTRIBUTE_HMD_REAL_YAW,// nolo
        DPNP_VALUE_TYPE_ATTRIBUTE_RECENTER_WITH_YAW,// nolo
        DPNP_VALUE_TYPE_ATTRIBUTE_PLUGIN_NAME,// nolo / Ximmerse / Polaris
        DPNP_VALUE_TYPE_ATTRIBUTE_MOVEMENT,// movement
        DPNP_VALUE_TYPE_ATTRIBUTE_INTERACTIVE_TYPE, // 0-mobile touchpad 1-controller
        DPNP_VALUE_TYPE_ATTRIBUTE_INTERACTIVE_HAND, // 0-right, 1-left

        DPNP_VALUE_TYPE_FEEDBACK = 0x900,           //Size = 1, type: void *
        DPNP_VALUE_TYPE_FEEDBACK_BREATH_LIGHT = DPNP_VALUE_TYPE_FEEDBACK,
        DPNP_VALUE_TYPE_FEEDBACK_MOTOR,

        DPNP_VALUE_TYPE_SENSOR = 0xa00,           //Size = 1, type: void *
        DPNP_VALUE_TYPE_SENSOR_ACC_X = DPNP_VALUE_TYPE_SENSOR,
        DPNP_VALUE_TYPE_SENSOR_ACC_Y,
        DPNP_VALUE_TYPE_SENSOR_ACC_Z,
        DPNP_VALUE_TYPE_SENSOR_GYRO_X,
        DPNP_VALUE_TYPE_SENSOR_GYRO_Y,
        DPNP_VALUE_TYPE_SENSOR_GYRO_Z,
        DPNP_VALUE_TYPE_SENSOR_MAG_X,
        DPNP_VALUE_TYPE_SENSOR_MAG_Y,
        DPNP_VALUE_TYPE_SENSOR_MAG_Z,
        DPNP_VALUE_TYPE_SENSOR_TEMPERATURE,
        DPNP_VALUE_TYPE_SENSOR_TIMEINSECONDS,
    };
    public enum DPNDDK_BUTTONMASK
    {
        DPNDDK_BUTTONMASK_B = 0x01,
        DPNDDK_BUTTONMASK_A = 0x02,
        DPNDDK_BUTTONMASK_GRIP = 0x04,
        DPNDDK_BUTTONMASK_HOME = 0x08,
        DPNDDK_BUTTONMASK_TRIGER = 0x10,
        DPNDDK_BUTTONMASK_A_II = 0x20, // key A of another controller

    };
    public enum DPNP_POLARIS_BUTTONS
    {
        DPNP_POLARIS_BUTTONS_ST, // stick button
        DPNP_POLARIS_BUTTONS_XA,
        DPNP_POLARIS_BUTTONS_YB,
        DPNP_POLARIS_BUTTONS_HOME_MENU,

        DPNP_POLARIS_BUTTONS_NUM,
    };
    public enum DPNP_POLARIS_BUTTONMASK
    {
        DPNP_POLARIS_BUTTONMASK_B = 0x01,
        DPNP_POLARIS_BUTTONMASK_A = 0x02,
        DPNP_POLARIS_BUTTONMASK_GRIP = 0x04,
        DPNP_POLARIS_BUTTONMASK_HOME = 0x08,
        DPNP_POLARIS_BUTTONMASK_TRIGER = 0x10,
        DPNP_POLARIS_BUTTONMASK_A_II = 0x20, // key A of another controller
        DPNP_POLARIS_BUTTONMASK_POWER = 0x40,
    };
    public enum DPNP_POLARIS_AXES
    {
        DPNP_POLARIS_AXIS_T,
        DPNP_POLARIS_AXIS_B,
        DPNP_POLARIS_AXIS_X, // stick x
        DPNP_POLARIS_AXIS_Y, // stick y

        DPNP_POLARIS_AXES_NUM,
    };
    public enum DPNP_DAYDREAM_BUTTONS
    {
        DPNP_DAYDREAM_BUTTON_APP,
        DPNP_DAYDREAM_BUTTON_HOME,
        DPNP_DAYDREAM_BUTTON_CLICK,
        DPNP_DAYDREAM_BUTTON_VOLUMEDOWN,
        DPNP_DAYDREAM_BUTTON_VOLUMEUP,
        DPNP_DAYDREAM_BUTTON_NUM,
    };
    public enum DPNP_FLIP_BUTTONS
    {
        DPNP_FLIP_BUTTON_APP,
        DPNP_FLIP_BUTTON_HOME,
        DPNP_FLIP_BUTTON_CLICK,
        DPNP_FLIP_BUTTON_TOUCH,
        DPNP_FLIP_BUTTON_TRIGGGER,
        DPNP_FLIP_BUTTON_RECENTERING,
        DPNP_FLIP_BUTTON_RECENTERED,
        DPNP_FLIP_BUTTON_NUM,
    };
    public enum DPNP_DAYDREAM_AXES
    {
        DPNP_DAYDREAM_AXIS_X,
        DPNP_DAYDREAM_AXIS_Y,
    };
    public enum DPNP_POLARIS_BASE_STATION
    {
        DPNP_POLARIS_BASE_A,
        DPNP_POLARIS_BASE_B,

        DPNP_POLARIS_BASE_NUM,
    };
    public enum DPNP_DAYDREAM_ATTRIBUTE
    {
        DPNP_DAYDREAM_ATTRIBUTE_DEVICE_STATUS = 0,
        DPNP_DAYDREAM_ATTRIBUTE_DEVICE_NAME,
        DPNP_DAYDREAM_ATTRIBUTE_DEVICE_SERIAL_NUMBER,
        DPNP_DAYDREAM_ATTRIBUTE_DEVICE_VID,
        DPNP_DAYDREAM_ATTRIBUTE_DEVICE_PID,
        DPNP_DAYDREAM_ATTRIBUTE_FIRMWARE_VERSION,
        DPNP_DAYDREAM_ATTRIBUTE_SOFTWARE_VERSION,
        DPNP_DAYDREAM_ATTRIBUTE_BACK_LIGHT,
        DPNP_DAYDREAM_ATTRIBUTE_UPDATE,
    }
    public enum DPNP_MOUSE_AXIS
    {
        DPNP_MOUSE_X,
        DPNP_MOUSE_Y,
        DPNP_MOUSE_WHEEL
    };
    public enum DPNP_JOYSTICK_AXIS
    {
        DPNP_JOYSTICK_X,
        DPNP_JOYSTICK_Y,
        DPNP_JOYSTICK_Z,

        DPNP_JOYSTICK_RX,
        DPNP_JOYSTICK_RY,
        DPNP_JOYSTICK_RZ,

        DPNP_JOYSTICK_POV
    };

    enum DPNP_COMMON_BUTTONS
    {
        DPNP_KEYBOARD_BUTTON_0,
        DPNP_KEYBOARD_BUTTON_1,
        DPNP_KEYBOARD_BUTTON_2,
        DPNP_KEYBOARD_BUTTON_3,
        DPNP_KEYBOARD_BUTTON_4,
        DPNP_KEYBOARD_BUTTON_5,
        DPNP_KEYBOARD_BUTTON_6,
        DPNP_KEYBOARD_BUTTON_7,
        DPNP_KEYBOARD_BUTTON_8,
        DPNP_KEYBOARD_BUTTON_9,

        DPNP_KEYBOARD_BUTTON_A,
        DPNP_KEYBOARD_BUTTON_B,
        DPNP_KEYBOARD_BUTTON_C,
        DPNP_KEYBOARD_BUTTON_D,
        DPNP_KEYBOARD_BUTTON_E,
        DPNP_KEYBOARD_BUTTON_F,
        DPNP_KEYBOARD_BUTTON_G,
        DPNP_KEYBOARD_BUTTON_H,
        DPNP_KEYBOARD_BUTTON_I,
        DPNP_KEYBOARD_BUTTON_J,
        DPNP_KEYBOARD_BUTTON_K,
        DPNP_KEYBOARD_BUTTON_L,
        DPNP_KEYBOARD_BUTTON_M,
        DPNP_KEYBOARD_BUTTON_N,
        DPNP_KEYBOARD_BUTTON_O,
        DPNP_KEYBOARD_BUTTON_P,
        DPNP_KEYBOARD_BUTTON_Q,
        DPNP_KEYBOARD_BUTTON_R,
        DPNP_KEYBOARD_BUTTON_S,
        DPNP_KEYBOARD_BUTTON_T,
        DPNP_KEYBOARD_BUTTON_U,
        DPNP_KEYBOARD_BUTTON_V,
        DPNP_KEYBOARD_BUTTON_W,
        DPNP_KEYBOARD_BUTTON_X,
        DPNP_KEYBOARD_BUTTON_Y,
        DPNP_KEYBOARD_BUTTON_Z,

        DPNP_KEYBOARD_BUTTON_F1,
        DPNP_KEYBOARD_BUTTON_F2,
        DPNP_KEYBOARD_BUTTON_F3,
        DPNP_KEYBOARD_BUTTON_F4,
        DPNP_KEYBOARD_BUTTON_F5,
        DPNP_KEYBOARD_BUTTON_F6,
        DPNP_KEYBOARD_BUTTON_F7,
        DPNP_KEYBOARD_BUTTON_F8,
        DPNP_KEYBOARD_BUTTON_F9,
        DPNP_KEYBOARD_BUTTON_F10,
        DPNP_KEYBOARD_BUTTON_F11,
        DPNP_KEYBOARD_BUTTON_F12,
        DPNP_KEYBOARD_BUTTON_F13,
        DPNP_KEYBOARD_BUTTON_F14,
        DPNP_KEYBOARD_BUTTON_F15,

        DPNP_KEYBOARD_BUTTON_BACKSPACE,
        DPNP_KEYBOARD_BUTTON_TAB,
        DPNP_KEYBOARD_BUTTON_CLEAR,
        DPNP_KEYBOARD_BUTTON_RETURN,
        DPNP_KEYBOARD_BUTTON_PAUSE,
        DPNP_KEYBOARD_BUTTON_ESCAPE,
        DPNP_KEYBOARD_BUTTON_SPACE,

        DPNP_KEYBOARD_BUTTON_KEYPAD0,
        DPNP_KEYBOARD_BUTTON_KEYPAD1,
        DPNP_KEYBOARD_BUTTON_KEYPAD2,
        DPNP_KEYBOARD_BUTTON_KEYPAD3,
        DPNP_KEYBOARD_BUTTON_KEYPAD4,
        DPNP_KEYBOARD_BUTTON_KEYPAD5,
        DPNP_KEYBOARD_BUTTON_KEYPAD6,
        DPNP_KEYBOARD_BUTTON_KEYPAD7,
        DPNP_KEYBOARD_BUTTON_KEYPAD8,
        DPNP_KEYBOARD_BUTTON_KEYPAD9,

        DPNP_KEYBOARD_BUTTON_KEYPAD_PERIOD, // del
        DPNP_KEYBOARD_BUTTON_KEYPAD_DIVIDE, // /
        DPNP_KEYBOARD_BUTTON_KEYPAD_MULTIPLY, // *
        DPNP_KEYBOARD_BUTTON_KEYPAD_MINUS, // -
        DPNP_KEYBOARD_BUTTON_KEYPAD_PLUS, // +
        DPNP_KEYBOARD_BUTTON_KEYPAD_ENTER,
        DPNP_KEYBOARD_BUTTON_KEYPAD_EQUALS, // =

        DPNP_KEYBOARD_BUTTON_UP,
        DPNP_KEYBOARD_BUTTON_DOWN,
        DPNP_KEYBOARD_BUTTON_RIGHT,
        DPNP_KEYBOARD_BUTTON_LEFT,

        DPNP_KEYBOARD_BUTTON_INSERT,
        DPNP_KEYBOARD_BUTTON_DELETE,
        DPNP_KEYBOARD_BUTTON_HOME,
        DPNP_KEYBOARD_BUTTON_END,
        DPNP_KEYBOARD_BUTTON_PAGE_UP,
        DPNP_KEYBOARD_BUTTON_PAGE_DOWN,

        DPNP_KEYBOARD_BUTTON_PRINT,
        DPNP_KEYBOARD_BUTTON_SYS_REQ,
        DPNP_KEYBOARD_BUTTON_BREAK,

        DPNP_KEYBOARD_BUTTON_EXCLAIM, // !
        DPNP_KEYBOARD_BUTTON_DOUBLE_QUOTE, // "
        DPNP_KEYBOARD_BUTTON_HASH, // #
        DPNP_KEYBOARD_BUTTON_DOLLAR, // $
        DPNP_KEYBOARD_BUTTON_AMPERSAND, // &
        DPNP_KEYBOARD_BUTTON_QUOTE, //'
        DPNP_KEYBOARD_BUTTON_LEFT_PAREN, // (
        DPNP_KEYBOARD_BUTTON_RIGHT_PAREN, // )
        DPNP_KEYBOARD_BUTTON_ASTERISK, // *
        DPNP_KEYBOARD_BUTTON_PLUS, // +
        DPNP_KEYBOARD_BUTTON_COMMA, // ,
        DPNP_KEYBOARD_BUTTON_MINUS, // -
        DPNP_KEYBOARD_BUTTON_PERIOD, // .
        DPNP_KEYBOARD_BUTTON_SLASH, // /
        DPNP_KEYBOARD_BUTTON_COLON, // :
        DPNP_KEYBOARD_BUTTON_SEMICOLON, // ;
        DPNP_KEYBOARD_BUTTON_LESS, // <
        DPNP_KEYBOARD_BUTTON_EQUALS, // =
        DPNP_KEYBOARD_BUTTON_GREATER, // >
        DPNP_KEYBOARD_BUTTON_QUESTION, // ?
        DPNP_KEYBOARD_BUTTON_AT, // @
        DPNP_KEYBOARD_BUTTON_LEFT_BRACKET, // [
        DPNP_KEYBOARD_BUTTON_RIGHT_BRACKET, // ]
        DPNP_KEYBOARD_BUTTON_CARET, // ^
        DPNP_KEYBOARD_BUTTON_UNDERSCORE, // _
        DPNP_KEYBOARD_BUTTON_BACKQUOTE, // `

        DPNP_KEYBOARD_BUTTON_NUMLOCK,
        DPNP_KEYBOARD_BUTTON_CAPSLOCK,
        DPNP_KEYBOARD_BUTTON_SCROLLLOCK,
        DPNP_KEYBOARD_BUTTON_RIGHT_SHIFT,
        DPNP_KEYBOARD_BUTTON_LEFT_SHIFT,
        DPNP_KEYBOARD_BUTTON_RIGHT_CONTROL,
        DPNP_KEYBOARD_BUTTON_LEFT_CONTROL,
        DPNP_KEYBOARD_BUTTON_RIGHT_ALT,
        DPNP_KEYBOARD_BUTTON_LEFT_ALT,

        DPNP_KEYBOARD_BUTTON_LEFT_COMMAND,
        DPNP_KEYBOARD_BUTTON_LEFT_APPLE,
        DPNP_KEYBOARD_BUTTON_LEFT_WINDOWS,
        DPNP_KEYBOARD_BUTTON_RIGHT_COMMAND,
        DPNP_KEYBOARD_BUTTON_RIGHT_APPLE,
        DPNP_KEYBOARD_BUTTON_RIGHT_WINDOWS,

        DPNP_KEYBOARD_BUTTON_ALT_GR,
        DPNP_KEYBOARD_BUTTON_HELP,
        DPNP_KEYBOARD_BUTTON_MENU,

        DPNP_MOUSE_LEFT_BUTTON = 0x100,
        DPNP_MOUSE_RIGHT_BUTTON,
        DPNP_MOUSE_MIDDLE_BUTTON
    };
    /*
    public enum DPNP_AXIS_TYPE {
        DPNP_AXIS_TYPE_POSE = 0,
        DPNP_AXIS_TYPE_POSE_S,
        DPNP_AXIS_TYPE_POSE_I,
        DPNP_AXIS_TYPE_POSE_J,
        DPNP_AXIS_TYPE_POSE_K,

        DPNP_AXIS_TYPE_POSE_S0 = DPNP_AXIS_TYPE_POSE_S,
        DPNP_AXIS_TYPE_POSE_I0 = DPNP_AXIS_TYPE_POSE_I,
        DPNP_AXIS_TYPE_POSE_J0 = DPNP_AXIS_TYPE_POSE_J,
        DPNP_AXIS_TYPE_POSE_K0 = DPNP_AXIS_TYPE_POSE_K,

        DPNP_AXIS_TYPE_POSE_S1,
        DPNP_AXIS_TYPE_POSE_I1,
        DPNP_AXIS_TYPE_POSE_J1,
        DPNP_AXIS_TYPE_POSE_K1,

        DPNP_AXIS_TYPE_POSE_S2,
        DPNP_AXIS_TYPE_POSE_I2,
        DPNP_AXIS_TYPE_POSE_J2,
        DPNP_AXIS_TYPE_POSE_K2,

        DPNP_AXIS_TYPE_POSE_S3,
        DPNP_AXIS_TYPE_POSE_I3,
        DPNP_AXIS_TYPE_POSE_J3,
        DPNP_AXIS_TYPE_POSE_K3,

        DPNP_AXIS_TYPE_POSE_S4,
        DPNP_AXIS_TYPE_POSE_I4,
        DPNP_AXIS_TYPE_POSE_J4,
        DPNP_AXIS_TYPE_POSE_K4,

        DPNP_AXIS_TYPE_POSE_S5,
        DPNP_AXIS_TYPE_POSE_I5,
        DPNP_AXIS_TYPE_POSE_J5,
        DPNP_AXIS_TYPE_POSE_K5,

        DPNP_AXIS_TYPE_POSE_S6,
        DPNP_AXIS_TYPE_POSE_I6,
        DPNP_AXIS_TYPE_POSE_J6,
        DPNP_AXIS_TYPE_POSE_K6,

        DPNP_AXIS_TYPE_POSE_S7,
        DPNP_AXIS_TYPE_POSE_I7,
        DPNP_AXIS_TYPE_POSE_J7,
        DPNP_AXIS_TYPE_POSE_K7,

        DPNP_AXIS_TYPE_POSE_S8,
        DPNP_AXIS_TYPE_POSE_I8,
        DPNP_AXIS_TYPE_POSE_J8,
        DPNP_AXIS_TYPE_POSE_K8,

        DPNP_AXIS_TYPE_POSE_S9,
        DPNP_AXIS_TYPE_POSE_I9,
        DPNP_AXIS_TYPE_POSE_J9,
        DPNP_AXIS_TYPE_POSE_K9,

        DPNP_AXIS_TYPE_POSE_S10,
        DPNP_AXIS_TYPE_POSE_I10,
        DPNP_AXIS_TYPE_POSE_J10,
        DPNP_AXIS_TYPE_POSE_K10,

        DPNP_AXIS_TYPE_POSE_S11,
        DPNP_AXIS_TYPE_POSE_I11,
        DPNP_AXIS_TYPE_POSE_J11,
        DPNP_AXIS_TYPE_POSE_K11,

        DPNP_AXIS_TYPE_POSE_S12,
        DPNP_AXIS_TYPE_POSE_I12,
        DPNP_AXIS_TYPE_POSE_J12,
        DPNP_AXIS_TYPE_POSE_K12,

        DPNP_AXIS_TYPE_POSE_S13,
        DPNP_AXIS_TYPE_POSE_I13,
        DPNP_AXIS_TYPE_POSE_J13,
        DPNP_AXIS_TYPE_POSE_K13,

        DPNP_AXIS_TYPE_POSE_S14,
        DPNP_AXIS_TYPE_POSE_I14,
        DPNP_AXIS_TYPE_POSE_J14,
        DPNP_AXIS_TYPE_POSE_K14,

        DPNP_AXIS_TYPE_POSE_S15,
        DPNP_AXIS_TYPE_POSE_I15,
        DPNP_AXIS_TYPE_POSE_J15,
        DPNP_AXIS_TYPE_POSE_K15,

        DPNP_AXIS_TYPE_POSE_S16,
        DPNP_AXIS_TYPE_POSE_I16,
        DPNP_AXIS_TYPE_POSE_J16,
        DPNP_AXIS_TYPE_POSE_K16,

        DPNP_AXIS_TYPE_POSE_S17,
        DPNP_AXIS_TYPE_POSE_I17,
        DPNP_AXIS_TYPE_POSE_J17,
        DPNP_AXIS_TYPE_POSE_K17,

        DPNP_AXIS_TYPE_POSE_S18,
        DPNP_AXIS_TYPE_POSE_I18,
        DPNP_AXIS_TYPE_POSE_J18,
        DPNP_AXIS_TYPE_POSE_K18,

        DPNP_AXIS_TYPE_POSE_S19,
        DPNP_AXIS_TYPE_POSE_I19,
        DPNP_AXIS_TYPE_POSE_J19,
        DPNP_AXIS_TYPE_POSE_K19,

        DPNP_AXIS_TYPE_POSITION = 0x100,
        DPNP_AXIS_TYPE_POSITION_X,
        DPNP_AXIS_TYPE_POSITION_Y,
        DPNP_AXIS_TYPE_POSITION_Z,

        DPNP_AXIS_TYPE_POSITION_X0 = DPNP_AXIS_TYPE_POSITION_X,
        DPNP_AXIS_TYPE_POSITION_Y0 = DPNP_AXIS_TYPE_POSITION_Y,
        DPNP_AXIS_TYPE_POSITION_Z0 = DPNP_AXIS_TYPE_POSITION_Z,

        DPNP_AXIS_TYPE_POSITION_X1,
        DPNP_AXIS_TYPE_POSITION_Y1,
        DPNP_AXIS_TYPE_POSITION_Z1,

        DPNP_AXIS_TYPE_POSITION_X2,
        DPNP_AXIS_TYPE_POSITION_Y2,
        DPNP_AXIS_TYPE_POSITION_Z2,

        DPNP_AXIS_TYPE_POSITION_X3,
        DPNP_AXIS_TYPE_POSITION_Y3,
        DPNP_AXIS_TYPE_POSITION_Z3,

        DPNP_AXIS_TYPE_POSITION_X4,
        DPNP_AXIS_TYPE_POSITION_Y4,
        DPNP_AXIS_TYPE_POSITION_Z4,

        DPNP_AXIS_TYPE_POSITION_X5,
        DPNP_AXIS_TYPE_POSITION_Y5,
        DPNP_AXIS_TYPE_POSITION_Z5,

        DPNP_AXIS_TYPE_POSITION_X6,
        DPNP_AXIS_TYPE_POSITION_Y6,
        DPNP_AXIS_TYPE_POSITION_Z6,

        DPNP_AXIS_TYPE_POSITION_X7,
        DPNP_AXIS_TYPE_POSITION_Y7,
        DPNP_AXIS_TYPE_POSITION_Z7,

        DPNP_AXIS_TYPE_POSITION_X8,
        DPNP_AXIS_TYPE_POSITION_Y8,
        DPNP_AXIS_TYPE_POSITION_Z8,

        DPNP_AXIS_TYPE_POSITION_X9,
        DPNP_AXIS_TYPE_POSITION_Y9,
        DPNP_AXIS_TYPE_POSITION_Z9,

        DPNP_AXIS_TYPE_POSITION_X10,
        DPNP_AXIS_TYPE_POSITION_Y10,
        DPNP_AXIS_TYPE_POSITION_Z10,

        DPNP_AXIS_TYPE_POSITION_X11,
        DPNP_AXIS_TYPE_POSITION_Y11,
        DPNP_AXIS_TYPE_POSITION_Z11,

        DPNP_AXIS_TYPE_POSITION_X12,
        DPNP_AXIS_TYPE_POSITION_Y12,
        DPNP_AXIS_TYPE_POSITION_Z12,

        DPNP_AXIS_TYPE_POSITION_X13,
        DPNP_AXIS_TYPE_POSITION_Y13,
        DPNP_AXIS_TYPE_POSITION_Z13,

        DPNP_AXIS_TYPE_POSITION_X14,
        DPNP_AXIS_TYPE_POSITION_Y14,
        DPNP_AXIS_TYPE_POSITION_Z14,

        DPNP_AXIS_TYPE_POSITION_X15,
        DPNP_AXIS_TYPE_POSITION_Y15,
        DPNP_AXIS_TYPE_POSITION_Z15,

        DPNP_AXIS_TYPE_POSITION_X16,
        DPNP_AXIS_TYPE_POSITION_Y16,
        DPNP_AXIS_TYPE_POSITION_Z16,

        DPNP_AXIS_TYPE_POSITION_X17,
        DPNP_AXIS_TYPE_POSITION_Y17,
        DPNP_AXIS_TYPE_POSITION_Z17,

        DPNP_AXIS_TYPE_POSITION_X18,
        DPNP_AXIS_TYPE_POSITION_Y18,
        DPNP_AXIS_TYPE_POSITION_Z18,

        DPNP_AXIS_TYPE_POSITION_X19,
        DPNP_AXIS_TYPE_POSITION_Y19,
        DPNP_AXIS_TYPE_POSITION_Z19,

        DPNP_AXIS_TYPE_AXIS = 0x200,
        DPNP_AXIS_TYPE_AXIS_X,
        DPNP_AXIS_TYPE_AXIS_Y,
        DPNP_AXIS_TYPE_AXIS_Z,
        DPNP_AXIS_TYPE_AXIS_W,

        DPNP_AXIS_TYPE_AXIS_X0 = DPNP_AXIS_TYPE_AXIS_X,
        DPNP_AXIS_TYPE_AXIS_Y0 = DPNP_AXIS_TYPE_AXIS_Y,
        DPNP_AXIS_TYPE_AXIS_Z0 = DPNP_AXIS_TYPE_AXIS_Z,
        DPNP_AXIS_TYPE_AXIS_W0 = DPNP_AXIS_TYPE_AXIS_W,

        DPNP_AXIS_TYPE_AXIS_X1,
        DPNP_AXIS_TYPE_AXIS_Y1,
        DPNP_AXIS_TYPE_AXIS_Z1,
        DPNP_AXIS_TYPE_AXIS_W1,

        DPNP_AXIS_TYPE_AXIS_X2,
        DPNP_AXIS_TYPE_AXIS_Y2,
        DPNP_AXIS_TYPE_AXIS_Z2,
        DPNP_AXIS_TYPE_AXIS_W2,

        DPNP_AXIS_TYPE_AXIS_X3,
        DPNP_AXIS_TYPE_AXIS_Y3,
        DPNP_AXIS_TYPE_AXIS_Z3,
        DPNP_AXIS_TYPE_AXIS_W3,

        DPNP_AXIS_TYPE_AXIS_X4,
        DPNP_AXIS_TYPE_AXIS_Y4,
        DPNP_AXIS_TYPE_AXIS_Z4,
        DPNP_AXIS_TYPE_AXIS_W4,

        DPNP_AXIS_TYPE_AXIS_X5,
        DPNP_AXIS_TYPE_AXIS_Y5,
        DPNP_AXIS_TYPE_AXIS_Z5,
        DPNP_AXIS_TYPE_AXIS_W5,

        DPNP_AXIS_TYPE_AXIS_X6,
        DPNP_AXIS_TYPE_AXIS_Y6,
        DPNP_AXIS_TYPE_AXIS_Z6,
        DPNP_AXIS_TYPE_AXIS_W6,

        DPNP_AXIS_TYPE_AXIS_X7,
        DPNP_AXIS_TYPE_AXIS_Y7,
        DPNP_AXIS_TYPE_AXIS_Z7,
        DPNP_AXIS_TYPE_AXIS_W7,

        DPNP_AXIS_TYPE_AXIS_X8,
        DPNP_AXIS_TYPE_AXIS_Y8,
        DPNP_AXIS_TYPE_AXIS_Z8,
        DPNP_AXIS_TYPE_AXIS_W8,

        DPNP_AXIS_TYPE_AXIS_X9,
        DPNP_AXIS_TYPE_AXIS_Y9,
        DPNP_AXIS_TYPE_AXIS_Z9,
        DPNP_AXIS_TYPE_AXIS_W9,

        DPNP_AXIS_TYPE_AXIS_X10,
        DPNP_AXIS_TYPE_AXIS_Y10,
        DPNP_AXIS_TYPE_AXIS_Z10,
        DPNP_AXIS_TYPE_AXIS_W10,

        DPNP_AXIS_TYPE_AXIS_X11,
        DPNP_AXIS_TYPE_AXIS_Y11,
        DPNP_AXIS_TYPE_AXIS_Z11,
        DPNP_AXIS_TYPE_AXIS_W11,

        DPNP_AXIS_TYPE_AXIS_X12,
        DPNP_AXIS_TYPE_AXIS_Y12,
        DPNP_AXIS_TYPE_AXIS_Z12,
        DPNP_AXIS_TYPE_AXIS_W12,

        DPNP_AXIS_TYPE_AXIS_X13,
        DPNP_AXIS_TYPE_AXIS_Y13,
        DPNP_AXIS_TYPE_AXIS_Z13,
        DPNP_AXIS_TYPE_AXIS_W13,

        DPNP_AXIS_TYPE_AXIS_X14,
        DPNP_AXIS_TYPE_AXIS_Y14,
        DPNP_AXIS_TYPE_AXIS_Z14,
        DPNP_AXIS_TYPE_AXIS_W14,

        DPNP_AXIS_TYPE_AXIS_X15,
        DPNP_AXIS_TYPE_AXIS_Y15,
        DPNP_AXIS_TYPE_AXIS_Z15,
        DPNP_AXIS_TYPE_AXIS_W15,

        DPNP_AXIS_TYPE_AXIS_X16,
        DPNP_AXIS_TYPE_AXIS_Y16,
        DPNP_AXIS_TYPE_AXIS_Z16,
        DPNP_AXIS_TYPE_AXIS_W16,

        DPNP_AXIS_TYPE_AXIS_X17,
        DPNP_AXIS_TYPE_AXIS_Y17,
        DPNP_AXIS_TYPE_AXIS_Z17,
        DPNP_AXIS_TYPE_AXIS_W17,

        DPNP_AXIS_TYPE_AXIS_X18,
        DPNP_AXIS_TYPE_AXIS_Y18,
        DPNP_AXIS_TYPE_AXIS_Z18,
        DPNP_AXIS_TYPE_AXIS_W18,

        DPNP_AXIS_TYPE_AXIS_X19,
        DPNP_AXIS_TYPE_AXIS_Y19,
        DPNP_AXIS_TYPE_AXIS_Z19,
        DPNP_AXIS_TYPE_AXIS_W19,

        DPNP_AXIS_TYPE_HAT_X,
        DPNP_AXIS_TYPE_HAT_Y,
        DPNP_AXIS_TYPE_HAT_Z,
        DPNP_AXIS_TYPE_HAT_W,

        DPNP_AXIS_TYPE_HAT_X0 = DPNP_AXIS_TYPE_HAT_X,
        DPNP_AXIS_TYPE_HAT_Y0 = DPNP_AXIS_TYPE_HAT_Y,
        DPNP_AXIS_TYPE_HAT_Z0 = DPNP_AXIS_TYPE_HAT_Z,
        DPNP_AXIS_TYPE_HAT_W0 = DPNP_AXIS_TYPE_HAT_W,

        DPNP_AXIS_TYPE_HAT_X1,
        DPNP_AXIS_TYPE_HAT_Y1,
        DPNP_AXIS_TYPE_HAT_Z1,
        DPNP_AXIS_TYPE_HAT_W1,

        DPNP_AXIS_TYPE_HAT_X2,
        DPNP_AXIS_TYPE_HAT_Y2,
        DPNP_AXIS_TYPE_HAT_Z2,
        DPNP_AXIS_TYPE_HAT_W2,

        DPNP_AXIS_TYPE_HAT_X3,
        DPNP_AXIS_TYPE_HAT_Y3,
        DPNP_AXIS_TYPE_HAT_Z3,
        DPNP_AXIS_TYPE_HAT_W3,

        DPNP_AXIS_TYPE_HAT_X4,
        DPNP_AXIS_TYPE_HAT_Y4,
        DPNP_AXIS_TYPE_HAT_Z4,
        DPNP_AXIS_TYPE_HAT_W4,

        DPNP_AXIS_TYPE_HAT_X5,
        DPNP_AXIS_TYPE_HAT_Y5,
        DPNP_AXIS_TYPE_HAT_Z5,
        DPNP_AXIS_TYPE_HAT_W5,

        DPNP_AXIS_TYPE_HAT_X6,
        DPNP_AXIS_TYPE_HAT_Y6,
        DPNP_AXIS_TYPE_HAT_Z6,
        DPNP_AXIS_TYPE_HAT_W6,

        DPNP_AXIS_TYPE_HAT_X7,
        DPNP_AXIS_TYPE_HAT_Y7,
        DPNP_AXIS_TYPE_HAT_Z7,
        DPNP_AXIS_TYPE_HAT_W7,

        DPNP_AXIS_TYPE_HAT_X8,
        DPNP_AXIS_TYPE_HAT_Y8,
        DPNP_AXIS_TYPE_HAT_Z8,
        DPNP_AXIS_TYPE_HAT_W8,

        DPNP_AXIS_TYPE_HAT_X9,
        DPNP_AXIS_TYPE_HAT_Y9,
        DPNP_AXIS_TYPE_HAT_Z9,
        DPNP_AXIS_TYPE_HAT_W9,

        DPNP_AXIS_TYPE_HAT_X10,
        DPNP_AXIS_TYPE_HAT_Y10,
        DPNP_AXIS_TYPE_HAT_Z10,
        DPNP_AXIS_TYPE_HAT_W10,

        DPNP_AXIS_TYPE_HAT_X11,
        DPNP_AXIS_TYPE_HAT_Y11,
        DPNP_AXIS_TYPE_HAT_Z11,
        DPNP_AXIS_TYPE_HAT_W11,

        DPNP_AXIS_TYPE_HAT_X12,
        DPNP_AXIS_TYPE_HAT_Y12,
        DPNP_AXIS_TYPE_HAT_Z12,
        DPNP_AXIS_TYPE_HAT_W12,

        DPNP_AXIS_TYPE_HAT_X13,
        DPNP_AXIS_TYPE_HAT_Y13,
        DPNP_AXIS_TYPE_HAT_Z13,
        DPNP_AXIS_TYPE_HAT_W13,

        DPNP_AXIS_TYPE_HAT_X14,
        DPNP_AXIS_TYPE_HAT_Y14,
        DPNP_AXIS_TYPE_HAT_Z14,
        DPNP_AXIS_TYPE_HAT_W14,

        DPNP_AXIS_TYPE_HAT_X15,
        DPNP_AXIS_TYPE_HAT_Y15,
        DPNP_AXIS_TYPE_HAT_Z15,
        DPNP_AXIS_TYPE_HAT_W15,

        DPNP_AXIS_TYPE_HAT_X16,
        DPNP_AXIS_TYPE_HAT_Y16,
        DPNP_AXIS_TYPE_HAT_Z16,
        DPNP_AXIS_TYPE_HAT_W16,

        DPNP_AXIS_TYPE_HAT_X17,
        DPNP_AXIS_TYPE_HAT_Y17,
        DPNP_AXIS_TYPE_HAT_Z17,
        DPNP_AXIS_TYPE_HAT_W17,

        DPNP_AXIS_TYPE_HAT_X18,
        DPNP_AXIS_TYPE_HAT_Y18,
        DPNP_AXIS_TYPE_HAT_Z18,
        DPNP_AXIS_TYPE_HAT_W18,

        DPNP_AXIS_TYPE_HAT_X19,
        DPNP_AXIS_TYPE_HAT_Y19,
        DPNP_AXIS_TYPE_HAT_Z19,
        DPNP_AXIS_TYPE_HAT_W19,

        DPNP_AXIS_TYPE_BUTTON = 0x300,
        DPNP_AXIS_TYPE_BUTTON_0,
        DPNP_AXIS_TYPE_BUTTON_1,
        DPNP_AXIS_TYPE_BUTTON_2,
        DPNP_AXIS_TYPE_BUTTON_3,
        DPNP_AXIS_TYPE_BUTTON_4,
        DPNP_AXIS_TYPE_BUTTON_5,
        DPNP_AXIS_TYPE_BUTTON_6,
        DPNP_AXIS_TYPE_BUTTON_7,
        DPNP_AXIS_TYPE_BUTTON_8,
        DPNP_AXIS_TYPE_BUTTON_9,

        DPNP_AXIS_TYPE_BUTTON_A,
        DPNP_AXIS_TYPE_BUTTON_B,
        DPNP_AXIS_TYPE_BUTTON_C,
        DPNP_AXIS_TYPE_BUTTON_D,
        DPNP_AXIS_TYPE_BUTTON_E,
        DPNP_AXIS_TYPE_BUTTON_F,
        DPNP_AXIS_TYPE_BUTTON_G,
        DPNP_AXIS_TYPE_BUTTON_H,
        DPNP_AXIS_TYPE_BUTTON_I,
        DPNP_AXIS_TYPE_BUTTON_J,
        DPNP_AXIS_TYPE_BUTTON_K,
        DPNP_AXIS_TYPE_BUTTON_L,
        DPNP_AXIS_TYPE_BUTTON_M,
        DPNP_AXIS_TYPE_BUTTON_N,
        DPNP_AXIS_TYPE_BUTTON_O,
        DPNP_AXIS_TYPE_BUTTON_P,
        DPNP_AXIS_TYPE_BUTTON_Q,
        DPNP_AXIS_TYPE_BUTTON_R,
        DPNP_AXIS_TYPE_BUTTON_S,
        DPNP_AXIS_TYPE_BUTTON_T,
        DPNP_AXIS_TYPE_BUTTON_U,
        DPNP_AXIS_TYPE_BUTTON_V,
        DPNP_AXIS_TYPE_BUTTON_W,
        DPNP_AXIS_TYPE_BUTTON_X,
        DPNP_AXIS_TYPE_BUTTON_Y,
        DPNP_AXIS_TYPE_BUTTON_Z,

        DPNP_AXIS_TYPE_BUTTON_F1,
        DPNP_AXIS_TYPE_BUTTON_F2,
        DPNP_AXIS_TYPE_BUTTON_F3,
        DPNP_AXIS_TYPE_BUTTON_F4,
        DPNP_AXIS_TYPE_BUTTON_F5,
        DPNP_AXIS_TYPE_BUTTON_F6,
        DPNP_AXIS_TYPE_BUTTON_F7,
        DPNP_AXIS_TYPE_BUTTON_F8,
        DPNP_AXIS_TYPE_BUTTON_F9,
        DPNP_AXIS_TYPE_BUTTON_F10,
        DPNP_AXIS_TYPE_BUTTON_F11,
        DPNP_AXIS_TYPE_BUTTON_F12,
        DPNP_AXIS_TYPE_BUTTON_F13,
        DPNP_AXIS_TYPE_BUTTON_F14,
        DPNP_AXIS_TYPE_BUTTON_F15,

        DPNP_AXIS_TYPE_BUTTON_BACKSPACE,
        DPNP_AXIS_TYPE_BUTTON_TAB,
        DPNP_AXIS_TYPE_BUTTON_CLEAR,
        DPNP_AXIS_TYPE_BUTTON_RETURN,
        DPNP_AXIS_TYPE_BUTTON_PAUSE,
        DPNP_AXIS_TYPE_BUTTON_ESCAPE,
        DPNP_AXIS_TYPE_BUTTON_SPACE,

        DPNP_AXIS_TYPE_BUTTON_KEYPAD0,
        DPNP_AXIS_TYPE_BUTTON_KEYPAD1,
        DPNP_AXIS_TYPE_BUTTON_KEYPAD2,
        DPNP_AXIS_TYPE_BUTTON_KEYPAD3,
        DPNP_AXIS_TYPE_BUTTON_KEYPAD4,
        DPNP_AXIS_TYPE_BUTTON_KEYPAD5,
        DPNP_AXIS_TYPE_BUTTON_KEYPAD6,
        DPNP_AXIS_TYPE_BUTTON_KEYPAD7,
        DPNP_AXIS_TYPE_BUTTON_KEYPAD8,
        DPNP_AXIS_TYPE_BUTTON_KEYPAD9,

        DPNP_AXIS_TYPE_BUTTON_KEYPAD_PERIOD, // del
        DPNP_AXIS_TYPE_BUTTON_KEYPAD_DIVIDE, // /
        DPNP_AXIS_TYPE_BUTTON_KEYPAD_MULTIPLY, // *
        DPNP_AXIS_TYPE_BUTTON_KEYPAD_MINUS, // -
        DPNP_AXIS_TYPE_BUTTON_KEYPAD_PLUS, // +
        DPNP_AXIS_TYPE_BUTTON_KEYPAD_ENTER,
        DPNP_AXIS_TYPE_BUTTON_KEYPAD_EQUALS, // =

        DPNP_AXIS_TYPE_BUTTON_UP,
        DPNP_AXIS_TYPE_BUTTON_DOWN,
        DPNP_AXIS_TYPE_BUTTON_RIGHT,
        DPNP_AXIS_TYPE_BUTTON_LEFT,

        DPNP_AXIS_TYPE_BUTTON_INSERT,
        DPNP_AXIS_TYPE_BUTTON_DELETE,
        DPNP_AXIS_TYPE_BUTTON_HOME,
        DPNP_AXIS_TYPE_BUTTON_END,
        DPNP_AXIS_TYPE_BUTTON_PAGE_UP,
        DPNP_AXIS_TYPE_BUTTON_PAGE_DOWN,

        DPNP_AXIS_TYPE_BUTTON_PRINT,
        DPNP_AXIS_TYPE_BUTTON_SYS_REQ,
        DPNP_AXIS_TYPE_BUTTON_BREAK,

        DPNP_AXIS_TYPE_BUTTON_EXCLAIM, // !
        DPNP_AXIS_TYPE_BUTTON_DOUBLE_QUOTE, // "
        DPNP_AXIS_TYPE_BUTTON_HASH, // #
        DPNP_AXIS_TYPE_BUTTON_DOLLAR, // $
        DPNP_AXIS_TYPE_BUTTON_AMPERSAND, // &
        DPNP_AXIS_TYPE_BUTTON_QUOTE, //'
        DPNP_AXIS_TYPE_BUTTON_LEFT_PAREN, // (
        DPNP_AXIS_TYPE_BUTTON_RIGHT_PAREN, // )
        DPNP_AXIS_TYPE_BUTTON_ASTERISK, // *
        DPNP_AXIS_TYPE_BUTTON_PLUS, // +
        DPNP_AXIS_TYPE_BUTTON_COMMA, // ,
        DPNP_AXIS_TYPE_BUTTON_MINUS, // -
        DPNP_AXIS_TYPE_BUTTON_PERIOD, // .
        DPNP_AXIS_TYPE_BUTTON_SLASH, // /
        DPNP_AXIS_TYPE_BUTTON_COLON, // :
        DPNP_AXIS_TYPE_BUTTON_SEMICOLON, // ;
        DPNP_AXIS_TYPE_BUTTON_LESS, // <
        DPNP_AXIS_TYPE_BUTTON_EQUALS, // =
        DPNP_AXIS_TYPE_BUTTON_GREATER, // >
        DPNP_AXIS_TYPE_BUTTON_QUESTION, // ?
        DPNP_AXIS_TYPE_BUTTON_AT, // @
        DPNP_AXIS_TYPE_BUTTON_LEFT_BRACKET, // [
        DPNP_AXIS_TYPE_BUTTON_RIGHT_BRACKET, // [
        DPNP_AXIS_TYPE_BUTTON_CARET, // ^
        DPNP_AXIS_TYPE_BUTTON_UNDERSCORE, // _
        DPNP_AXIS_TYPE_BUTTON_BACKQUOTE, // `

        DPNP_AXIS_TYPE_BUTTON_NUMLOCK,
        DPNP_AXIS_TYPE_BUTTON_CAPSLOCK,
        DPNP_AXIS_TYPE_BUTTON_SCROLLLOCK,
        DPNP_AXIS_TYPE_BUTTON_RIGHT_SHIFT,
        DPNP_AXIS_TYPE_BUTTON_LEFT_SHIFT,
        DPNP_AXIS_TYPE_BUTTON_RIGHT_CONTROL,
        DPNP_AXIS_TYPE_BUTTON_LEFT_CONTROL,
        DPNP_AXIS_TYPE_BUTTON_RIGHT_ALT,
        DPNP_AXIS_TYPE_BUTTON_LEFT_ALT,

        DPNP_AXIS_TYPE_BUTTON_LEFT_COMMAND,
        DPNP_AXIS_TYPE_BUTTON_LEFT_APPLE,
        DPNP_AXIS_TYPE_BUTTON_LEFT_WINDOWS,
        DPNP_AXIS_TYPE_BUTTON_RIGHT_COMMAND,
        DPNP_AXIS_TYPE_BUTTON_RIGHT_APPLE,
        DPNP_AXIS_TYPE_BUTTON_RIGHT_WINDOWS,

        DPNP_AXIS_TYPE_BUTTON_ALT_GR,
        DPNP_AXIS_TYPE_BUTTON_HELP,
        DPNP_AXIS_TYPE_BUTTON_MENU,

        DPNP_AXIS_TYPE_BUTTON_MOUSE0,
        DPNP_AXIS_TYPE_BUTTON_MOUSE1,
        DPNP_AXIS_TYPE_BUTTON_MOUSE2,
        DPNP_AXIS_TYPE_BUTTON_MOUSE3,
        DPNP_AXIS_TYPE_BUTTON_MOUSE4,
        DPNP_AXIS_TYPE_BUTTON_MOUSE5,
        DPNP_AXIS_TYPE_BUTTON_MOUSE6,
        DPNP_AXIS_TYPE_BUTTON_MOUSE7,

        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_0,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_1,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_2,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_3,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_4,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_5,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_6,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_7,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_8,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_9,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_10,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_11,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_12,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_13,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_14,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_15,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_16,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_17,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_18,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_19,

        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A0 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_0,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A1 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_1,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A2 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_2,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A3 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_3,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A4 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_4,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A5 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_5,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A6 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_6,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A7 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_7,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A8 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_8,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A9 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_9,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A10 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_10,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A11 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_11,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A12 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_12,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A13 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_13,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A14 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_14,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A15 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_15,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A16 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_16,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A17 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_17,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A18 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_18,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_A19 = DPNP_AXIS_TYPE_BUTTON_JOYSTICK_19,

        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B0,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B1,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B2,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B3,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B4,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B5,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B6,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B7,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B8,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B9,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B10,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B11,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B12,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B13,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B14,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B15,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B16,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B17,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B18,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_B19,

        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C0,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C1,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C2,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C3,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C4,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C5,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C6,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C7,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C8,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C9,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C10,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C11,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C12,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C13,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C14,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C15,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C16,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C17,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C18,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_C19,

        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D0,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D1,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D2,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D3,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D4,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D5,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D6,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D7,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D8,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D9,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D10,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D11,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D12,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D13,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D14,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D15,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D16,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D17,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D18,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_D19,

        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E0,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E1,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E2,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E3,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E4,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E5,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E6,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E7,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E8,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E9,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E10,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E11,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E12,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E13,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E14,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E15,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E16,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E17,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E18,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_E19,

        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F0,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F1,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F2,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F3,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F4,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F5,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F6,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F7,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F8,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F9,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F10,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F11,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F12,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F13,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F14,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F15,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F16,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F17,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F18,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_F19,

        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G0,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G1,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G2,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G3,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G4,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G5,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G6,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G7,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G8,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G9,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G10,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G11,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G12,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G13,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G14,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G15,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G16,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G17,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G18,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_G19,

        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H0,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H1,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H2,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H3,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H4,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H5,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H6,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H7,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H8,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H9,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H10,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H11,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H12,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H13,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H14,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H15,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H16,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H17,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H18,
        DPNP_AXIS_TYPE_BUTTON_JOYSTICK_H19,
        //DPNP_AXIS_TYPE_BUTTON_10,
        //DPNP_AXIS_TYPE_BUTTON_11,
        //DPNP_AXIS_TYPE_BUTTON_12,
        //DPNP_AXIS_TYPE_BUTTON_13,
        //DPNP_AXIS_TYPE_BUTTON_14,
        //DPNP_AXIS_TYPE_BUTTON_15,

        DPNP_AXIS_TYPE_FEEDBACK = 0x600,

        DPNP_AXIS_TYPE_FEEDBACK_ = 0x600,
        DPNP_AXIS_TYPE_FEEDBACK_32 = 0x620,

        DPNP_AXIS_TYPE_BUTTON_PPGUN_1 = DPNP_AXIS_TYPE_BUTTON_U,//U
            DPNP_AXIS_TYPE_BUTTON_PPGUN_2 = DPNP_AXIS_TYPE_BUTTON_I,//I
            DPNP_AXIS_TYPE_BUTTON_PPGUN_3 = DPNP_AXIS_TYPE_BUTTON_O,//O
            DPNP_AXIS_TYPE_BUTTON_PPGUN_4 = DPNP_AXIS_TYPE_BUTTON_H,//H
            DPNP_AXIS_TYPE_BUTTON_PPGUN_5 = DPNP_AXIS_TYPE_BUTTON_J,//J
            DPNP_AXIS_TYPE_BUTTON_PPGUN_6 = DPNP_AXIS_TYPE_BUTTON_M,//M
            DPNP_AXIS_TYPE_BUTTON_PPGUN_7 = DPNP_AXIS_TYPE_BUTTON_MOUSE1,//right mouse down
            DPNP_AXIS_TYPE_BUTTON_PPGUN_8 = DPNP_AXIS_TYPE_BUTTON_N,//N
            DPNP_AXIS_TYPE_BUTTON_PPGUN_Fire = DPNP_AXIS_TYPE_BUTTON_MOUSE0,//left mouse down
            DPNP_AXIS_TYPE_BUTTON_PPGUN_EN = DPNP_AXIS_TYPE_BUTTON_KEYPAD_ENTER,//Enter
            DPNP_AXIS_TYPE_BUTTON_PPGUN_W = DPNP_AXIS_TYPE_BUTTON_W,//W
            DPNP_AXIS_TYPE_BUTTON_PPGUN_S = DPNP_AXIS_TYPE_BUTTON_S,//S
            DPNP_AXIS_TYPE_BUTTON_PPGUN_A = DPNP_AXIS_TYPE_BUTTON_A,//A
            DPNP_AXIS_TYPE_BUTTON_PPGUN_D = DPNP_AXIS_TYPE_BUTTON_D,//D
            DPNP_AXIS_TYPE_BUTTON_PPGUN_Space = DPNP_AXIS_TYPE_BUTTON_SPACE,//Space


        DPNP_AXIS_TYPE_JOYSTICK_X,
        DPNP_AXIS_TYPE_JOYSTICK_Y,
        DPNP_AXIS_TYPE_JOYSTICK_Z,

        DPNP_AXIS_TYPE_JOYSTICK_RX,
        DPNP_AXIS_TYPE_JOYSTICK_RY,
        DPNP_AXIS_TYPE_JOYSTICK_RZ,

        DPNP_AXIS_TYPE_JOYSTICK_POV
    };*/

    [StructLayout(LayoutKind.Sequential)]
    public struct DpnnDevicePose
    {
        public dpnQuarterion qRotation;
        public dpnVector3 vecAngularVelocity;
        public dpnVector3 vecAngularAcceleration;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DpnnDevicePosition
    {
        public dpnVector3 vecPosition;
        public dpnVector3 vecVelocity;
        public dpnVector3 vecAcceleration;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DpnnDeviceAxis
    {
        public float T;
        public float B;
        public float X;
        public float Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DpnnDevicePosePosition
    {
        public DpnnDevicePose pose;      //pose.Length = 10
        public DpnnDevicePosition position;     //position.Length = 9
        public int is_valid;
        public int is_connected;
        public double update_time;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct DpnnControllerState
    {
        public uint packet_number;
        public float touch_pad_analog_x;
        public float touch_pad_analog_y;
        public float trigger_analog;
        public ulong button_pressed_flags;
        public ulong button_touched_flags;
        public uint power_button_mode;
        public int is_valid;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct DpnnControllerStateRecord
    {
        public DpnnDevicePosePosition controllerPosePosition;
        public DpnnControllerState controllerState;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct DpnnControllerStateRecordOriginal
    {
        public double time;
        public int connect_state;
        public DpnnDevicePose pose;      //pose.Length = 10
        public DpnnDevicePosition position;     //position.Length = 9
        public int button_mask;
        public DpnnDeviceAxis axis;

        public DpnnControllerStateRecordOriginal(Peripheralstatus status)
        {
            time = status.time_state[1][0];
            connect_state = status.device_status;
            pose.qRotation = new dpnQuarterion(status.pose_state[0][0], status.pose_state[0][1], status.pose_state[0][2], status.pose_state[0][3]);
            pose.vecAngularVelocity = new dpnVector3(status.pose_state[0][4], status.pose_state[0][5], status.pose_state[0][6]);
            pose.vecAngularAcceleration = new dpnVector3(status.pose_state[0][7], status.pose_state[0][8], status.pose_state[0][9]);
            position.vecPosition = new dpnVector3(status.position_state[0][0], status.position_state[0][1], status.position_state[0][2]);
            position.vecVelocity = new dpnVector3(status.position_state[0][3], status.position_state[0][4], status.position_state[0][5]);
            position.vecAcceleration = new dpnVector3(status.position_state[0][6], status.position_state[0][7], status.position_state[0][8]);
            button_mask = status.button_state[(int)DPNP_POLARIS_BUTTONS.DPNP_POLARIS_BUTTONS_YB][0];
            axis.T = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_T][0];
            axis.B = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_B][0];
            axis.X = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_X][0];
            axis.Y = status.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_Y][0];
        }
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct VRControllerAxis_t
    {
        public float x;
        public float y;
    }
    [StructLayout(LayoutKind.Sequential)]
    public struct VRControllerState_t
    {
        public uint unPacketNum;
        public ulong ulButtonPressed;
        public ulong ulButtonTouched;
        public VRControllerAxis_t rAxis0; //VRControllerAxis_t[5]
        public VRControllerAxis_t rAxis1;
        public VRControllerAxis_t rAxis2;
        public VRControllerAxis_t rAxis3;
        public VRControllerAxis_t rAxis4;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TrackedDevicePose_t
    {
        public dpnVector3 vVelocity;
        public dpnVector3 vAngularVelocity;
        [MarshalAs(UnmanagedType.I1)]
        public bool bPoseIsValid;
        [MarshalAs(UnmanagedType.I1)]
        public bool bDeviceIsConnected;
    }
}

