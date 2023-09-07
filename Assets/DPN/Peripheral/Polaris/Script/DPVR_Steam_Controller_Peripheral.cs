using UnityEngine;
using System.Collections;

namespace dpn
{
public class DPVR_Steam_Controller_Peripheral {

    public DpnPeripheral peripheral;
    public VRControllerState_t state, prevState;
    public TrackedDevicePose_t pose;

    private DpnnControllerStateRecord record = new DpnnControllerStateRecord();
    private DPVRControllerType device_type;

    public DPVR_Steam_Controller_Peripheral(DpnPeripheral _peripheral, DPVRControllerType type)
    {
        peripheral = _peripheral;
        device_type = type;
    }

    private int FrameNum = -1;
    public void DeviceUpdate()
    {
        if (Time.frameCount == FrameNum)
        {
            return;
        }
        else
        {
            FrameNum = Time.frameCount;
        }

        prevState = state;
        DpnnControllerStateRecordOriginal temp = new DpnnControllerStateRecordOriginal(peripheral.peripheralstatus);
        peripheral.DpnupHandleControllerData((int)device_type, ref temp, ref record);
        state.unPacketNum = record.controllerState.packet_number;
        state.ulButtonPressed = record.controllerState.button_pressed_flags;
        state.ulButtonTouched = record.controllerState.button_touched_flags;
        state.rAxis0.x = record.controllerState.touch_pad_analog_x;
        state.rAxis0.y = record.controllerState.touch_pad_analog_y;
        state.rAxis1.x = peripheral.peripheralstatus.axis_state[(int)DPNP_POLARIS_AXES.DPNP_POLARIS_AXIS_T][0] / 1000.0f;
        state.rAxis1.y = 0;

        pose.bDeviceIsConnected = (record.controllerPosePosition.is_connected != 0);
        pose.bPoseIsValid = (record.controllerPosePosition.is_valid != 0);
        pose.vVelocity = record.controllerPosePosition.position.vecVelocity;
        pose.vAngularVelocity = record.controllerPosePosition.pose.vecAngularVelocity;
    }
}
}
