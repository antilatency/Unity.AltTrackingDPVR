/************************************************************************************

Copyright   :   Copyright 2015-2017 DeePoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

namespace dpn
{
#if UNITY_ANDROID && !UNITY_EDITOR
	public class JavaActivity
	{
		// Get this from Unity on startup so we can call Activity java functions.
		private bool androidJavaInit = false;
		private AndroidJavaObject activity;
		private AndroidJavaClass javaVrActivityClass;

		public void Init()
		{
			if (!androidJavaInit)
			{
				AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
				activity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
				//javaVrActivityClass = new AndroidJavaClass("com.deepoon.sdk.VrActivity");
				// Prepare for the RenderThreadInit()
			    DPN_SetInitVariables(activity.GetRawObject(), (IntPtr)0);
				
				androidJavaInit = true;
			}
		}

		[DllImport(Common.LibDpn)]
		private static extern void DPN_SetInitVariables(IntPtr activity, IntPtr vrActivityClass);
	}
#endif
}