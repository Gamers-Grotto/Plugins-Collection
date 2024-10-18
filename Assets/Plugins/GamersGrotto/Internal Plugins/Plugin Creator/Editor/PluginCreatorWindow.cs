namespace Editor.PluginCreator {
    using System.Collections.Generic;
    using System.IO;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;

    public class PluginCreatorWindow : EditorWindow {
        string pluginName = "NewPlugin";
        string companyName = "GamersGrotto";
        List<AssemblyDefinitionAsset> editorReferences = new List<AssemblyDefinitionAsset>();
        List<AssemblyDefinitionAsset> runtimeReferences = new List<AssemblyDefinitionAsset>();
        List<AssemblyDefinitionAsset> testsEditorReferences = new List<AssemblyDefinitionAsset>();
        List<AssemblyDefinitionAsset> testsRuntimeReferences = new List<AssemblyDefinitionAsset>();

        bool includeEditor, includeRuntime, includeTests;
        string version = "0.0.1";
        string outputDirectory = "Assets/Plugins/GamersGrotto/Modules/";
        string combinedOutputDirectoryAndName => Path.Combine(outputDirectory, pluginName);

        ReorderableList reorderablePluginCollection;
        ReorderableList editorReferencesList;
        ReorderableList runtimeReferencesList;
        ReorderableList testsEditorReferencesList;
        ReorderableList testsRuntimeReferencesList;
        bool showReferences = true;
        bool isExportPackage = false;

        public List<PluginCollection> pluginCollections;

        private ExportPackageOptions exportOptions = ExportPackageOptions.Recurse;


        [MenuItem("GamersGrotto/Plugin Creator")]
        public static void ShowWindow() {
            GetWindow<PluginCreatorWindow>("Plugin Creator");
        }

        void OnEnable() {
            // reset
            editorReferences.Clear();
            runtimeReferences.Clear();
            testsEditorReferences.Clear();
            testsRuntimeReferences.Clear();

            pluginCollections ??= new List<PluginCollection>();
            reorderablePluginCollection = CreateReorderableList(pluginCollections, "Plugin Collections");

            editorReferences.Add(
                AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(
                    "Assets/Plugins/GamersGrotto/Core/Editor/gamersgrotto.core.editor.asmdef"));
            editorReferences.Add(AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(
                "Assets/Plugins/GamersGrotto/Core/Runtime/gamersgrotto.core.runtime.asmdef"));
            runtimeReferences.Add(AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(
                "Assets/Plugins/GamersGrotto/Core/Runtime/gamersgrotto.core.runtime.asmdef"));
            testsEditorReferences.Add(
                AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(
                    "Assets/Plugins/GamersGrotto/Core/Editor/gamersgrotto.core.editor.asmdef"));
            testsEditorReferences.Add(
                AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(
                    "Assets/Plugins/GamersGrotto/Core/Runtime/gamersgrotto.core.runtime.asmdef"));
            testsRuntimeReferences.Add(
                AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(
                    "Assets/Plugins/GamersGrotto/Core/Runtime/gamersgrotto.core.runtime.asmdef"));

            editorReferencesList = CreateReorderableList(editorReferences, "Editor References");
            runtimeReferencesList = CreateReorderableList(runtimeReferences, "Runtime References");
            testsEditorReferencesList = CreateReorderableList(testsEditorReferences, "Tests Editor References");
            testsRuntimeReferencesList = CreateReorderableList(testsRuntimeReferences, "Tests Runtime References");
        }

        void OnGUI() {
            GUILayout.Label(isExportPackage ? "Package Creator" : "Plugin Creator", CustomEditorStyles.HeaderLabel);
            
            isExportPackage = EditorGUILayout.Toggle("Is Export Package", isExportPackage);
            
            pluginName = EditorGUILayout.TextField(isExportPackage ? "Package Name" : "Plugin Name", pluginName);

            companyName = EditorGUILayout.TextField("Company Name", companyName);
            version = EditorGUILayout.TextField("Version", version);

            if (!isExportPackage) {
                outputDirectory = EditorGUILayout.TextField("Output Directory", outputDirectory);
                if (GUILayout.Button("Set Output Directory", CustomEditorStyles.MediumButton)) {
                    SetOutputDirectory();
                }
            }
              
            
            if (isExportPackage) {
                exportOptions = (ExportPackageOptions)EditorGUILayout.EnumPopup("Package Options", exportOptions);
                var exportOptionsTooltip = GetExportOptionsTooltip(exportOptions);
                EditorGUILayout.HelpBox(exportOptionsTooltip, MessageType.Info);

                reorderablePluginCollection.DoLayoutList();
            }


            if (!isExportPackage) {
                DisplayReferences();

                if (GUILayout.Button("Create Plugin", CustomEditorStyles.LargeButton)) {
                    CreatePlugin();
                }
            }


            //  if (GUILayout.Button("Export Plugin", CustomEditorStyles.LargeButton)) {
            //      ExportPackage();
            //  }
//
            //  if (GUILayout.Button("Move Plugins Back", CustomEditorStyles.LargeButton)) {
            //      MovePluginsBack();
            //  }
//

            if (isExportPackage) {
                if (GUILayout.Button("Create, Move, Export, Move Back Plugins", CustomEditorStyles.LargeButton)) {
                    CreateExportAndMovePlugins();
                }
            }
        }

        void DisplayReferences() {
            includeEditor = EditorGUILayout.Toggle("Include Editor", includeEditor);
            includeRuntime = EditorGUILayout.Toggle("Include Runtime", includeRuntime);
            includeTests = EditorGUILayout.Toggle("Include Tests", includeTests);

            showReferences = EditorGUILayout.Foldout(showReferences, "References", true, CustomEditorStyles.Foldout);
            if (showReferences) {
                if (includeEditor) {
                    editorReferencesList.DoLayoutList();
                }

                if (includeRuntime) {
                    runtimeReferencesList.DoLayoutList();
                }

                if (includeTests) {
                    if (includeEditor) {
                        testsEditorReferencesList.DoLayoutList();
                    }

                    if (includeRuntime) {
                        testsRuntimeReferencesList.DoLayoutList();
                    }

                    if (!includeEditor && !includeRuntime) {
                        EditorGUILayout.HelpBox(
                            "Please select at least one of the following: Include Editor, Include Runtime",
                            MessageType.Error);
                    }
                }

                if (!includeEditor && !includeRuntime && !includeTests) {
                    EditorGUILayout.HelpBox(
                        "Please select at least one of the following: Include Editor, Include Runtime",
                        MessageType.Error);
                }
            }
        }

        void CreateExportAndMovePlugins() {
            CreatePlugin();
            ExportPackage();
            MovePluginsBack();
        }

        public void ExportPackage() {
            string packagePath = "Assets/Builds/";
            var fullPackagePath = Path.Combine(packagePath, pluginName + ".unitypackage").Replace(" ", "_");
            if (!Directory.Exists(Path.GetDirectoryName(fullPackagePath))) {
                Directory.CreateDirectory(Path.GetDirectoryName(fullPackagePath));
            }

            AssetDatabase.ExportPackage(combinedOutputDirectoryAndName, fullPackagePath, exportOptions);
            AssetDatabase.Refresh();
            Debug.Log("Package exported to: " + fullPackagePath);
        }

        ReorderableList CreateReorderableList(List<AssemblyDefinitionAsset> list, string title) {
            return new ReorderableList(list, typeof(AssemblyDefinitionAsset), true, true, true, true) {
                drawHeaderCallback = rect => EditorGUI.LabelField(rect, title),
                drawElementCallback = (rect, index, isActive, isFocused) => {
                    list[index] = (AssemblyDefinitionAsset)EditorGUI.ObjectField(
                        new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                        list[index],
                        typeof(AssemblyDefinitionAsset),
                        false
                    );
                },
                onAddCallback = reorderableList => { list.Add(null); }
            };
        }

        private ReorderableList CreateReorderableList(List<PluginCollection> list, string title) {
            return new ReorderableList(list, typeof(PluginCollection), true, true, true, true) {
                drawHeaderCallback = rect => EditorGUI.LabelField(rect, title),
                drawElementCallback = (rect, index, isActive, isFocused) => {
                    var element = list[index];
                    var fieldRect = new Rect(rect.x, rect.y, rect.width - 60, EditorGUIUtility.singleLineHeight);
                    var buttonRect = new Rect(rect.x + rect.width - 55, rect.y, 50, EditorGUIUtility.singleLineHeight);

                    list[index] =
                        (PluginCollection)EditorGUI.ObjectField(fieldRect, element, typeof(PluginCollection), false);
                },
                onAddCallback = reorderableList => list.Add(null)
            };
        }


        void SetOutputDirectory() {
            var folder = EditorUtility.OpenFolderPanel("Select Folder", "Assets/Plugins/GamersGrotto/", "");
            if (string.IsNullOrEmpty(folder))
                return;

            if (folder.StartsWith(Application.dataPath)) {
                folder = "Assets" + folder.Substring(Application.dataPath.Length);
                outputDirectory = folder;
            }
            else {
                Debug.LogError(
                    "Selected folder is outside the project. Please select a folder within the 'Assets' folder.");
            }
        }

        void CreatePlugin() {
            CreateDirectories();
            AssetDatabase.Refresh();
            CreateFiles();
            AssetDatabase.Refresh();
        }

        void CreateDirectories() {
            var fullPath = $"{outputDirectory}/{pluginName}";
            var editorPath = $"{fullPath}/Editor";
            var runtimePath = $"{fullPath}/Runtime";
            var testsPath = $"{fullPath}/Tests";
            var testsEditorPath = $"{testsPath}/Editor";
            var testsRuntimePath = $"{testsPath}/Runtime";

            if (!AssetDatabase.IsValidFolder(fullPath))
                AssetDatabase.CreateFolder(outputDirectory, pluginName);
            AssetDatabase.Refresh();

            if (isExportPackage) {
                MovePluginsTemporarily();
            }

            if (includeEditor && !AssetDatabase.IsValidFolder(editorPath))
                AssetDatabase.CreateFolder(fullPath, "Editor");

            if (includeRuntime && !AssetDatabase.IsValidFolder(runtimePath))
                AssetDatabase.CreateFolder(fullPath, "Runtime");

            if (includeTests) {
                if (!AssetDatabase.IsValidFolder(testsPath))
                    AssetDatabase.CreateFolder(fullPath, "Tests");
                if (includeEditor && !AssetDatabase.IsValidFolder(testsEditorPath))
                    AssetDatabase.CreateFolder(testsPath, "Editor");
                if (includeRuntime && !AssetDatabase.IsValidFolder(testsRuntimePath))
                    AssetDatabase.CreateFolder(testsPath, "Runtime");
            }
        }

        void MovePluginsTemporarily() {
            foreach (var pluginCollection in pluginCollections) {
                foreach (var folderPath in pluginCollection.pluginFolderPaths) {
                    var folderName = Path.GetFileName(folderPath);
                    var destinationPath = Path.Combine(outputDirectory, pluginName, folderName);

                    // Ensure the destination directory exists
                    var destinationDirectory = Path.GetDirectoryName(destinationPath);
                    if (!Directory.Exists(destinationDirectory)) {
                        Directory.CreateDirectory(destinationDirectory);
                    }

                    // Move the folder
                    try {
                        Debug.Log($"Moving folder from {folderPath} to {destinationPath}");
                        Directory.Move(folderPath, destinationPath);
                    }
                    catch (IOException ex) {
                        Debug.LogError($"Failed to move folder {folderPath} to {destinationPath}: {ex.Message}");
                    }
                }
            }
        }

        void CreateFiles() {
            var fullPath = $"{outputDirectory}/{pluginName}";
            var defaultPackage = new Package(companyName, pluginName, version);
            var defaultReadme = new Readme();
            var defaultChangeLog = new ChangeLog(version, "Initial release");
            var defaultLicense =
                new License(new ThirdParty("MIT License", "MIT", "https://opensource.org/licenses/MIT"));
            var exampleThirdParty = new ThirdParty();
            var defaultThirdPartyNotices = new ThirdPartyNotices(exampleThirdParty);

            var editorAssembly = new AssemblyDefinition(companyName, pluginName, false, true, editorReferences);
            var runtimeAssembly = new AssemblyDefinition(companyName, pluginName, false, false, runtimeReferences);
            var testsAssembly = new AssemblyDefinition(companyName, pluginName, true, false, testsEditorReferences);
            var testsEditorAssembly =
                new AssemblyDefinition(companyName, pluginName, true, true, testsRuntimeReferences);

            if (includeEditor && !isExportPackage)
                CreateFile(editorAssembly, Path.Combine(fullPath, "Editor"));
            if (includeRuntime && !isExportPackage)
                CreateFile(runtimeAssembly, Path.Combine(fullPath, "Runtime"));
            if (includeTests && !isExportPackage) {
                if (includeEditor)
                    CreateFile(testsAssembly, Path.Combine(fullPath, "Tests", "Runtime"));
                if (includeRuntime)
                    CreateFile(testsEditorAssembly, Path.Combine(fullPath, "Tests", "Editor"));
            }

            CreateFile(defaultPackage, fullPath);
            CreateFile(defaultReadme, fullPath);
            CreateFile(defaultChangeLog, fullPath);
            CreateFile(defaultLicense, fullPath);
            CreateFile(defaultThirdPartyNotices, fullPath);
            CreateFile(new Documentation(), fullPath);
        }

        void CreateFile<T>(T file, string path) where T : IFile {
            var fileCreator = new FileCreator<T>(file);
            fileCreator.CreateFile(path);
        }

        void MovePluginsBack() {
            if (isExportPackage) {
                // Delete existing folders
                foreach (var pluginCollection in pluginCollections) {
                    foreach (var folderPath in pluginCollection.pluginFolderPaths) {
                        //check if empty
                        if (Directory.Exists(folderPath) && Directory.GetFileSystemEntries(folderPath).Length == 0) {
                            Directory.Delete(folderPath, true);
                        }
                    }
                }

                // Move folders and their .meta files back
                foreach (var pluginCollection in pluginCollections) {
                    foreach (var folderPath in pluginCollection.pluginFolderPaths) {
                        var folderName = Path.GetFileName(folderPath);
                        var destinationPath = Path.Combine(outputDirectory, pluginName, folderName);
                        var metaFilePath = folderPath + ".meta";
                        var destinationMetaPath = destinationPath + ".meta";

                        // Move the folder
                        try {
                            Directory.Move(destinationPath, folderPath);
                            if (File.Exists(destinationMetaPath)) {
                                File.Move(destinationMetaPath, metaFilePath);
                            }
                        }
                        catch (IOException ex) {
                            Debug.LogError($"Failed to move folder {destinationPath} to {folderPath}: {ex.Message}");
                        }
                    }
                }

                AssetDatabase.Refresh();

                DeleteEmptyOutputFolder();
            }
        }

        void DeleteEmptyOutputFolder() {
            // Delete the output folder if it's empty
            //Delete metafile

            if (Directory.Exists(combinedOutputDirectoryAndName)) {
                Directory.Delete(combinedOutputDirectoryAndName, true);
                var metaFilePath = combinedOutputDirectoryAndName + ".meta";
                if (File.Exists(metaFilePath)) {
                    File.Delete(metaFilePath);
                }
            }

            AssetDatabase.Refresh();
        }

        private string GetExportOptionsTooltip(ExportPackageOptions options) {
            switch (options) {
                case ExportPackageOptions.Default:
                    return
                        "Will not include dependencies or subdirectories nor include Library assets unless specifically included in the asset list.";
                case ExportPackageOptions.Interactive:
                    return
                        "The export operation will be run asynchronously and reveal the exported package file in a file browser window after the export is finished.";
                case ExportPackageOptions.Recurse:
                    return "Will recurse through any subdirectories listed and include all assets inside them.";
                case ExportPackageOptions.IncludeDependencies:
                    return "In addition to the assets paths listed, all dependent assets will be included as well.";
                case ExportPackageOptions.IncludeLibraryAssets:
                    return
                        "The exported package will include all library assets, i.e., the project settings located in the Library folder of the project.";
                default:
                    return "Unknown option.";
            }
        }
    }
}