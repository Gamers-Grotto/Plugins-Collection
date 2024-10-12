#if UNITY_EDITOR

using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public static class PackageExporter {
   
    public static void ExportPackage(List<string> foldersToInclude, string packagePath = "Assets/Builds/",string packageName = "ExportedPackage", ExportPackageOptions exportOptions = ExportPackageOptions.Default)
    {
        if (foldersToInclude == null || foldersToInclude.Count == 0)
        {
            Debug.LogError("No folders specified for export.");
            return;
        }

        var assetPaths = GetAllAssetPathsFromFolders(foldersToInclude);

        if (assetPaths.Length == 0)
        {
            Debug.LogWarning("No assets found in the specified folders.");
            return;
        }
        
        var fullPackagePath = Path.Combine(packagePath, packageName + ".unitypackage").Replace(" ", "_");
        if (!Directory.Exists(Path.GetDirectoryName(fullPackagePath)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPackagePath));
        }

        AssetDatabase.ExportPackage(assetPaths, fullPackagePath, exportOptions);
        AssetDatabase.Refresh();
        Debug.Log("Package exported to: " + fullPackagePath);
    }

    private static string[] GetAllAssetPathsFromFolders(List<string> folders)
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
#endif