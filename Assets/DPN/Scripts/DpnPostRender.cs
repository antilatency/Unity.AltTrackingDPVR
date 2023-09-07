/************************************************************************************

Copyright   :   Copyright 2015-2017 DeePoon LLC. All Rights reserved.

DPVR Developer Website: http://developer.dpvr.cn

************************************************************************************/

using System;
using UnityEngine;
using UnityEngine.Rendering;
using System.Runtime.InteropServices;

namespace dpn
{
	public class DpnPostRender : MonoBehaviour
    {
#if !UNITY_ANDROID
        private CommandBuffer commandbuff;
        private Mesh mesh;
        private Material material;

        private Vector3[] g_VertexDataLeftStandard = {
        new Vector3(1.000000f, 0.000000f), new Vector3(1.000000f, 0.199122f), new Vector3(0.910000f, 0.110000f),
        new Vector3(1.000000f, 0.000000f), new Vector3(0.910000f, 0.110000f), new Vector3(0.810000f, 0.045000f),
        new Vector3(1.000000f, 0.000000f), new Vector3(0.810000f, 0.045000f), new Vector3(0.680000f, 0.000000f),
        new Vector3(0.000000f, 0.000000f), new Vector3(0.370000f, 0.000000f), new Vector3(0.240000f, 0.050000f),
        new Vector3(0.000000f, 0.000000f), new Vector3(0.240000f, 0.050000f), new Vector3(0.132000f, 0.123000f),
        new Vector3(0.000000f, 0.000000f), new Vector3(0.132000f, 0.123000f), new Vector3(0.060000f, 0.200000f),
        new Vector3(0.000000f, 0.000000f), new Vector3(0.060000f, 0.200000f), new Vector3(0.000000f, 0.295000f),
        new Vector3(0.000000f, 1.000000f), new Vector3(0.000000f, 0.700000f), new Vector3(0.040000f, 0.768000f),
        new Vector3(0.000000f, 1.000000f), new Vector3(0.040000f, 0.768000f), new Vector3(0.090000f, 0.833000f),
        new Vector3(0.000000f, 1.000000f), new Vector3(0.090000f, 0.833000f), new Vector3(0.190000f, 0.915000f),
        new Vector3(0.000000f, 1.000000f), new Vector3(0.190000f, 0.915000f), new Vector3(0.271000f, 0.960000f),
        new Vector3(0.000000f, 1.000000f), new Vector3(0.271000f, 0.960000f), new Vector3(0.388000f, 1.000000f),
        new Vector3(1.000000f, 1.000000f), new Vector3(0.656000f, 1.000000f), new Vector3(0.740000f, 0.975000f),
        new Vector3(1.000000f, 1.000000f), new Vector3(0.740000f, 0.975000f), new Vector3(0.813000f, 0.945000f),
        new Vector3(1.000000f, 1.000000f), new Vector3(0.813000f, 0.945000f), new Vector3(0.930000f, 0.868000f),
        new Vector3(1.000000f, 1.000000f), new Vector3(0.930000f, 0.868000f), new Vector3(1.000000f, 0.800978f)
        };

