using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace dpn
{
    [RequireComponent(typeof(Camera))]
    public class DpnBaseCamera : MonoBehaviour
    {
        Camera _camera;
        public new Camera camera
        {
            get
            {
                if (_camera == null)
                {
                    _camera = GetComponent<Camera>();
                }
                return _camera;
            }
        }

        [SerializeField]
        DpnEye _eye = DpnEye.Both;

        public DpnEye Eye { get { return _eye; } }

        [SerializeField]
        int _layer = 0;

        public int Layer { get { return _layer; } }

        [SerializeField]
        dpncTwType _twType = dpncTwType.TW_DISTORTION;

        public dpncTwType TwType { get { return _twType; } }

        static public Action<DpnBaseCamera> onPostRender;


        virtual protected void OnEnable()
        {
#if UNITY_2019_1_OR_NEWER
            if (DpnManager.IsScriptRenderingPipeline)
            {
                RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
                RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
        }
#endif
        }

        virtual protected void OnDisable()
        {
#if UNITY_2019_1_OR_NEWER
            if (DpnManager.IsScriptRenderingPipeline)
            {
                RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
                RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
            }
#endif
        }

#if UNITY_2019_1_OR_NEWER
        void OnBeginCameraRendering(ScriptableRenderContext context, Camera camera)
        {
            if (camera != this.camera)
                return;

            OnPreRender();
        }

        void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
        {
            if (camera != this.camera)
                return;

            OnPostRender();
        }
#endif
        virtual protected void OnPreRender() { }

        virtual protected void OnPostRender() { }
    }

    public class DpnAndroidCamera : DpnBaseCamera
    {
#if UNITY_5_6_OR_NEWER
        //Shader Variables used for single-pass stereo rendering
        private Vector4[] unity_StereoScaleOffset = new Vector4[2];

        private Matrix4x4[] unity_StereoCameraProjection = new Matrix4x4[2];
        private Matrix4x4[] unity_StereoCameraInvProjection = new Matrix4x4[2];

        private Matrix4x4[] unity_StereoWorldToCamera = new Matrix4x4[2];
        private Matrix4x4[] unity_StereoCameraToWorld = new Matrix4x4[2];

        private Matrix4x4[] unity_StereoMatrixV = new Matrix4x4[2];
        private Matrix4x4[] unity_StereoMatrixInvV = new Matrix4x4[2];

        private Matrix4x4[] unity_StereoMatrixP = new Matrix4x4[2];
        private Matrix4x4[] unity_StereoMatrixInvP = new Matrix4x4[2];

        private Matrix4x4[] unity_StereoMatrixVP = new Matrix4x4[2];
        Matrix4x4[] unity_StereoMatrixVP_skybox = new Matrix4x4[2];

        Matrix4x4[] eyeOffsetMatrix = new Matrix4x4[2];

        Vector4[] unity_StereoWorldSpaceCameraPos = new Vector4[2];

        //the eye distance, will use the value from the Camera.stereoSeparation
        private Vector3[] eyeOffsetVector = new Vector3[2];

        private void OnDestroy()
        {
            foreach(var renderTexture in renderTextures)
            {
                renderTexture.Release();
            }
        }

        const int renderTextureCount = 3;

        RenderTexture[] renderTextures = new RenderTexture[renderTextureCount];

        int renderTextureIndex = 0;

        public RenderTexture CurrentRenderTexture
        {
            get
            {
                return renderTextures[renderTextureIndex];
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            DisableSinglePass();
        }

        void EnableSinglePass()
        {
            Shader.EnableKeyword("STEREO_MULTIVIEW_ON");
            Shader.EnableKeyword("UNITY_SINGLE_PASS_STEREO");
        }

        void DisableSinglePass()
        {
            Shader.DisableKeyword("STEREO_MULTIVIEW_ON");
            Shader.DisableKeyword("UNITY_SINGLE_PASS_STEREO");
        }

        public static bool IsSinglePassEnabled = false;

        void SetSinglePassEnabled(bool value)
        {
            IsSinglePassEnabled = value;
            IntPtr tempPtr = Marshal.StringToHGlobalAnsi("is_single_pass_enabled");
            dpn.Composer.DpnuSetIntValue(tempPtr, value ? 1 : 0);
            Marshal.FreeHGlobal(tempPtr);
        }

        // Use this for initialization
        void Start()
        {
            if(!DpnManager.IsSinglePass)
                return;
      
            for (int i = 0; i < renderTextureCount; ++i)
            {
                renderTextures[i] = DpnUtils.CreateRenderTexture_Tex2DArray();
            }

            EnableSinglePass();

            SetSinglePassEnabled(true);

            //Very few documentations about this variable, not sure if I set this right.
            Vector4[] unity_StereoScaleOffset = new Vector4[2];
            unity_StereoScaleOffset[0] = new Vector4(1.0f, 1.0f, 0f, 0f);
            unity_StereoScaleOffset[1] = new Vector4(1.0f, 1.0f, 0.5f, 0f);
            // for StereoScreenSpaceTex
            Shader.SetGlobalVectorArray("unity_StereoScaleOffset", unity_StereoScaleOffset);

            //The MatrixVP should be set before skybox rendering.
            if (beforeSkyCB == null)
            {
                beforeSkyCB = new CommandBuffer();
                camera.AddCommandBuffer(CameraEvent.BeforeSkybox, beforeSkyCB);
            }

            if (afterSkyCB == null)
            {
                afterSkyCB = new CommandBuffer();
                camera.AddCommandBuffer(CameraEvent.AfterSkybox, afterSkyCB);
            }

            float eyeOffset = DpnDevice.DeviceInfo.ipd / 2.0f;
            eyeOffsetVector[0] = new Vector3(-eyeOffset, 0, 0);
            eyeOffsetVector[1] = new Vector3(eyeOffset, 0, 0);
            eyeOffsetMatrix[0] = Matrix4x4.TRS(eyeOffsetVector[0], Quaternion.identity, Vector3.one);
            eyeOffsetMatrix[1] = Matrix4x4.TRS(eyeOffsetVector[1], Quaternion.identity, Vector3.one);
        }

        CommandBuffer afterSkyCB;

        public static Matrix4x4 MakeProjection(float n, float f)
        {
            Matrix4x4 m = new Matrix4x4
            {
                m00 = 1,
                m11 = 1,
                m22 = -(f + n) / (f - n),
                m23 = -2 * f * n / (f - n),
                m32 = -1,
            };

            return m;
        }

        protected override void OnPreRender()
        {
            base.OnPreRender();

            renderTextureIndex = (renderTextureIndex + 1) % renderTextureCount;
            this.camera.targetTexture = CurrentRenderTexture;

            Matrix4x4 world2camera = camera.worldToCameraMatrix;
            Matrix4x4 camera2world = camera.cameraToWorldMatrix;

            //The camera is the center point of two eyes
            unity_StereoWorldToCamera[0] = eyeOffsetMatrix[0].inverse * world2camera;
            unity_StereoWorldToCamera[1] = eyeOffsetMatrix[1].inverse * world2camera;

            unity_StereoCameraToWorld[0] = camera2world * eyeOffsetMatrix[0];
            unity_StereoCameraToWorld[1] = camera2world * eyeOffsetMatrix[1];

            unity_StereoMatrixV[0] = unity_StereoWorldToCamera[0];
            unity_StereoMatrixV[1] = unity_StereoWorldToCamera[1];

            unity_StereoMatrixInvV[0] = unity_StereoMatrixV[0].inverse;
            unity_StereoMatrixInvV[1] = unity_StereoMatrixV[1].inverse;

            unity_StereoMatrixP[0] = MakeProjection( camera.nearClipPlane, camera.farClipPlane);
            unity_StereoMatrixP[1] = MakeProjection( camera.nearClipPlane, camera.farClipPlane);

            unity_StereoCameraProjection[0] = unity_StereoMatrixP[0];
            unity_StereoCameraProjection[1] = unity_StereoMatrixP[1];

            unity_StereoCameraInvProjection[0] = unity_StereoCameraProjection[0].inverse;
            unity_StereoCameraInvProjection[1] = unity_StereoCameraProjection[1].inverse;

            unity_StereoWorldSpaceCameraPos[0] = unity_StereoCameraToWorld[0].MultiplyPoint(Vector3.zero);
            unity_StereoWorldSpaceCameraPos[1] = unity_StereoCameraToWorld[1].MultiplyPoint(Vector3.zero);

            unity_StereoMatrixVP[0] = unity_StereoMatrixP[0] * unity_StereoMatrixV[0];
            unity_StereoMatrixVP[1] = unity_StereoMatrixP[1] * unity_StereoMatrixV[1];

            Shader.SetGlobalMatrixArray("unity_StereoCameraProjection", unity_StereoCameraProjection);
            Shader.SetGlobalMatrixArray("unity_StereoCameraInvProjection", unity_StereoCameraInvProjection);

            Shader.SetGlobalMatrixArray("unity_StereoWorldToCamera", unity_StereoWorldToCamera);
            Shader.SetGlobalMatrixArray("unity_StereoCameraToWorld", unity_StereoCameraToWorld);

            Shader.SetGlobalVectorArray("unity_StereoWorldSpaceCameraPos", unity_StereoWorldSpaceCameraPos);

            Shader.SetGlobalMatrixArray("unity_StereoMatrixV", unity_StereoMatrixV);
            Shader.SetGlobalMatrixArray("unity_StereoMatrixInvV", unity_StereoMatrixInvV);

            Shader.SetGlobalMatrixArray("unity_StereoMatrixP", unity_StereoMatrixP);
            Shader.SetGlobalMatrixArray("unity_StereoMatrixVP", unity_StereoMatrixVP);

            // render skybox
            Matrix4x4 viewMatrixSkybox = Matrix4x4.LookAt(Vector3.zero, camera.transform.forward, camera.transform.up) * Matrix4x4.Scale(new Vector3(1, 1, -1));

            viewMatrixSkybox = viewMatrixSkybox.transpose;
            Matrix4x4 proj0 = unity_StereoCameraProjection[0];
            Matrix4x4 proj1 = unity_StereoCameraProjection[1];
            proj0.m22 = -1.0f;
            proj1.m22 = -1.0f;
            unity_StereoMatrixVP_skybox[0] = proj0 * viewMatrixSkybox;
            unity_StereoMatrixVP_skybox[1] = proj1 * viewMatrixSkybox;

            beforeSkyCB.SetGlobalMatrixArray("unity_StereoMatrixVP", unity_StereoMatrixVP_skybox);

            afterSkyCB.SetGlobalMatrixArray("unity_StereoMatrixVP", unity_StereoMatrixVP);
        }

        CommandBuffer beforeSkyCB;

        PluginEventHandler onPluginEvent_OnPostRender;

        protected override void OnPostRender()
        {
            base.OnPostRender();

            if(onPostRender != null)
            {
                onPostRender(this);
            }

            //this.camera.targetTexture = null;

            EndEye();
        }


#if UNITY_5_6_0 || UNITY_5_6_1
        IntPtr prev;
        RenderTexture prevTarget;
#endif
        void EndEye()
        {
            var targetTexture = CurrentRenderTexture;

            dpnRect view = new dpnRect(new Rect(0, 0, targetTexture.width, targetTexture.height));
            IntPtr tempPtr = Marshal.AllocHGlobal(Marshal.SizeOf(view));
            Marshal.StructureToPtr(view, tempPtr, false);
            if (!DpnDevice.bQCOM)
            { 
                // XR1: support texture clamp to board, don't need app fill edge as black. Plugin use partial clear to filledge, which cause enable foveated rendering failed
                Composer.PostRender(RENDER_EVENT.Postnontransparent, (int)tempPtr);
            }
#if UNITY_5_6_0 || UNITY_5_6_1
            IntPtr eyeTexturePtr = prev;
            if (DpnCameraRig.IsRecentering)
            {
                DpnCameraRig.ClearRenderTexture(prevTarget, Color.black);
            }
            prev = targetTexture.GetNativeTexturePtr();
            prevTarget = targetTexture;
#else
            IntPtr eyeTexturePtr = targetTexture.GetNativeTexturePtr();
            if (DpnCameraRig.IsRecentering)
            {
                DpnCameraRig.ClearRenderTexture(targetTexture, Color.black);
            }
#endif
            dpncEYE eye = dpncEYE.LEFT;
            switch (Eye)
            {
                case DpnEye.Left:
                    eye = dpncEYE.LEFT;
                    break;
                case DpnEye.Right:
                    eye = dpncEYE.RIGHT;
                    break;
                case DpnEye.Both:
                    eye = dpncEYE.BOTH;
                    break;
            }

            eyeTexturePtr = CurrentRenderTexture.GetNativeTexturePtr();

            var rect = new dpnRect() { x = 0, y = 0, w = 1, h = 1 };
            Composer.DpnuSetTextureEx((int)Layer, eyeTexturePtr, (int)eye, (int)TwType, rect);
        }
#endif
    }

    public class DpnWindowsCamera : DpnBaseCamera
    {

    }

    [RequireComponent(typeof(Camera))]
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
    public class DpnCamera : DpnWindowsCamera
#elif UNITY_ANDROID
    public class DpnCamera : DpnAndroidCamera
#endif
    {

    }
}
