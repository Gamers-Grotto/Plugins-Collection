#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Editor {
    public class PackageExporterWindow : EditorWindow {
        private string packagePath = "Assets/Builds/";
        private string packageName = "ExportedPackage";
        private ExportPackageOptions exportOptions = ExportPackageOptions.Default;

        public PluginCollection pluginCollection;

        [MenuItem("GamersGrotto/Package Exporter Window")]
        public static void ShowWindow() {
            GetWindow<PackageExporterWindow>("Package Exporter");
        }

        private void OnGUI() {
            GUILayout.Label("Package Exporter", EditorStyles.boldLabel);

            pluginCollection =
                EditorGUILayout.ObjectField("Package Collection", pluginCollection, typeof(PluginCollection), false)
                    as PluginCollection;

            if (pluginCollection == null)
            {
                if (GUILayout.Button("Create New Plugin Collection"))
                    CreateNewPluginCollection();
            }
            
            GUILayout.Space(10);

            SelectSaveDestination();

            GUILayout.Space(10);
            
            SelectPackageName();
            
            GUILayout.Space(10);

            GUILayout.Label("Export Package Options:", EditorStyles.label);
            exportOptions = (ExportPackageOptions)EditorGUILayout.EnumPopup("Package Options", exportOptions);

            if (GUILayout.Button("Export Package"))
            {
                if(pluginCollection != null)
                    PackageExporter.ExportPackage(pluginCollection.pluginFolderPaths, packagePath, packageName,exportOptions);
                else 
                    Debug.LogError("No Package Collection selected");
            }
        }

        private void SelectSaveDestination() {
            GUILayout.Label("Output Package Path:", EditorStyles.label);

            packagePath = EditorGUILayout.TextField(packagePath);

            if (GUILayout.Button("Choose Package Destination")) {
                var path = EditorUtility.SaveFilePanel("Save Unity Package", "Assets/", "NewPackage", "unitypackage");
                if (!string.IsNullOrEmpty(path)) {
                    packagePath = path;

                    if (path.StartsWith(Application.dataPath))
                        packagePath = "Assets" + path.Substring(Application.dataPath.Length);
                }
            }
        }
        
        private void SelectPackageName() {
            GUILayout.Label("Output Package Name:", EditorStyles.label);

            packageName = EditorGUILayout.TextField(packageName);
        }
        
        private void CreateNewPluginCollection()
        {
            var path = EditorUtility.SaveFilePanelInProject("Save New Plugin Collection", "NewPluginCollection", "asset", "Please enter a file name to save the package collection");

            if (string.IsNullOrEmpty(path)) 
                return;
            
            var newPluginCollection = CreateInstance<PluginCollection>();
            AssetDatabase.CreateAsset(newPluginCollection, path);
            AssetDatabase.SaveAssets();

            pluginCollection = AssetDatabase.LoadAssetAtPath<PluginCollection>(path);
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = pluginCollection;
        }
    }
}
#endif