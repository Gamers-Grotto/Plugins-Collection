using UnityEditor;
using UnityEngine;

namespace Editor {
    public class PackageExporterWindow : EditorWindow {
        private string packagePath = "Assets/Builds/ExportedPackage.unitypackage";
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
            GUILayout.Space(10);

            SelectSaveDestination();

            GUILayout.Space(10);

            GUILayout.Label("Export Package Options:", EditorStyles.label);
            exportOptions = (ExportPackageOptions)EditorGUILayout.EnumPopup("Package Options", exportOptions);

            if (GUILayout.Button("Export Package"))
                PackageExporter.ExportPackage(packageCollection.packageFolderPaths, packagePath, exportOptions);
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
    }
}