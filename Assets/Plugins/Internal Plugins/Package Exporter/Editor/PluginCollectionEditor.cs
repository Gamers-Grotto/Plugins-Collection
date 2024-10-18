using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(PluginCollection))]
    public class PluginCollectionEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var pluginCollection = (PluginCollection)target;

            DrawDefaultInspector();

            if (GUILayout.Button("Add Folder"))
            {
                var folder = EditorUtility.OpenFolderPanel("Select Folder", "Assets/Plugins/GamersGrotto", "");
                if (!string.IsNullOrEmpty(folder))
                {
                    if (folder.StartsWith(Application.dataPath))
                    {
                        folder = "Assets" + folder.Substring(Application.dataPath.Length);
                        pluginCollection.pluginFolderPaths.Add(folder);
                        EditorUtility.SetDirty(pluginCollection);
                    }
                    else
                    {
                        Debug.LogError("Selected folder is outside the project. Please select a folder within 'Assets'.");
                    }
                }
            }

            if (GUILayout.Button("Clear Folders"))
            {
                pluginCollection.pluginFolderPaths.Clear();
                EditorUtility.SetDirty(pluginCollection); 
            }

            GUILayout.Space(10);

            if (GUILayout.Button("Export Package"))
                PackageExporter.ExportPackage(pluginCollection.pluginFolderPaths,packageName: pluginCollection.packageName);
        }
    }
}