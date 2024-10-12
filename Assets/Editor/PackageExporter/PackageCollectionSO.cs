#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPackageCollection", menuName = "GamersGrotto/Package Management/PackageCollection"),
 Serializable]
public class PackageCollection : ScriptableObject {
    public List<string> packageFolderPaths;

    [Button]
    public void BuildPackage() {
        PackageExporter.ExportPackage(packageFolderPaths);
    }

    [Button]
    public void AddFolder() {
        var folder = EditorUtility.OpenFolderPanel("Select Folder", "Assets", "");
        if (string.IsNullOrEmpty(folder))
            return;

        if (folder.StartsWith(Application.dataPath)) {
            folder = "Assets" + folder.Substring(Application.dataPath.Length);
            packageFolderPaths.Add(folder);
        }
        else {
            Debug.LogError(
                "Selected folder is outside the project. Please select a folder within the 'Assets' folder.");
        }
    }
}

#endif