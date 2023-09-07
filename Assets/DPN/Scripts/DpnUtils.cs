

using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Rendering;

namespace dpn
{
    public static class DpnUtils
    {
#if UNITY_5_6_OR_NEWER

        const string DllName = "DpnUnity";

        [DllImport(DllName)]
        static extern IntPtr DpnuInitRenderTexture2DArray(IntPtr colorTexture, IntPtr depthTexture, int antiAliasing);

        static public RenderTexture CreateRenderTexture_Tex2D()
        {
            int width = DpnDevice.DeviceInfo.resolution_x;
            int height = DpnDevice.DeviceInfo.resolution_y;

            RenderTexture renderTexture = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32);

            renderTexture.dimension = TextureDimension.Tex2D;
            renderTexture.volumeDepth = 2;
            renderTexture.antiAliasing = 1;
            renderTexture.useMipMap = false;
            renderTexture.autoGenerateMips = false;

            renderTexture.Create();

            return renderTexture;
        }

        static public RenderTexture CreateRenderTexture_Tex2DArray()
        {
            int width = DpnDevice.DeviceInfo.resolution_x;
            int height = DpnDevice.DeviceInfo.resolution_y;

            RenderTexture renderTexture = new RenderTexture(width, height, 24, RenderTextureFormat.ARGB32)
            {
                dimension = TextureDimension.Tex2DArray,
                volumeDepth = 2,
                antiAliasing = 1,
                useMipMap = false,
                autoGenerateMips = false,
            };

            renderTexture.Create();

            var colorTexture = renderTexture.GetNativeTexturePtr();
            var depthTexture = renderTexture.GetNativeDepthBufferPtr();
            int antiAliasing = QualitySettings.antiAliasing;

            PluginEventHandler handler = new PluginEventHandler();
            handler.AddRef();

            handler.onEvent = ((eventID) =>
            {
                DpnuInitRenderTexture2DArray(colorTexture, depthTexture, antiAliasing);
                handler.Release();
            });

            CommandBuffer cmdbuf = new CommandBuffer();
            cmdbuf.SetRenderTarget(renderTexture);
            cmdbuf.IssuePluginEvent(handler.callback, 0);

            Graphics.ExecuteCommandBuffer(cmdbuf);

            return renderTexture;
        }
#endif

        public static int MakeVersion(int major, int minor, int point)
        {
            return (major << 16) | (1 << 8) | point;
        }

        public static string MakeVersionString(int version)
        {
            return string.Format("{0}.{1}.{2}", version >> 16, (version & 0xFF00) >> 8, version & 0xFF);
        }
    }
}