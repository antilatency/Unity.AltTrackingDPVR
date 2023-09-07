
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using System.Runtime.InteropServices;
using System;

namespace dpn
{
    public static class DpnBuildProcessor
    {
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget target, string path)
        {
        }

        [PostProcessScene]
        public static void OnPostProcessScene()
        {
            string wndClassName = "#32770";
            string wndName = "Building Player";

            IntPtr hWndBuildPlayer = FindWindow(wndClassName, wndName);
#if UNITY_ANDROID
            if (PlayerSettings.defaultInterfaceOrientation != UIOrientation.LandscapeLeft)
            {
                PlayerSettings.defaultInterfaceOrientation = UIOrientation.LandscapeLeft;
            }

#if UNITY_5_5_OR_NEWER

            var deviceTypes = PlayerSettings.GetGraphicsAPIs(BuildTarget.Android);
            if (deviceTypes.Length > 0)
            {
                var deviceType = deviceTypes[0];
                if (deviceType == UnityEngine.Rendering.GraphicsDeviceType.Vulkan)
                {
                    string message = "The Graphics APIs: [ Vulkan ] is not supported! \r\n Please switch to OpenGLES3.";
                    if (EditorUtility.DisplayDialog("Error", message, "OK"))
                    {
                        if (hWndBuildPlayer != IntPtr.Zero)
                        {
                            SendMessage(hWndBuildPlayer, 0x10, new IntPtr(0), "0");
                            return;
                        }
                    }
                }
            }
#endif

#if UNITY_5_6_OR_NEWER
            var settings = DpnManager.LoadSettings();
            DpnManagerEdit.SetSinglePassEnabled(settings != null && settings.info.isSinglePass);
#endif

#endif
        }
    }
}