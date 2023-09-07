/************************************************************************************

Copyright   :   Copyright 2015-2017 DeePoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

namespace dpn
{
    public struct PeripheralList
    {
        public DpnPeripheral peripheral;
        public List<DpnBasePeripheral> list;

        public PeripheralList(DpnPeripheral t)
        {
            peripheral = t;
            list = new List<DpnBasePeripheral>();
        }
    }

    public class DpnDevice : MonoBehaviour
    {
#if UNITY_ANDROID && !UNITY_EDITOR
		static private JavaActivity _java_activity = new JavaActivity();
#endif


        private Buffers _buffers = new Buffers();
        private Composer _composer = new Composer();
        private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
        private readonly Dictionary<string, PeripheralList> s_Peripherals = new Dictionary<string, PeripheralList>();

        private const int BUFF_NUM = 2;

        static DpnDevice _instance;
        public static DpnDevice Instance
        {
            get
            {
                if (_instance == null)
                {
                    create();
                    _instance.Init();
                }
                return _instance;
            }
        }

        bool _bVR9 = false;
        static public bool bVR9 { get { return Instance._bVR9; } }

        bool _bQCOM = false;
        static public bool bQCOM { get { return Instance._bQCOM; } }

#if UNITY_ANDROID && !UNITY_EDITOR
        private string PROPERTY_ISVR9 = "is_vr9";
        private string PROPERTY_ISQCOM = "is_qcom";
#endif

        private dpnnPrediction _prediction;

        void Awake()
        {
            SDKManagerVersion = 0;
        }

        void QueryDeviceInfo()
        {
            bool ret = Composer.DpnuGetDeviceInfo(ref _deviceInfo);
            if ((!ret) || (_deviceInfo.resolution_x <= 0) || (_deviceInfo.resolution_y <= 0))
            {
                //case: editor mode with no platform sdk installed
                _deviceInfo.ipd = 0.064f * 1;
#if UNITY_ANDROID && !UNITY_EDITOR
					_deviceInfo.fov_x = 96.0f;
					_deviceInfo.fov_y = 96.0f;
#else
                _deviceInfo.fov_x = 100.0f;
                _deviceInfo.fov_y = 100.0f;
#endif
                _deviceInfo.resolution_x = 1024;
                _deviceInfo.resolution_y = 1024;
            }
            _deviceInfo.ipd = _deviceInfo.ipd * 1;
            _deviceInfo.resolution_x = (int)(_deviceInfo.resolution_x * DpnManager.eyeTextureScale);
            _deviceInfo.resolution_y = (int)(_deviceInfo.resolution_y * DpnManager.eyeTextureScale);

            Debug.Log(string.Format("DeviceInfo : ipd {0}, resolution {1}x{2}, fov({3},{4})"
                , _deviceInfo.ipd, _deviceInfo.resolution_x, _deviceInfo.resolution_y, _deviceInfo.fov_x, _deviceInfo.fov_y));

#if UNITY_ANDROID && !UNITY_EDITOR
            IntPtr tempPtr = Marshal.StringToHGlobalAnsi(PROPERTY_ISVR9);
            _bVR9 = Composer.DpnuGetIntValue(tempPtr, 0) == 0? false: true;
            Marshal.FreeHGlobal(tempPtr);
            IntPtr tempPtrQcom = Marshal.StringToHGlobalAnsi(PROPERTY_ISQCOM);
            _bQCOM = Composer.DpnuGetIntValue(tempPtrQcom, 0) == 0? false: true;
            Marshal.FreeHGlobal(tempPtrQcom);
            Debug.Log("DpnDevice Init bVR9 " + _bVR9  + " bQCOM " + _bQCOM);
#endif
            SDKManagerVersion = QuerySDKManagerVersion();
            Debug.Log("SDKManagerVersion : " + DpnUtils.MakeVersionString(SDKManagerVersion));

            IsSupportSinglePass = CheckSupportSinglePass();
        }

        void Init()
        {
            QueryDeviceInfo();

            ConfigEngine.Init();

            if (!bVR9)
            {
                _buffers.Init
                    (DpnManager.eyeTextureDepth
                    , DpnManager.eyeTextureFormat
                    , DpnDevice.DeviceInfo.resolution_x
                    , DpnDevice.DeviceInfo.resolution_y);
#if UNITY_ANDROID && !UNITY_EDITOR
#else
                IntPtr[] buf_ptr = new IntPtr[(int)dpncEYE.NUM]
                        { _buffers.GetEyeTexturePtr( dpncEYE.LEFT )
                        , _buffers.GetEyeTexturePtr( dpncEYE.RIGHT ) };
                Composer.SetTextures(buf_ptr[0], dpncEYE.LEFT);
                Composer.SetTextures(buf_ptr[1], dpncEYE.RIGHT);
#endif
            }
            //
#if !UNITY_ANDROID
            string pcScreenOutput = "pcScreenOutput";
#if UNITY_EDITOR
			IntPtr OutputPtr = Marshal.StringToHGlobalAnsi(pcScreenOutput);
			Composer.DpnuSetIntValue(OutputPtr, (int)dpncOutputMode.NONE);
			Marshal.FreeHGlobal(OutputPtr);
#else
			IntPtr OutputPtr = Marshal.StringToHGlobalAnsi(pcScreenOutput);
			Composer.DpnuSetIntValue(OutputPtr, (int)DpnManager.pcScreenOutputMode);
			Marshal.FreeHGlobal(OutputPtr);
#endif
#endif
        }
        void OnEnable()
        {

            if (DpnManager.IsScriptRenderingPipeline)
            {
#if UNITY_2019_1_OR_NEWER
                RenderPipelineManager.beginCameraRendering += BeginCameraRendering;
#endif
            }
            else
            {
                Camera.onPreRender += CameraPreRender;
                Camera.onPreCull += CameraPreCull;
            }

            //cameras
            StartCoroutine(CallbackCoroutine());
        }

        void Start()
        {
            if (!bVR9)
            {
                DpnCameraRig.Instance._left_eye.targetTexture = _instance._buffers.GetEyeTexture(dpncEYE.LEFT);
                DpnCameraRig.Instance._right_eye.targetTexture = _instance._buffers.GetEyeTexture(dpncEYE.RIGHT);
            }
        }

        void OnDisable()
        {
            IntPtr tempPtr = Marshal.StringToHGlobalAnsi("OnDisable");
            Composer.DpnuSetIntValue(tempPtr, 1);
            Marshal.FreeHGlobal(tempPtr);

            if (DpnManager.IsScriptRenderingPipeline)
            {
#if UNITY_2019_1_OR_NEWER
                RenderPipelineManager.beginCameraRendering -= BeginCameraRendering;
#endif
            }
            else
            {
                Camera.onPreRender -= CameraPreRender;
                Camera.onPreCull -= CameraPreCull;
            }
        }

#if UNITY_ANDROID && !UNITY_EDITOR

       static public void InitJavaActivity()
       {
            _java_activity.Init();
       }
#endif

        /// <summary>
        /// Run DeePoon HMD
        /// </summary>
        public static void create()
        {
            if (_instance != null)
            {
                _instance.gameObject.SetActive(true);
                return;
            }
#if UNITY_ANDROID && !UNITY_EDITOR
			_java_activity.Init();
#endif
            Composer.Init();

            //create a new DeviceObject
            GameObject device_object = new GameObject
                (Common.DeePoonDeviceGameObjectName
                    , typeof(DpnDevice));

            if (DpnManager.DPVRPointer)
            {
                EventSystem eventSystem = FindObjectOfType<EventSystem>();
                if (eventSystem == null)
                {
                    Debug.LogWarning("DpnEventSystem is needed after version 0.7.5.");
                    GameObject gameObject = new GameObject("DpnEventSystem", typeof(EventSystem));
                    eventSystem = gameObject.GetComponent<EventSystem>();
                }
                {
                    DpnStandaloneInputModule inputModule = eventSystem.GetComponent<DpnStandaloneInputModule>();
                    if (inputModule == null)
                    {
                        Debug.LogWarning("DpnStandaloneInputModule component must be in the EventSystem, after version 0.7.5.");
                        eventSystem.gameObject.AddComponent<DpnStandaloneInputModule>();
                    }

                }
            }

            //Don't destroy it.
            DontDestroyOnLoad(device_object);

            //get device
            _instance = device_object.GetComponent<DpnDevice>();

            _instance.StartCoroutine(_instance._OnResume());
        }

        private void OnDestroy()
        {
            StopAllCoroutines();

#if UNITY_ANDROID && !UNITY_EDITOR
			RenderTexture.active = null;
#else
            // do nothing
#endif

            Composer.Uninit();
            if (!bVR9)
            {
                if (DpnCameraRig.Instance != null)
                {
                    if (DpnCameraRig.Instance._left_eye)
                        DpnCameraRig.Instance._left_eye.targetTexture = null;

                    if (DpnCameraRig.Instance._right_eye)
                        DpnCameraRig.Instance._right_eye.targetTexture = null;

                    if (DpnCameraRig.Instance._center_eye)
                        DpnCameraRig.Instance._center_eye.targetTexture = null;
                }

                _buffers.clear();
            }
        }

        /// <summary>
        /// event functions 
        /// </summary>
        private void CameraPreRender(Camera cam)
        {
            if (!bVR9 && !DpnManager.IsSinglePass)
            {
                UpdateCameraTexture(cam);
            }
        }

        int lastFrameCount = -1;
        private void CameraPreCull(Camera cam)
        {
            // Only update poses on the first camera per frame.
            if (Time.frameCount != lastFrameCount)
            {
                lastFrameCount = Time.frameCount;
                DeviceUpdate();
                foreach (KeyValuePair<string, PeripheralList> i in s_Peripherals)
                {
                    if (i.Value.list.Count > 0)
                    {
                        foreach (DpnBasePeripheral s in i.Value.list)
                        {
                            s.DpnpUpdate();
                        }
                    }
                }
            }
        }

        private void DeviceUpdate()
        {
            if (!bVR9)
            {
                if (false == _buffers.SwapBuffers())
                {
#if UNITY_ANDROID && !UNITY_EDITOR
#else
                    Debug.Log("DPVR texture is recreated.");
                    IntPtr[] buf_ptr = new IntPtr[(int)dpncEYE.NUM]
                    { _buffers.GetEyeTexturePtr( dpncEYE.LEFT )
                        , _buffers.GetEyeTexturePtr( dpncEYE.RIGHT ) };
                    Composer.SetTextures(buf_ptr[0], dpncEYE.LEFT);
                    Composer.SetTextures(buf_ptr[1], dpncEYE.RIGHT);
#endif
                }
            }
        }

        private void UpdateCameraTexture(Camera cam)
        {
            if (cam == DpnCameraRig.Instance._left_eye)
            {
                cam.targetTexture = _instance._buffers.GetEyeTexture(dpncEYE.LEFT);
            }
            if (cam == DpnCameraRig.Instance._right_eye)
            {
                cam.targetTexture = _instance._buffers.GetEyeTexture(dpncEYE.RIGHT);
            }
        }

        private IEnumerator CallbackCoroutine()
        {
            while (true)
            {
                yield return waitForEndOfFrame;
                if (null != DpnCameraRig.Instance)
                {
                    DpnCameraRig.Instance.UpdatePose();
                    _composer.Compose();
#if UNITY_ANDROID && !UNITY_EDITOR
                    HideSplash();
#endif
                }
            }
        }

        private bool hasSplash = true;

#if UNITY_ANDROID && !UNITY_EDITOR

        private void HideSplash()
        {
            if (!hasSplash)
                return;

            Debug.Log("HideSplash");
            if (Application.platform.Equals(RuntimePlatform.Android))
            {
                using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        hasSplash = false;
                        jo.Call("HideSplash");
                    }
                }
            }
        }
#endif

        private void OnApplicationPause(bool pause)
        {
            if (pause)
            {
                foreach (KeyValuePair<string, PeripheralList> i in s_Peripherals)
                {
                    if (i.Value.list.Count > 0)
                    {
                        foreach (DpnBasePeripheral s in i.Value.list)
                        {
                            s.DpnpPause();
                        }
                    }
                }
                Composer.Pause();
            }
            else
                StartCoroutine(_OnResume());
        }

        private void OnApplicationFocus(bool focus)
        {
            // OnApplicationFocus() does not appear to be called 
            // consistently while OnApplicationPause is. Moved
            // functionality to OnApplicationPause().
        }

        //triggered by OnApplicationPause and OnApplicationFocus
        private IEnumerator _OnResume()
        {
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;
            yield return null;

            Composer.Resume();
            foreach (KeyValuePair<string, PeripheralList> i in s_Peripherals)
            {
                if (i.Value.list.Count > 0)
                {
                    foreach (DpnBasePeripheral s in i.Value.list)
                    {
                        s.DpnpResume();
                    }
                }
            }
        }

        public DpnPeripheral OpenPeripheral(string deviceId, DpnBasePeripheral basePeripheral)
        {
            if (deviceId == null)
            {
                return null;
            }
            if (s_Peripherals.ContainsKey(deviceId))
            {
                if (s_Peripherals[deviceId].list.Contains(basePeripheral))
                {
                    return basePeripheral.peripheral;
                }
                else
                {
                    basePeripheral.peripheral = s_Peripherals[deviceId].peripheral;
                    s_Peripherals[deviceId].list.Add(basePeripheral);
                    return basePeripheral.peripheral;
                }
            }
            DpnPeripheral temp = DpnPeripheral.OpenPeripheralDevice(deviceId);
            if (temp == null)
            {
                return null;
            }
            basePeripheral.peripheral = temp;
            s_Peripherals.Add(deviceId, new PeripheralList(temp));
            s_Peripherals[deviceId].list.Add(basePeripheral);
            return basePeripheral.peripheral;
        }

        public Dictionary<string, PeripheralList> GetPeripherals()
        {
            return s_Peripherals;
        }

        public void ClosePeripheral(DpnBasePeripheral basePeripheral)
        {
            if (!s_Peripherals[basePeripheral.peripheral._deviceId].list.Contains(basePeripheral))
            {
                return;
            }
            s_Peripherals[basePeripheral.peripheral._deviceId].list.Remove(basePeripheral);
            if (s_Peripherals[basePeripheral.peripheral._deviceId].list.Count == 0)
            {
                DpnPeripheral.ClosePeripheralDevice(basePeripheral.peripheral);
                s_Peripherals.Remove(basePeripheral.peripheral._deviceId);
            }
            basePeripheral.peripheral = null;
            return;
        }


#if UNITY_2019_1_OR_NEWER
        void BeginCameraRendering(ScriptableRenderContext context, Camera camera)
        {
            CameraPreCull(camera);
            CameraPreRender(camera);
        }
#endif

        public int SDKManagerVersion { get; private set; }

        int QuerySDKManagerVersion()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
                string PROPERTY_SDK_MANAGER_VERSION = "sdk_manager_version";
                IntPtr property_name = Marshal.StringToHGlobalAnsi(PROPERTY_SDK_MANAGER_VERSION);
                int version = dpn.Composer.DpnuGetIntValue(property_name, 0);
                Marshal.FreeHGlobal(property_name);
                return version;
#else
            return 0;
#endif
        }

        bool CheckSupportSinglePass()
        {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
            Debug.LogWarning("SinglePass is not supported for this platform.");
            return false;
#elif UNITY_ANDROID
#if !UNITY_5_6_OR_NEWER
                return false;
#endif
                if (bVR9)
                {
                    Debug.LogWarning("Platform is VR9.  SinglePass is not supported now.");
                    return false;
                }

                if(SDKManagerVersion < DpnUtils.MakeVersion(2,1,55))
                {
                    Debug.LogWarning(string.Format("SinglePass is enabled, " +
                    "but the version of SDKManager ({0}) is lower than 2.1.55. " +
                    "It is set to disabled. Please upgrade your system.",
                    DpnUtils.MakeVersionString(DpnDevice.Instance.SDKManagerVersion)));
                    return false;
                }
                return true;
#endif
        }

        public bool IsSupportSinglePass
        {
            get; private set;
        }

        private dpnnDeviceInfo _deviceInfo;

        public static dpnnDeviceInfo DeviceInfo
        {
            get
            {
                return Instance._deviceInfo;
            }
        }

    }
}
