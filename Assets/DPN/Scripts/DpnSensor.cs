/************************************************************************************

Copyright   :   Copyright 2015-2017 DeePoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/

using UnityEngine;
using System.Collections;
using System;
using System.Runtime.InteropServices;

namespace dpn
{
    public class Sensor
    {
        /// <summary>
        /// Gets the pose of the given eye, predicted for the time when the current frame will scan out.
        /// </summary>

        public Pose GetEyePose
            (dpncEYE eye
            , dpnQuarterion pose
            , dpnVector3 position // position 在当前应该已经被转成左手系（Unity）了
            , float interpupillary_distance
            )
        {
            Quaternion rot = pose.ToQuaternion(); // 右手螺旋, 左手系 -> 左手螺旋，左手系（Unity）

            float eye_offset_x = 0.5f * interpupillary_distance;
            eye_offset_x = (eye == dpncEYE.LEFT) ? -eye_offset_x : eye_offset_x;

            float neck_to_eye_height = 0.0f;
            float eye_depth = 0.0f; // PC: head model do in native sdk, Android: for compatible old game, do here as old apps
#if UNITY_ANDROID && !UNITY_EDITOR
            float eye_height = 1.6f;
            float player_height = eye_height * 1.067f; // eye should be at 15/16 of total height.
            neck_to_eye_height = player_height * 0.0625f * DpnManager.worldScale; //neck_to_eye_height should be 1/16 of total height.
            eye_depth = 0.0805f * DpnManager.worldScale;
#endif

            Vector3 neck_model = new Vector3(0.0f, neck_to_eye_height, eye_depth);
            Vector3 pos = rot * (new Vector3(eye_offset_x, 0.0f, 0.0f) + neck_model); // neck-pivot space
            pos -= neck_model; // what it does is converting pos to initially oriented center-eye space, making the initial position of center eye (0,0,0)

            Pose ret = new Pose();
            ret.position = pos + new Vector3(position.x, position.y, position.z);
            ret.orientation = rot;
            return ret;
        }

        /// <summary>
        /// Recenters the head pose.
        /// </summary>
        public void RecenterPose()
        {
			Composer.DpnuResetPose();
        }

        /// <summary>
        /// Gets the head pose.
        /// </summary>
        /// <param name="predictionTime">The prediction time.</param>
        /// <returns></returns>
        public Pose GetHeadPose(double predictionTime = 0d)
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
            dpnQuarterion pose = Composer.DpnuGetPredictedPose(predictionTime);
            dpnVector3 position = Composer.DpnuGetPredictedPosition(predictionTime);
            #else
            dpnQuarterion pose = Composer.DpnuGetPose();
            dpnVector3 position = Composer.DpnuGetPosition();
            #endif
            return new Pose
            {
                position = new Vector3(position.x, position.y, position.z),
                orientation = pose.ToQuaternion(),
            };
        }

        public Vector3 acceleration
        {
            get
            {
                dpnSensorData SensorData = new dpnSensorData();
                Composer.DpnuGetSensorData(ref SensorData);
                return new Vector3(SensorData.linear_acceleration.x, SensorData.linear_acceleration.y, SensorData.linear_acceleration.z);
            }
        }

        public Vector3 angularVelocity
        {
            get
            {
                dpnSensorData SensorData = new dpnSensorData();
                Composer.DpnuGetSensorData(ref SensorData);
                return new Vector3(SensorData.angular_velocity.x, SensorData.angular_velocity.y, SensorData.angular_velocity.z);
            }
        }

        public float GetYaw()
        {
            string property_name = "get_yaw";
            IntPtr tempPtr = Marshal.StringToHGlobalAnsi(property_name);
            float ret = Composer.DpnuGetFloatValue(tempPtr, 0.0f);
            Marshal.FreeHGlobal(tempPtr);
            return ret;
        }

        /// <summary>
        /// Sets the yaw.
        /// </summary>
        /// <param name="angle">The angle.</param>
        public void SetYaw(float angle)
        {
            string property_name = "set_yaw";
            IntPtr tempPtr = Marshal.StringToHGlobalAnsi(property_name);
            Composer.DpnuSetFloatValue(tempPtr, angle);
        }
    }
}