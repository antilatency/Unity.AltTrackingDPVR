/************************************************************************************

Copyright   :   Copyright 2015-2017 DeePoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/

using UnityEngine;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace dpn
{
	/// <summary>
	/// Contains the valid range of texture depth values usable with Unity render textures.
	/// </summary>
	public enum TEXTURE_DEPTH
	{
		_0  =  0,
		_16 = 16,
		_24 = 24,
	}
	
	/// <summary>
	/// Matches the events in the native plugin.
	/// </summary>
	public enum PLUGIN_EVENT_TYPE
	{
		// PC
		BeginFrame = 0,
		EndFrame = 1,
		Initialize = 2,
		Destroy = 3,
		
		// Android
		InitRenderThread = 0,
		Pause = 1,
		Resume = 2,
		LeftEyeEndFrame = 3,
		RightEyeEndFrame = 4,
		TimeWarp = 5,
		PlatformUI = 6,
		PlatformUIConfirmQuit = 7,
		ResetVrModeParms = 8,
		PlatformUITutorial = 9,
		ShutdownRenderThread = 10,
	}

	public enum dpncEYE
	{
		LEFT = 0 ,
		RIGHT = 1 ,
        BOTH = 2,
		BOTH_LEFT = 2 ,
		BOTH_RIGHT = 3 ,
		NUM = 2 ,
	}

	public enum dpncOutputMode
	{
		NONE = -1,
		LEFT_EYE = 0,
		RIGHT_EYE = 1,
		BOTH_NORMAL = 2,
		BOTH_DISTORTION = 3,
	}

	public enum dpncTwType
	{
		NONE = 0,
		DISTORTION = 1,
		// It is not recommanded to use the following two options for multilayer.
		// If you want to use these, please leave transparent blank at the edge.
		TW= 2,
		TW_DISTORTION= 3,
	}

	/// <summary>
	/// An affine transformation built from a Unity position and orientation.
	/// </summary>
	public struct Pose
	{
		/// <summary>
		/// The position.
		/// </summary>
		public Vector3 position;
		
		/// <summary>
		/// The orientation.
		/// </summary>
		public Quaternion orientation;
	}
	
	public enum dpnpGENDER
	{
		SECRET ,
		MALE ,
		FEMALE ,
		NUM ,
	};

	[StructLayout(LayoutKind.Sequential)]
	public struct dpnVector2
	{
		public float x;
		public float y;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct dpnProfile
	{
		public float interpupillary_distance;
		public dpnVector2 fov;
		public float neck_to_eye_height;
		public float eye_depth;
		public float player_height;
		public dpnpGENDER gender;
	};
	public enum RENDER_EVENT
	{
		Compose = 0,
		Resume = 1,
		Pause = 2,
		LEFT_EYE = 3,
		RIGHT_EYE = 4,
		Init = 5,
		Deinit = 6,
		Postnontransparent = 7,
		Posttransparent = 8,
        UpdatePose = 9,
	}
	
	public enum dpncAPPLY
	{
		dpncAPPLY_NONE ,
		dpncAPPLY_DISTORTION ,
		dpncAPPLY_TIMEWARP ,
		dpncAPPLY_DISTORTION_TIMEWARP ,
		dpncAPPLY_NUM ,
	}

	/// <summary>
	/// Selects a human eye.
	/// </summary>
	public enum dpnhMESSAGE
	{
		dpnhMESSAGE_OK ,
		dpnhMESSAGE_KEY ,
		dpnhMESSAGE_MOUSE_BUTTON ,
		dpnhMESSAGE_MOUSE_MOVE ,
		dpnhMESSAGE_RESIZE ,
		dpnhMESSAGE_DISPLAY_CHANGE ,
		dpnhMESSAGE_NUM ,
	};
	
	public enum dpnuUsageMode {
    	DPNU_UM_DEFAULT=0,
    	DPNU_UM_SENSOR_ONLY=1,
    	DPNU_UM_EDITOR_MODE=2,
    	DPNU_UM_COUNT,
    };

	public enum dpnuCONFIG
	{
		dpnuCONFIG_FREEZE , //freeze time-warp? 0 not freezed, 1 freezed. time-warp is disabled when freezed.
		dpnuCONFIG_INTERPUPILLARY , // interpupillary distance in millimeter.
		dpnuCONFIG_RENDER_FOVX , // fov x when rendering, in degree
		dpnuCONFIG_RENDER_FOVY , // fov y when rendering, in degree
		dpnuCONFIG_COUNT ,
	};

	[StructLayout(LayoutKind.Sequential)]
	public struct dpnRect
	{
		public float x;
		public float y;
		public float w;
		public float h;

		public dpnRect (Rect rect)
		{
			x = rect.x;
			y = rect.y;
			w = rect.width;
			h = rect.height;
		}
	};
	
	[StructLayout(LayoutKind.Sequential)]
	public struct dpnVector3
	{
		public float x;
		public float y;
		public float z;

        public dpnVector3(float _x, float _y, float _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }
    }

	[StructLayout(LayoutKind.Sequential)]
	public struct dpnQuarterion
	{
		public float s;
		public float i;
		public float j;
		public float k;

        public dpnQuarterion(float _s, float _i, float _j, float _k)
        {
            s = _s;
            i = _i;
            j = _j;
            k = _k;
        }

        public Quaternion ToQuaternion()
		{
#if UNITY_ANDROID && !UNITY_EDITOR
			return new Quaternion( i , j , -k , -s );
#else
            return new Quaternion( -i , -j , -k , s );
#endif
		}
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct dpnTransform
	{
		public dpnQuarterion q;
		public dpnVector3 p;
	}
	[StructLayout(LayoutKind.Sequential)]
	public struct dpnSensorData
	{
		public dpnVector3 angular_velocity;
		public dpnVector3 linear_acceleration;
		public dpnVector3 magnetometer;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct dpnnDeviceInfo
	{
		public float ipd;
		public int resolution_x; // single eye
		public int resolution_y; // single eye
		public float fov_x;
		public float fov_y;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct dpnnPrediction
	{
		public int key;
		public dpnQuarterion pose;
		public dpnVector3 position;
		public double time;
		double sampleTime;
	}

	public struct Common
	{
		public const string DeePoonDeviceGameObjectName = "_DeePoonDeviceGameObject";
        public const string LibDpn = "DpnUnity";
		#if UNITY_ANDROID && !UNITY_EDITOR
		public const int NUM_BUFFER = 3; // triple buffer
        #else
        public const int NUM_BUFFER = 1;

		#endif
	}
}
