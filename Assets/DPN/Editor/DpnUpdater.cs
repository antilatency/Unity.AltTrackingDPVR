

using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UnityEditor
{

    [InitializeOnLoad]
    public static class DpnUpdater
    {
        [InitializeOnLoadMethod]
        static void AutoUpdate()
        {
            Update();
        }

        //[MenuItem("DPVR/Update")]
        static void Update()
        {
            int itemCount = 0;

            itemCount += DeleteFiles();

            itemCount += MoveFiles();

            itemCount += DeleteDirectories();

            if (itemCount > 0)
            {
                AssetDatabase.Refresh();
            }
        }

        struct FileMoveElement
        {
            public string src;
            public string dest;
            public bool moveMeta;

            public FileMoveElement(string src, string dest, bool meta = true)
            {
                this.src = src;
                this.dest = dest;
                this.moveMeta = meta;
            }
        }

        static void CreateDirectories()
        {
            string[] directories = new string[]
            {
                @"Assets\DPN\Scenes\MultiLayer",
                @"Assets\DPN\Scenes\GazeCursor",
                @"Assets\DPN\Scenes\Cubes",
            };

            foreach (var dir in directories)
            {
                if (Directory.Exists(dir))
                    continue;

                Directory.CreateDirectory(dir);
            }
        }

        static int MoveFiles()
        {
            CreateDirectories();

            int moveItemCount = 0;
            FileMoveElement[] elements = new FileMoveElement[]
            {
                //new FileMoveElement(@"Assets\DPN\GazeCursor\DpnBasePointer.cs", @"Assets\DPN\Scripts\DpnBasePointer.cs"),
                new FileMoveElement(@"Assets\DPN\MultiLayer\MultiLayer_Cube.mat" ,@"Assets\DPN\Scenes\MultiLayer\MultiLayer_Cube.mat"),
                new FileMoveElement(@"Assets\DPN\MultiLayer\MultiLayer_Cube.shader" ,@"Assets\DPN\Scenes\MultiLayer\MultiLayer_Cube.shader"),
                new FileMoveElement(@"Assets\DPN\MultiLayer\MultiLayer_CubeTransparent.mat" ,@"Assets\DPN\Scenes\MultiLayer\MultiLayer_CubeTransparent.mat"),
                new FileMoveElement(@"Assets\DPN\MultiLayer\MultiLayer_CubeTransparent.shader" ,@"Assets\DPN\Scenes\MultiLayer\MultiLayer_CubeTransparent.shader"),
                new FileMoveElement(@"Assets\DPN\MultiLayer\MultiLayer_New.unity" ,@"Assets\DPN\Scenes\MultiLayer\MultiLayer.unity"),

                new FileMoveElement(@"Assets\DPN\Scenes\Cubes.unity", @"Assets\DPN\Scenes\Cubes\Cubes.unity"),
                new FileMoveElement(@"Assets\DPN\Scenes\GazeCursor.unity", @"Assets\DPN\Scenes\GazeCursor\GazeCursor.unity"),
                
                // files
                new FileMoveElement(@"Assets\DPN\GazeCursor\DpnBasePointer.cs", @"Assets\DPN\Scripts\DpnBasePointer.cs"),
                new FileMoveElement(@"Assets\DPN\GazeCursor\DpnPointerManager.cs", @"Assets\DPN\Scripts\DpnPointerManager.cs"),
                new FileMoveElement(@"Assets\DPN\GazeCursor\DpnStandaloneInputModule.cs", @"Assets\DPN\Scripts\DpnStandaloneInputModule.cs"),
                new FileMoveElement(@"Assets\Plugins\DpnVRPay.cs", @"Assets\DPN\Pay\DpnVRPay.cs"),
                new FileMoveElement(@"Assets\DPN\Utilities\texCube.png", @"Assets\DPN\Scenes\Cubes\texCube.png"),
                new FileMoveElement(@"Assets\DPN\Utilities\shdCube.shader", @"Assets\DPN\Scenes\Cubes\shdCube.shader"),

                // libs
                new FileMoveElement(@"Assets\Plugins\Android\libdeepoon_sdk.so", @"Assets\Plugins\Android\libs\armeabi-v7a\libdeepoon_sdk.so"),
                new FileMoveElement(@"Assets\Plugins\Android\libdeepoon_vrpay.so", @"Assets\Plugins\Android\libs\armeabi-v7a\libdeepoon_vrpay.so"),
                new FileMoveElement(@"Assets\Plugins\Android\libDpnUnity.so", @"Assets\Plugins\Android\libs\armeabi-v7a\libDpnUnity.so"),
                new FileMoveElement(@"Assets\Plugins\Android\libdpvr_api.so", @"Assets\Plugins\Android\libs\armeabi-v7a\libdpvr_api.so"),
                new FileMoveElement(@"Assets\Plugins\Android\libsvrapi.so", @"Assets\Plugins\Android\libs\armeabi-v7a\libsvrapi.so"),

                new FileMoveElement(@"Assets\DPN\Prefabs\FullCubeGrid.prefab",@"Assets\DPN\Scenes\Cubes\FullCubeGrid.prefab"),

                new FileMoveElement(@"Assets\DPN\Utilities\matCube.mat", @"Assets\DPN\Scenes\Cubes\matCube.mat"),
                new FileMoveElement(@"Assets\DPN\Utilities\TestMotion.cs", @"Assets\DPN\Scenes\Cubes\TestMotion.cs"),
            };

            // Assets\MoveTest.cs
            var root = System.Environment.CurrentDirectory;

            foreach (var element in elements)
            {
                var src = element.src;
                var dest = element.dest;

                if (File.Exists(src))
                {
                    moveItemCount++;
                    try
                    {
                        if (File.Exists(dest))
                        {
                            if (File.Exists(src))
                            {
                                File.Delete(src);
                                Debug.Log(string.Format("Delete file[{0}]", src));
                            }
                        }
                        else
                        {
                            File.Move(src, dest);
                            if (element.moveMeta)
                            {
                                File.Move(src + ".meta", dest + ".meta");
                                Debug.Log(string.Format("Move file[{0}] to [{1}]", src, dest));
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Debug.LogError(string.Format("Move file[{0}] to [{1}]", src, dest));
                    }
                    
                }
            }

            return moveItemCount;
        }

        static int DeleteFiles()
        {
            string[] files = new string[]
            {
                // files
                @"Assets\DPN\GazeCursor\DpnBasePointerRaycaster.cs",
                @"Assets\DPN\GazeCursor\DpnEventInterfacesExtension.cs",
                @"Assets\DPN\GazeCursor\DpnExecuteEventsExtension.cs",
                @"Assets\DPN\GazeCursor\DpnPointerGraphicRaycaster.cs",
                @"Assets\DPN\GazeCursor\DpnPointerInputModule.cs",
                @"Assets\DPN\GazeCursor\DpnPointerPhysicsRaycaster.cs",
                @"Assets\DPN\GazeCursor\IDpnPointer.cs",
                @"Assets\DPN\GUI\Scripts\DpnGUI.cs",
                @"Assets\DPN\Scripts\DpnCamera3.cs",
                @"Assets\DPN\Scripts\DpnUI.cs",
                @"Assets\DPN\Utilities\CursorMaterial\ControllerRay.cs",
                @"Assets\DPN\Utilities\CursorMaterial\DpnHMDPointer.cs",
                @"Assets\DPN\Utilities\CursorMaterial\ReticlePointer.cs",
                @"Assets\DPN\Utilities\SampleGUI.cs",

                // scenes
                @"Assets\DPN\Scenes\PC\Polaris_Sample.unity",
                @"Assets\DPN\Scenes\Mobile\DpnDaydream.unity",
                @"Assets\DPN\Scenes\Spheres.unity",
                @"Assets\DPN\Scenes\MultiLayer.unity",

                @"Assets\DPN\GUI\Materials\DpnGuiMat.mat",
                @"Assets\DPN\GUI\Models\Materials\DpnGuiMatCurvedSurface.mat",
                @"Assets\DPN\GUI\Models\Materials\DpnGuiMatFlatSurface.mat",
                @"Assets\DPN\GUI\Shaders\DpnGuiTransparentOverlayShader.shader",
                @"Assets\DPN\GUI\Skins\ExampleSkin.guiskin",
                @"Assets\DPN\Scripts\DpnRotate.cs",

                @"Assets\DPN\Editor\DpnBuild.cs",
                @"Assets\DPN\Editor\DpnShimLoader.cs",
                @"Assets\DPN\Prefabs\DpnCameraRigWithGui.prefab",
                @"Assets\DPN\GUI\Resources\DpnGuiCurvedSurface.prefab",
                @"Assets\DPN\GUI\Resources\DpnGuiFlatSurface.prefab",
                @"Assets\DPN\GUI\Models\Materials\DpnGuiMatFlatSurface.mat",
                @"Assets\DPN\GUI\Models\Materials\DpnGuiMatCurvedSurface.mat",
                @"Assets\DPN\GUI\Models\DpnGuiFbxFlatSurface.fbx",
                @"Assets\DPN\GUI\Models\DpnGuiFbxCurvedSurface.fbx",
                @"Assets\DPN\GUI\Materials\DpnGuiMat.mat",
                @"Assets\DPN\Prefabs\DpnPlayer.prefab",
                @"Assets\DPN\Prefabs\Spheres.prefab"
            };

            int deleteItemCount = 0;
            var cur = System.Environment.CurrentDirectory;
            foreach (var file in files)
            {
                if (File.Exists(file))
                {
                    deleteItemCount++;
                    Debug.Log("Delete ObsoleteFile : " + file);
                    File.Delete(file);
                    File.Delete(file + ".meta");
                    Debug.Log("Delete ObsoleteFile : " + file + ".meta");
                }
            }

            return deleteItemCount;
        }

        static int DeleteDirectories()
        {
            int deleteItemCount = 0;
            string[] dirs = new string[]
            {
                @"Assets\DPN\Scenes\PC",
                @"Assets\DPN\Scenes\Mobile",
            };


            foreach (var dir in dirs)
            {
                if (Directory.Exists(dir))
                {
                    deleteItemCount++;
                    Debug.Log("Delete Directory : " + dir);
                    Directory.Delete(dir, true);
                }
            }

            return deleteItemCount;
        }
    }
}