        private Vector3[] g_VertexDataRightStandard = {
        new Vector3(0.000000f, 0.000000f), new Vector3(0.000000f, 0.195412f), new Vector3(0.090000f, 0.110000f),
        new Vector3(0.000000f, 0.000000f), new Vector3(0.090000f, 0.110000f), new Vector3(0.190000f, 0.045000f),
        new Vector3(0.000000f, 0.000000f), new Vector3(0.190000f, 0.045000f), new Vector3(0.320000f, 0.000000f),
        new Vector3(1.000000f, 0.000000f), new Vector3(0.630000f, 0.000000f), new Vector3(0.760000f, 0.050000f),
        new Vector3(1.000000f, 0.000000f), new Vector3(0.760000f, 0.050000f), new Vector3(0.868000f, 0.123000f),
        new Vector3(1.000000f, 0.000000f), new Vector3(0.868000f, 0.123000f), new Vector3(0.940000f, 0.200000f),
        new Vector3(1.000000f, 0.000000f), new Vector3(0.940000f, 0.200000f), new Vector3(1.000000f, 0.295000f),
        new Vector3(1.000000f, 1.000000f), new Vector3(1.000000f, 0.700000f), new Vector3(0.960000f, 0.768000f),
        new Vector3(1.000000f, 1.000000f), new Vector3(0.960000f, 0.768000f), new Vector3(0.910000f, 0.833000f),
        new Vector3(1.000000f, 1.000000f), new Vector3(0.910000f, 0.833000f), new Vector3(0.810000f, 0.915000f),
        new Vector3(1.000000f, 1.000000f), new Vector3(0.810000f, 0.915000f), new Vector3(0.729000f, 0.960000f),
        new Vector3(1.000000f, 1.000000f), new Vector3(0.729000f, 0.960000f), new Vector3(0.612000f, 1.000000f),
        new Vector3(0.000000f, 1.000000f), new Vector3(0.344000f, 1.000000f), new Vector3(0.260000f, 0.975000f),
        new Vector3(0.000000f, 1.000000f), new Vector3(0.260000f, 0.975000f), new Vector3(0.187000f, 0.945000f),
        new Vector3(0.000000f, 1.000000f), new Vector3(0.187000f, 0.945000f), new Vector3(0.070000f, 0.868000f),
        new Vector3(0.000000f, 1.000000f), new Vector3(0.070000f, 0.868000f), new Vector3(0.000000f, 0.799378f)
        };

        int[] indices = {
        0,1,2,3,4,5,6,7,8,9,
        10,11,12,13,14,15,16,17,18,19,
        20,21,22,23,24,25,26,27,28,29,
        30,31,32,33,34,35,36,37,38,39,
        40,41,42,43,44,45,46,47
        };

#endif
        new Camera camera;

        private void Awake()
        {
            camera = GetComponent<Camera>();
        }

        // Use this for initialization
        void OnEnable()
        {
#if !UNITY_ANDROID
            commandbuff = new CommandBuffer();
            mesh = new Mesh();
            if (transform.name.Equals("LeftEyeAnchor"))
            {
                mesh.vertices = g_VertexDataLeftStandard;
            }
            else
            {
                mesh.vertices = g_VertexDataRightStandard;
            }
            mesh.triangles = indices;
            material = Resources.Load<Material>("DPN/hiddenmesh");
            commandbuff.DrawMesh(mesh, Matrix4x4.identity, material);
            GetComponent<Camera>().AddCommandBuffer(CameraEvent.BeforeForwardOpaque, commandbuff);
#endif

            if (DpnManager.IsScriptRenderingPipeline)
            {
#if UNITY_2019_1_OR_NEWER
                RenderPipelineManager.endCameraRendering += EndCameraRendering;
#endif
            }
        }

#if UNITY_2019_1_OR_NEWER
        void EndCameraRendering(ScriptableRenderContext context, Camera camera)
        {
            if (camera != this.camera)
                return;


#if UNITY_ANDROID && !UNITY_EDITOR
            if (!DpnDevice.bVR9)
            {      
                _EndEye(camera);
            }
#endif
        }
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
		void OnPostRender()
		{
		    if (!DpnDevice.bVR9)
            {      
                _EndEye(camera);
            }
		}

#if UNITY_5_6_0 || UNITY_5_6_1
        IntPtr prev;
        RenderTexture prevTarget;
#endif
        /// <summary>
        /// invoked by DpnPostRender
        /// </summary>
        public void _EndEye(Camera cam)
		{
			dpncEYE eye = ((PLUGIN_EVENT_TYPE)cam.depth == PLUGIN_EVENT_TYPE.RightEyeEndFrame) ? dpncEYE.RIGHT : dpncEYE.LEFT;
			dpnRect view = new dpnRect(new Rect(0,0, cam.targetTexture.width, cam.targetTexture.height));
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
            prev = cam.targetTexture.GetNativeTexturePtr();
            prevTarget = camera.targetTexture;
#else
            IntPtr eyeTexturePtr = cam.targetTexture.GetNativeTexturePtr();
            if (DpnCameraRig.IsRecentering)
            {
                DpnCameraRig.ClearRenderTexture(cam.targetTexture, Color.black);
            }
#endif
            Composer.SetTextures( eyeTexturePtr, eye );
		}
#endif
    }
}
