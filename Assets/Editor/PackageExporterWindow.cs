using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class PackageExporterWindow : EditorWindow
    {
        private string packagePath = "Assets/Builds/ExportedPackage.unitypackage";
        private ExportPackageOptions exportOptions = ExportPackageOptions.Default;
        
        private List<string> foldersToInclude = new ();

        [MenuItem("GamersGrotto/Package Exporter Window")]
        public static void ShowWindow()
        {
            GetWindow<PackageExporterWindow>("Package Exporter");
        }

        private void OnGUI()
        {
            GUILayout.Label("Package Exporter", EditorStyles.boldLabel);

            SelectFolders();
            
            GUILayout.Space(10);
            
            SelectSaveDestination();
            
            GUILayout.Space(10);
            
            GUILayout.Label("Export Package Options:", EditorStyles.label);
            exportOptions = (ExportPackageOptions)EditorGUILayout.EnumPopup("Package Options", exportOptions);
            
            if (GUILayout.Button("Export Package"))
                ExportPackage();
        }

        private void SelectSaveDestination()
        {
            GUILayout.Label("Output Package Path:", EditorStyles.label);
            
            packagePath = EditorGUILayout.TextField(packagePath);

            if (GUILayout.Button("Choose Package Destination"))
            {
                var path = EditorUtility.SaveFilePanel("Save Unity Package", "Assets/", "NewPackage", "unitypackage");
                if (!string.IsNullOrEmpty(path))
                {
                    packagePath = path;

                    if (path.StartsWith(Application.dataPath))
                        packagePath = "Assets" + path.Substring(Application.dataPath.Length);
                }
            }
        }

        private void SelectFolders()
        {
            GUILayout.Label("Folders to Include:", EditorStyles.label);
            
            for (int i = 0; i < foldersToInclude.Count; i++)
            {
                GUILayout.BeginHorizontal();
                
                foldersToInclude[i] = EditorGUILayout.TextField(foldersToInclude[i]);

                if (GUILayout.Button("-", GUILayout.Width(25)))
                    foldersToInclude.RemoveAt(i);
                
                GUILayout.EndHorizontal();
            }

            if (GUILayout.Button("Add Folder"))
            {
                var folder = EditorUtility.OpenFolderPanel("Select Folder", "Assets", "");
                if (string.IsNullOrEmpty(folder)) 
                    return;
                
                if (folder.StartsWith(Application.dataPath))
                {
                    folder = "Assets" + folder.Substring(Application.dataPath.Length);
                    foldersToInclude.Add(folder);
                }
                else
                {
                    Debug.LogError("Selected folder is outside the project. Please select a folder within the 'Assets' folder.");
                }
            }
        }

        private void ExportPackage()
        {
            if (foldersToInclude == null || foldersToInclude.Count == 0)
            {
                Debug.LogError("No folders specified for export.");
                return;
            }

            var assetPaths = GetAllAssetPathsFromFolders(foldersToInclude.ToArray());

            if (assetPaths.Length == 0)
            {
                Debug.LogWarning("No assets found in the specified folders.");
                return;
            }

            AssetDatabase.ExportPackage(assetPaths, packagePath, exportOptions);
            AssetDatabase.Refresh();
            Debug.Log("Package exported to: " + packagePath);
        }

        private string[] GetAllAssetPathsFromFolders(string[] folders)
        {
            var allAssetPaths = new List<string>();

            foreach (var folder in folders)
            {
                if (!AssetDatabase.IsValidFolder(folder))
                {
                    Debug.LogError($"Invalid folder path: {folder}");
                    continue;
                }

                var fileEntries = Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories);

                allAssetPaths.AddRange(fileEntries.Select(file => file.Replace('\\', '/')));
            }

            Debug.Log($"Total asset paths found: {allAssetPaths.Count}");

            return allAssetPaths.ToArray();
        }
    }
}
