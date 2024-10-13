#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Attributes;
using Editor;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPackageCollection", menuName = "GamersGrotto/Plugin Management/PluginCollection"),
 Serializable]
public class PluginCollection : ScriptableObject {
    [Tooltip("Default: ExportedPackage. Empty also defaults it. Spaces -> _")] 
    public string packageName =  "ExportedPackage";
    public List<string> pluginFolderPaths;


    [Button]
    public void AddFolder() {
        var folder = EditorUtility.OpenFolderPanel("Select Folder", "Assets/Plugins/GamersGrotto/", "");
        if (string.IsNullOrEmpty(folder))
            return;


        if (folder.StartsWith(Application.dataPath)) {
            folder = "Assets" + folder.Substring(Application.dataPath.Length);

            if (pluginFolderPaths.Contains(folder)) {
                Debug.LogWarning("Folder already added to the Plugin collection. Not adding again.");
                return;
            }

            pluginFolderPaths.Add(folder);
        }
        else {
            Debug.LogError(
                "Selected folder is outside the project. Please select a folder within the 'Assets' folder.");
        }
    }

    [Button]
    public void BuildPackage() {
        PackageExporter.ExportPackage(pluginFolderPaths, packageName: packageName);
    }

    [Button]
    public void OpenExporterWindow() {
        PackageExporterWindow.ShowWindow();
    }
}

#endif