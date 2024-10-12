using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(PackageCollection))]
    public class PackageCollectionEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var packageCollection = (PackageCollection)target;

            DrawDefaultInspector();

            if (GUILayout.Button("Add Folder"))
            {
                var folder = EditorUtility.OpenFolderPanel("Select Folder", "Assets", "");
                if (!string.IsNullOrEmpty(folder))
                {
                    if (folder.StartsWith(Application.dataPath))
                    {
                        folder = "Assets" + folder.Substring(Application.dataPath.Length);
                        packageCollection.packageFolderPaths.Add(folder);
                        EditorUtility.SetDirty(packageCollection);
                    }
                    else
                    {
                        Debug.LogError("Selected folder is outside the project. Please select a folder within 'Assets'.");
                    }
                }
            }

            if (GUILayout.Button("Clear Folders"))
            {
                packageCollection.packageFolderPaths.Clear();
                EditorUtility.SetDirty(packageCollection); 
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Export Package"))
                PackageExporter.ExportPackage(packageCollection.packageFolderPaths);
        }
    }
}