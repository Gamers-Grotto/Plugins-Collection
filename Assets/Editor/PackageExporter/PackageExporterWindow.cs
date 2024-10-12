#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Editor {
    public class PackageExporterWindow : EditorWindow {
        private string packagePath = "Assets/Builds/";
        private string packageName = "ExportedPackage";
        private ExportPackageOptions exportOptions = ExportPackageOptions.Default;

        public PackageCollection packageCollection;

        [MenuItem("GamersGrotto/Package Exporter Window")]
        public static void ShowWindow() {
            GetWindow<PackageExporterWindow>("Package Exporter");
        }

        private void OnGUI() {
            GUILayout.Label("Package Exporter", EditorStyles.boldLabel);

            packageCollection =
                EditorGUILayout.ObjectField("Package Collection", packageCollection, typeof(PackageCollection), false)
                    as PackageCollection;

            if (packageCollection == null)
            {
                if (GUILayout.Button("Create New Package Collection"))
                    CreateNewPackageCollection();
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
                if(packageCollection != null)
                    PackageExporter.ExportPackage(packageCollection.packageFolderPaths, packagePath, packageName,exportOptions);
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
        
        private void CreateNewPackageCollection()
        {
            var path = EditorUtility.SaveFilePanelInProject("Save New Package Collection", "NewPackageCollection", "asset", "Please enter a file name to save the package collection");

            if (string.IsNullOrEmpty(path)) 
                return;
            
            var newPackageCollection = CreateInstance<PackageCollection>();
            AssetDatabase.CreateAsset(newPackageCollection, path);
            AssetDatabase.SaveAssets();

            packageCollection = AssetDatabase.LoadAssetAtPath<PackageCollection>(path);
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = packageCollection;
        }
    }
}
#endif