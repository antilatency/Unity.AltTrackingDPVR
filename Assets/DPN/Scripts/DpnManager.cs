/************************************************************************************

Copyright   :   Copyright 2015-2017 DeePoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/

//using System.IO;
//using System.Collections;
//using System.Runtime.InteropServices;
using UnityEngine;
using System.Runtime.InteropServices;
using System;
using System.Collections;
using System.IO;
using UnityEngine.Rendering;

namespace dpn
{
    /*
	[System.Serializable]
	public struct SettingInfo
	{
		public dpncOutputMode pcScreenOutputMode;
		public TEXTURE_DEPTH eyeTextureDepth;
		public float worldScale;
		public int minimumVsync;
		public bool resetTrackerOnLoad;
		public float eyeTextureScale;
		public bool polarisSupport;
		public bool androidEditorUseHmd;
	}
	*/

    public struct SettingFileInfo
    {
        public dpncOutputMode pcScreenOutputMode;
        public TEXTURE_DEPTH eyeTextureDepth;
        public float worldScale;
        public int minimumVsync;
        public bool resetTrackerOnLoad;
        public float pcEyeTextureScale;
        public float mobileEyeTextureScale;
        public DPVRPeripheral peripheral;
        public DPVRKeyMode controllerKeyMode;
        public bool androidEditorUseHmd;
        public bool useDPVRPointer;
        public bool hmdPointerSwitchable;
        public TouchPosOrig touchPosOrig;
        public bool isSinglePass;

    }

    public class Settings
    {
        public SettingFileInfo info;
    }

    public enum DPVRKeyMode
    {
        DPVR,
        STEAM
    }

    public enum TouchPosOrig
    {
        TOP_LEFT,
        CENTER,
    }

    public enum DPVRPeripheral
    {
        None = 0,
        Anything = 1,    //not support now
        Polaris = 2,     //pc support only
        Nolo = 3,        //mobile support only
        Flip = 4,        //mobile support only
        //Wisevision = 4   //mobile support only
    }

    /// <summary>
    /// Configuration for DeePoon virtual reality.
    /// Will be deprecated in the future
    /// </summary>
    public class DpnManager
    {
        static DpnManager _instance;
        public static DpnManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DpnManager();
                    _instance.LoadSettingInfo();
                    _instance.Init();
                }
                return _instance;
            }
        }

        public static SettingFileInfo Settings
        {
            get
            {
                return Instance.settingInfo;
            }
        }

        SettingFileInfo settingInfo;

        void LoadSettingInfo()
        {
            TextAsset file_info = Resources.Load("DPN/DPNUnityConfig") as TextAsset;
            if (!file_info)
            {
                Debug.LogError("DPNUnityConfig file don't exists");
                return;
            }
            if (file_info.text == string.Empty)
            {
                Debug.LogError("DPNUnityConfig file has no data");
                return;
            }

            settingInfo = JsonUtility.FromJson<SettingFileInfo>(file_info.text);
        }

        static public Settings LoadSettings()
        {
            TextAsset file_info = Resources.Load("DPN/DPNUnityConfig") as TextAsset;
            if (!file_info)
            {
                Debug.LogError("DPNUnityConfig file don't exists");
                return null;
            }
            if (file_info.text == string.Empty)
            {
                Debug.LogError("DPNUnityConfig file has no data");
                return null;
            }

            return new Settings()
            {
                info = JsonUtility.FromJson<SettingFileInfo>(file_info.text),
            };
        }

        DpnManager()
        {

        }

        void Init()
        {
#if UNITY_2019_1_OR_NEWER
            _isScriptRenderingPipeline = (GraphicsSettings.renderPipelineAsset != null);
            if (_isScriptRenderingPipeline)
            {
                var name = GraphicsSettings.renderPipelineAsset.name;
                _isLWRP = name.Contains("LWRP");
                _isUniversalRP = name.Contains("UniversalRP");
            }
            else
            {
                _isLWRP = false;
                _isUniversalRP = false;
            }
#endif
            _isSinglePass = Settings.isSinglePass && DpnDevice.Instance.IsSupportSinglePass;
        }

        /// <summary>
        /// Dpvr unity version
        /// </summary>
        public static string DpvrUnityVersion = "0.8.0";

        /// <summary>
        /// The format of each eye texture.
        /// </summary>
        //public RenderTextureFormat eyeTextureFormat = RenderTextureFormat.Default;
        public static RenderTextureFormat eyeTextureFormat
        {
            get
            {
                return RenderTextureFormat.ARGB32;
            }
        }

#if !UNITY_ANDROID
		public static dpncOutputMode pcScreenOutputMode
        {
            get
            {
                return Settings.pcScreenOutputMode;
            }
        }
#endif

        /// <summary>
        /// The depth of each eye texture in bits. Valid Unity render texture depths are 0, 16, and 24.
        /// </summary>
        public static TEXTURE_DEPTH eyeTextureDepth
        {
            get
            {
                return Settings.eyeTextureDepth;
            }
        }

        public static float worldScale
        {
            get
            {
                return Settings.worldScale;
            }
        }

        public static int minimumVsync
        {
            get
            {
                return Settings.minimumVsync;
            }
        }

        /// <summary>
        /// If true, each scene load will cause the head pose to reset.
        /// </summary>
        public static bool resetTrackerOnLoad
        {
            get
            {
                return Settings.resetTrackerOnLoad;
            }
        }

        /// <summary>
        /// Controls the size of the eye textures.
        /// Values must be above 0.
        /// Values below 1 permit sub-sampling for improved performance.
        /// Values above 1 permit super-sampling for improved sharpness.
        /// </summary>

        public static float eyeTextureScale
        {
            get
            {
#if UNITY_ANDROID
                return Settings.mobileEyeTextureScale;
#else
                return Settings.pcEyeTextureScale;
#endif
            }
        }

        public static bool androidEditorUseHmd
        {
            get
            {
#if UNITY_ANDROID
                return Settings.androidEditorUseHmd;
#else
                return true;
#endif
            }
        }

        public static DPVRKeyMode controllerKeyMode
        {
            get
            {
                return Settings.controllerKeyMode;
            }
        }

        public static DPVRPeripheral peripheral
        {
            get
            {
                return Settings.peripheral;
            }
        }

        public static bool DPVRPointer
        {
            get
            {
                return Settings.useDPVRPointer;
            }
        }

        public static TouchPosOrig touchPosOrig
        {
            get
            {
                return Settings.touchPosOrig;
            }
        }
        public static bool hmdPointerSwitchable
        {
            get
            {
                return Settings.hmdPointerSwitchable;
            }
        }


        bool _isScriptRenderingPipeline = false;

        public static bool IsScriptRenderingPipeline
        {
            get
            {
                return Instance._isScriptRenderingPipeline;
            }
        }

        bool _isLWRP = false;
        public static bool IsLWRP
        {
            get
            {
                return Instance._isLWRP;
            }
        }

        bool _isUniversalRP = false;

        public static bool IsUniversalRP
        {
            get
            {
                return Instance._isUniversalRP;
            }
        }

        bool _isSinglePass = false;
        public static bool IsSinglePass
        {
            get
            {
                return Instance._isSinglePass;
            }
        }

        [System.Obsolete("Use DpnDevice.DeviceInfo instead.", true)]
        public static dpnnDeviceInfo DeviceInfo;

    }
}
