using System.Linq;

namespace Editor.PackageCreator {
    using System.Collections.Generic;
    using System.IO;
    using UnityEditor;
    using UnityEditorInternal;
    using UnityEngine;

    public class PackageCreatorWindow : EditorWindow {
        string packageName = "NewPackage";
        string companyName = "GamersGrotto";
        List<AssemblyDefinitionAsset> editorReferences = new List<AssemblyDefinitionAsset>();
        List<AssemblyDefinitionAsset> runtimeReferences = new List<AssemblyDefinitionAsset>();
        List<AssemblyDefinitionAsset> testsEditorReferences = new List<AssemblyDefinitionAsset>();
        List<AssemblyDefinitionAsset> testsRuntimeReferences = new List<AssemblyDefinitionAsset>();

        bool includeEditor, includeRuntime, includeTests;
        string version = "0.0.1";
        string outputDirectory = "Assets/Plugins/GamersGrotto/Modules/";

        ReorderableList editorReferencesList;
        ReorderableList runtimeReferencesList;
        ReorderableList testsEditorReferencesList;
        ReorderableList testsRuntimeReferencesList;

        bool showReferences = true;
        

        [MenuItem("GamersGrotto/Package Creator")]
        public static void ShowWindow() {
            GetWindow<PackageCreatorWindow>("Package Creator");
        }

        void OnEnable() {
            // reset
            editorReferences.Clear();
            runtimeReferences.Clear();
            testsEditorReferences.Clear();
            testsRuntimeReferences.Clear();
            
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
            GUILayout.Label("Package Settings", EditorStyles.boldLabel);

            packageName = EditorGUILayout.TextField("Package Name", packageName);
            companyName = EditorGUILayout.TextField("Company Name", companyName);
            version = EditorGUILayout.TextField("Version", version);
            outputDirectory = EditorGUILayout.TextField("Output Directory", outputDirectory);
            if (GUILayout.Button("Set Output Directory")) {
                SetOutputDirectory();
            }

            includeEditor = EditorGUILayout.Toggle("Include Editor", includeEditor);
            includeRuntime = EditorGUILayout.Toggle("Include Runtime", includeRuntime);
            includeTests = EditorGUILayout.Toggle("Include Tests", includeTests);
            
            var foldoutStyle = new GUIStyle(EditorStyles.foldout) {
                fontStyle = FontStyle.Bold,
                fontSize = 16,
                normal = {
                    textColor = Color.white
                }
            };
            showReferences = EditorGUILayout.Foldout(showReferences, "References", true, foldoutStyle); 
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

           

            if (GUILayout.Button("Create Package", new GUIStyle(GUI.skin.button) { fontSize = 16 , fontStyle = FontStyle.Bold, fixedHeight = 50})) {
                CreatePackage();
            }
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

        void CreatePackage() {
            CreateDirectories();
            CreateFiles();
        }

        void CreateDirectories() {
            var fullPath = $"{outputDirectory}/{packageName}";
            var editorPath = $"{fullPath}/Editor";
            var runtimePath = $"{fullPath}/Runtime";
            var testsPath = $"{fullPath}/Tests";
            var testsEditorPath = $"{testsPath}/Editor";
            var testsRuntimePath = $"{testsPath}/Runtime";

            if (!AssetDatabase.IsValidFolder(fullPath))
                AssetDatabase.CreateFolder(outputDirectory, packageName);

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

        void CreateFiles() {
            var fullPath = $"{outputDirectory}/{packageName}";
            var defaultPackage = new Package(companyName, packageName, version);
            var defaultReadme = new Readme();
            var defaultChangeLog = new ChangeLog(version, "Initial release");
            var defaultLicense =
                new License(new ThirdParty("MIT License", "MIT", "https://opensource.org/licenses/MIT"));
            var exampleThirdParty = new ThirdParty();
            var defaultThirdPartyNotices = new ThirdPartyNotices(exampleThirdParty);

            var editorAssembly = new AssemblyDefinition(companyName, packageName, false, true, editorReferences);
            var runtimeAssembly = new AssemblyDefinition(companyName, packageName, false, false, runtimeReferences);
            var testsAssembly = new AssemblyDefinition(companyName, packageName, true, false, testsEditorReferences);
            var testsEditorAssembly =
                new AssemblyDefinition(companyName, packageName, true, true, testsRuntimeReferences);

            if (includeEditor)
                CreateFile(editorAssembly, Path.Combine(fullPath, "Editor"));
            if (includeRuntime)
                CreateFile(runtimeAssembly, Path.Combine(fullPath, "Runtime"));
            if (includeTests) {
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
    }
}