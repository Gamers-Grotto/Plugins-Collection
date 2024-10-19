#if UNITY_EDITOR
using System.Collections.Generic;
using GamersGrotto.Custom_Editor_Styles.Editor;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace GamersGrotto.Package_Exporter.Editor {
    public class PackageExporterWindow : EditorWindow {
        private string packagePath = "Assets/Builds/";
        private ExportPackageOptions exportOptions = ExportPackageOptions.Recurse;
        public List<PluginCollection> pluginCollections;
        ReorderableList reorderablePluginCollection;
        const string createPluginCollectionPath = "Assets/Plugins/GamersGrotto/Internal Plugins/Package Exporter/Editor/Plugin Collections";

        private string exportOptionsTooltip;

        [MenuItem("GamersGrotto/Package Exporter Window")]
        public static void ShowWindow() {
            GetWindow<PackageExporterWindow>("Package Exporter");
        }

        private void OnEnable() {
            pluginCollections ??= new List<PluginCollection>();
            reorderablePluginCollection = CreateReorderableList(pluginCollections, "Plugin Collections");
        }

        private void OnGUI() {
            GUILayout.Label("Package Exporter", CustomEditorStyles.HeaderLabel);
            reorderablePluginCollection.DoLayoutList();

            if (position.width > 600) {
                GUILayout.BeginHorizontal();
            } else {
                GUILayout.BeginVertical();
            }

            if (GUILayout.Button("Add All Plugin Collections", CustomEditorStyles.MediumButton)) {
                FindAllPluginCollections();
            }
            if (GUILayout.Button("Create New Plugin Collection", CustomEditorStyles.MediumButton)) {
                CreateNewPluginCollection();
            }
            if (GUILayout.Button("Clear Plugin Collections", CustomEditorStyles.MediumButton)) {
                pluginCollections.Clear();
            }

            if (position.width > 600) {
                GUILayout.EndHorizontal();
            } else {
                GUILayout.EndVertical();
            }

            GUILayout.Space(10);
            SelectSaveDestination();
            GUILayout.Space(10);
            
            exportOptionsTooltip = GetExportOptionsTooltip(exportOptions);
            EditorGUILayout.HelpBox(exportOptionsTooltip, MessageType.Info);
            exportOptions = (ExportPackageOptions)EditorGUILayout.EnumPopup("Package Options", exportOptions);

            var buttonText = pluginCollections.Count > 1 ? "Export Packages" : "Export Package";
            if (GUILayout.Button(buttonText,CustomEditorStyles.LargeButton)) {
                ExportSelectedPackages();
            }
        }

        private string GetExportOptionsTooltip(ExportPackageOptions options) {
            switch (options) {
                case ExportPackageOptions.Default:
                    return "Will not include dependencies or subdirectories nor include Library assets unless specifically included in the asset list.";
                case ExportPackageOptions.Interactive:
                    return "The export operation will be run asynchronously and reveal the exported package file in a file browser window after the export is finished.";
                case ExportPackageOptions.Recurse:
                    return "Will recurse through any subdirectories listed and include all assets inside them.";
                case ExportPackageOptions.IncludeDependencies:
                    return "In addition to the assets paths listed, all dependent assets will be included as well.";
                case ExportPackageOptions.IncludeLibraryAssets:
                    return "The exported package will include all library assets, i.e., the project settings located in the Library folder of the project.";
                default:
                    return "Unknown option.";
            }
        }

        private void FindAllPluginCollections() {
            var guids = AssetDatabase.FindAssets("t:PluginCollection");
            foreach (var guid in guids) {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var pluginCollection = AssetDatabase.LoadAssetAtPath<PluginCollection>(path);
                if (pluginCollection != null) {
                    AddPluginCollection(pluginCollection);
                }
            }

            EditorUtility.FocusProjectWindow();
            if (pluginCollections.Count > 0) {
                Selection.activeObject = pluginCollections[^1];
            }
        }

        private void AddPluginCollection(PluginCollection pluginCollection) {
            if (pluginCollections.Contains(pluginCollection)) {
                Debug.LogWarning($"Plugin Collection {pluginCollection.packageName} already added to the list. Not adding again.");
                return;
            }
            pluginCollections.Add(pluginCollection);
        }

        private void SelectSaveDestination() {
            GUILayout.Label("Output Package Path:", CustomEditorStyles.SubHeaderLabel);
            packagePath = EditorGUILayout.TextField(packagePath);

            if (GUILayout.Button("Choose Package Destination", CustomEditorStyles.MediumButton)) {
                var path = EditorUtility.SaveFilePanel("Save Unity Package", "Assets/", "NewPackage", "unitypackage");
                if (!string.IsNullOrEmpty(path)) {
                    packagePath = path.StartsWith(Application.dataPath) ? "Assets" + path.Substring(Application.dataPath.Length) : path;
                }
            }
        }

        private ReorderableList CreateReorderableList(List<PluginCollection> list, string title) {
            return new ReorderableList(list, typeof(PluginCollection), true, true, true, true) {
                drawHeaderCallback = rect => EditorGUI.LabelField(rect, title),
                drawElementCallback = (rect, index, isActive, isFocused) => {
                    var element = list[index];
                    var fieldRect = new Rect(rect.x, rect.y, rect.width - 60, EditorGUIUtility.singleLineHeight);
                    var buttonRect = new Rect(rect.x + rect.width - 55, rect.y, 50, EditorGUIUtility.singleLineHeight);

                    list[index] = (PluginCollection)EditorGUI.ObjectField(fieldRect, element, typeof(PluginCollection), false);

                    if (GUI.Button(buttonRect, "Export", CustomEditorStyles.BasicButton)) {
                        PackageExporter.ExportPackage(element.pluginFolderPaths, packagePath, element.packageName, exportOptions);
                    }
                },
                onAddCallback = reorderableList => list.Add(null)
            };
        }

        private void CreateNewPluginCollection() {
            var path = EditorUtility.SaveFilePanelInProject("Save New Plugin Collection", "NewPluginCollection", "asset", "Please enter a file name to save the package collection", createPluginCollectionPath);
            if (string.IsNullOrEmpty(path)) return;

            var newPluginCollection = CreateInstance<PluginCollection>();
            AssetDatabase.CreateAsset(newPluginCollection, path);
            AssetDatabase.SaveAssets();

            pluginCollections.Add(AssetDatabase.LoadAssetAtPath<PluginCollection>(path));
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = pluginCollections[^1];
        }

        private void ExportSelectedPackages() {
            if (pluginCollections == null) {
                Debug.LogError("No Package Collection selected");
                return;
            }

            foreach (var collection in pluginCollections) {
                PackageExporter.ExportPackage(collection.pluginFolderPaths, packagePath, collection.packageName, exportOptions);
            }
        }
    }
}
#endif