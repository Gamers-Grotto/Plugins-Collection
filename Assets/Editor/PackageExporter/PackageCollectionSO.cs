#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Editor;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPackageCollection", menuName = "GamersGrotto/Package Management/PackageCollection"),
 Serializable]
public class PackageCollection : ScriptableObject {
    public List<string> packageFolderPaths;
    public string packageName;

    [Button]
    public void AddFolder() {
        var folder = EditorUtility.OpenFolderPanel("Select Folder", "Assets/Plugins/GamersGrotto/", "");
        if (string.IsNullOrEmpty(folder))
            return;

        
        
        if (folder.StartsWith(Application.dataPath)) {
            folder = "Assets" + folder.Substring(Application.dataPath.Length);
            
            if (packageFolderPaths.Contains(folder)) {
                Debug.LogWarning("Folder already added to the package collection. Not adding again.");
                return;
            }
            
            packageFolderPaths.Add(folder);
        }
        else {
            Debug.LogError(
                "Selected folder is outside the project. Please select a folder within the 'Assets' folder.");
        }
    }

    [Button]
    public void BuildPackage() {
        PackageExporter.ExportPackage(packageFolderPaths,packageName:packageName);
    }

    [Button]
    public void OpenExporterWindow() {
        PackageExporterWindow.ShowWindow();
    }
}

#endif