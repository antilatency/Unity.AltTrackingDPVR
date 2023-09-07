
using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace dpn
{
    public class DpnFinalRenderer : MonoBehaviour
    {
        new Renderer renderer;

        void Awake()
        {
            renderer = GetComponent<Renderer>();
#if UNITY_5_6_0 || UNITY_5_6_1
            this.enabled = false;
#endif
        }

        void OnEnable()
        {
            if (DpnManager.IsScriptRenderingPipeline)
            {
#if UNITY_2019_1_OR_NEWER
                RenderPipelineManager.endCameraRendering += EndCameraRendering;
#endif
            }
            else
            {
                if(DpnManager.IsSinglePass)
                {
                    DpnBaseCamera.onPostRender += OnCameraPostRender;
                }
                else
                {
                    Camera.onPostRender += OnCameraPostRender;
                }
            }

            renderer.enabled = false;
        }

        void OnDisable()
        {
            if (DpnManager.IsScriptRenderingPipeline)
            {
#if UNITY_2019_1_OR_NEWER
                RenderPipelineManager.endCameraRendering -= EndCameraRendering;
#endif
            }
            else
            {
                if(DpnManager.IsSinglePass)
                {
                    DpnBaseCamera.onPostRender -= OnCameraPostRender;
                }
                else
                {
                    Camera.onPostRender -= OnCameraPostRender;
                }
                
            }
            renderer.enabled = true;
        }

        void OnCameraPostRender(DpnBaseCamera vrCamera)
        {
            OnCameraPostRender(vrCamera.camera);
        }

        void OnCameraPostRender(Camera camera)
        {
            if (renderer == null)
                return;

            int layerMask = 1 << gameObject.layer;
            if ((camera.cullingMask & layerMask) == 0)
            {
                return;
            }

            Camera curCamera = Camera.current;
            CommandBuffer cmd = new CommandBuffer();

            Camera.SetupCurrent(camera);

            if (camera.targetTexture != null)
                cmd.SetRenderTarget(camera.targetTexture);

            cmd.DrawRenderer(this.renderer, renderer.material);
            Graphics.ExecuteCommandBuffer(cmd);

            Camera.SetupCurrent(curCamera);
        }


#if UNITY_2019_1_OR_NEWER
        void EndCameraRendering(ScriptableRenderContext context, Camera camera)
        {
            OnCameraPostRender(camera);
        }
#endif
    }
}