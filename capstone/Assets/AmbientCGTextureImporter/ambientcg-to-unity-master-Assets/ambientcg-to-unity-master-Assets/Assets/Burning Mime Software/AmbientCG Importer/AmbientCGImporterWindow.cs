/***********************************************************************************************************************
Copyright (C) 2023 Burning Mime Software, LLC.

This software is provided 'as-is', without any express or implied
warranty.  In no event will the authors be held liable for any damages
arising from the use of this software.

Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:

1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would be
   appreciated but is not required.
2. Altered source versions must be plainly marked as such, and must not be
   misrepresented as being the original software.
3. This notice may not be removed or altered from any source distribution.
***********************************************************************************************************************/

using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UDebug = UnityEngine.Debug;

namespace burningmime.unity.editor
{
    class AmbientCGImporterWindow : EditorWindow
    {
        [MenuItem("Tools/Ambient CG Importer")]
        private static void showWindow() => 
            GetWindow<AmbientCGImporterWindow>(false, "Ambient CG Importer", true);
        
        private AmbientCGZipImporter.MaterialCreationMode _mode;
        private Shader _shader;
        private GUIStyle _labelStyle;
        private bool _isHdrp;
        private string[] _creationModeLabels;

        private void OnGUI()
        {
            if(!_shader)
            {
                RenderPipelineAsset renderPipeline = GraphicsSettings.currentRenderPipeline;
                if(renderPipeline)
                {
                    string rpName = renderPipeline.GetType().Name;
                    _isHdrp = rpName.Contains("HD") || rpName.Contains("HighDefinition");
                    _shader = renderPipeline.defaultShader;
                    if(!_shader) _shader = Shader.Find("Standard");
                }
                else
                {
                    _shader = Shader.Find("Standard");
                }
            }
            
            _shader = (Shader) EditorGUILayout.ObjectField("Shader", _shader, typeof(Shader), false);
            AmbientCGZipImporter.MaterialCreationMode oldMode = _mode;
            _creationModeLabels ??= new[] { "Default", "Smoothness in Albedo", "Single Map", "Include Height Map", "Height Map in MOS Blue" };
            _mode = (AmbientCGZipImporter.MaterialCreationMode) EditorGUILayout.Popup("Mode", (int) oldMode, _creationModeLabels);
            AmbientCGZipImporter.MaterialCreationMode mode = _mode;
            switch(mode)
            {
                case AmbientCGZipImporter.MaterialCreationMode.SMOOTHNESS_IN_ALBEDO:
                    if(_isHdrp)
                        EditorGUILayout.HelpBox("This mode doesn't work in HDRP", MessageType.Error);
                    else
                        EditorGUILayout.HelpBox("Occlusion and metalness maps will be disabled. It may look less detailed, " +
                        "but smoothness and normal maps are often enough. It won't work for stuff like painted or scratched " +
                        "metal, where there are both metallic and non-metallic parts. The memory requirement will be reduced, " +
                        "and performance will be better, so this is a good optimization for slightly bumpy dielectric " +
                        "materials like concrete or painted plaster. For very flat materials, consider the single map mode.", 
                        MessageType.Info);
                    break;
                case AmbientCGZipImporter.MaterialCreationMode.SINGLE_MAP:
                    if(_isHdrp)
                        EditorGUILayout.HelpBox("This mode doesn't work in HDRP", MessageType.Error);
                    else
                        EditorGUILayout.HelpBox("Normal, occlusion, and metalness maps will be disabled. This is usually " +
                        "fine for flat materials; the smoothness map can be good enough to provide lighting variety. It " +
                        "won't work for stuff like painted metal, where there are both metallic and non-metallic parts. " +
                        "The memory requirement will be greatly reduced and performance will be better, so this is a " +
                        "good optimization for materials like plastic, marble, wallpaper, sanded wood, and smooth metal.", 
                        MessageType.Info);
                    break;
                case AmbientCGZipImporter.MaterialCreationMode.INCLUDE_HEIGHT_MAP:
                    EditorGUILayout.HelpBox("Parallax occlusion mapping can be slow, especially on mobile, so only use " +
                    "it on materials that need it. Once the material is generated, adjust the height until it looks good. " +
                    "In HDRP, you will also need to set the height map mode. Vertex displacement (tesselation) looks " +
                    "better than pixel displacement (parallax), and may also perform better if you're memory-bandwidth-bound.",
                    MessageType.Info);
                    break;
                case AmbientCGZipImporter.MaterialCreationMode.HEIGHT_MAP_IN_BLUE:
                    EditorGUILayout.HelpBox("This is for custom shaders. Unless you have a special shader that knows " +
                    "about blue height maps, you should not use this mode.", MessageType.Warning);
                    break;
            }
            
            if(GUILayout.Button("Open AmbientCG"))
                Application.OpenURL("https://ambientcg.com/list");

            if(GUILayout.Button("Import ZIP File"))
            {
                string zipFile = EditorUtility.OpenFilePanel("Select ZIP File", "", "zip");
                if(zipFile != null && File.Exists(zipFile))
                    doImportZip(zipFile);
            }
        }

        private void doImportZip(string zipFile)
        {
            string outDir = getActiveFolderPath();
            string outName = Path.GetFileNameWithoutExtension(zipFile);
            Material material = AmbientCGZipImporter.importZipFile(zipFile, outDir, outName, _mode, _shader);
            ProjectWindowUtil.ShowCreatedAsset(material);
        }
        
        private static string getActiveFolderPath()
        {
            const BindingFlags bindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            MethodInfo getActiveFolderPath = typeof(ProjectWindowUtil).GetMethod("GetActiveFolderPath", bindingFlags)!;
            return (string) getActiveFolderPath.Invoke(null, Array.Empty<object>());
        }
    }
}
