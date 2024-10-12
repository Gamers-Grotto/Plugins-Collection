using System.Collections.Generic;
using System.IO;
using Editor.PackageCreator;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPackageCreatorSO", menuName = "GamersGrotto/Package Management/PackageCreatorSO")]
public class PackageCreatorSO : ScriptableObject {
    [SerializeField] string packageName = "NewPackage";
    [SerializeField] string companyName = "GamersGrotto";
    [SerializeField] List<AssemblyDefinitionAsset> editorReferences;
    [SerializeField] List<AssemblyDefinitionAsset> runtimeReferences;
    [SerializeField] List<AssemblyDefinitionAsset> testsEditorReferences;
    [SerializeField] List<AssemblyDefinitionAsset> testsRuntimeReferences;

    [SerializeField] bool IncludeEditor, IncludeRuntime, IncludeTests;
    [SerializeField] string version = "0.0.1";

    public string outputDirectory = "Assets/Plugins/GamersGrotto/";

    string FullName => $"{companyName}.{packageName}".ToLower().Replace(" ", "_").Replace("-", "_");
    string FullPath => $"{outputDirectory}/{packageName}";
    public void CreateDirectories() {
        
        var editorPath = $"{FullPath}/Editor";
        var runtimePath = $"{FullPath}/Runtime";
        var testsPath = $"{FullPath}/Tests";
        var testsEditorPath = $"{testsPath}/Editor";
        var testsRuntimePath = $"{testsPath}/Runtime";
        
        if(!AssetDatabase.IsValidFolder(FullPath))
            AssetDatabase.CreateFolder(outputDirectory, packageName);
        
        if (IncludeEditor) {
            if (!AssetDatabase.IsValidFolder(editorPath)) {
                AssetDatabase.CreateFolder(FullPath, "Editor");
            }
        }
        if (IncludeRuntime) {
            if (!AssetDatabase.IsValidFolder(runtimePath)) {
                AssetDatabase.CreateFolder(FullPath, "Runtime");
            }
        }
        
        if (IncludeTests) {
            if (!AssetDatabase.IsValidFolder(testsPath)) {
                AssetDatabase.CreateFolder(FullPath, "Tests");
            }
            if (IncludeEditor) {
                if (!AssetDatabase.IsValidFolder(testsEditorPath)) {
                    AssetDatabase.CreateFolder(testsPath, "Editor");
                }
            }
            if (IncludeRuntime) {
                if (!AssetDatabase.IsValidFolder(testsRuntimePath)) {
                    AssetDatabase.CreateFolder(testsPath, "Runtime");
                }
            }
        }
        
    }

    public void CreateFiles() {
        
        var defaultPackage = new Package(companyName, packageName, version);
        var defaultReadme = new Readme();
        var defaultChangeLog = new ChangeLog(version, "Initial release");
        var defaultLicense = new License(new ThirdParty("MIT License", "MIT", "https://opensource.org/licenses/MIT"));
        var exampleThirdParty = new ThirdParty();
        var defaultThirdPartyNotices = new ThirdPartyNotices(exampleThirdParty);
        
        var editorAssembly = new AssemblyDefinition(companyName, packageName, false, true, editorReferences);
        var runtimeAssembly = new AssemblyDefinition(companyName, packageName, false, false, runtimeReferences);
        var testsAssembly = new AssemblyDefinition(companyName, packageName, true, false, testsEditorReferences);
        var testsEditorAssembly = new AssemblyDefinition(companyName, packageName, true, true, testsRuntimeReferences);

        if( IncludeEditor)
            CreateFile(editorAssembly, Path.Combine(FullPath, "Editor"));
        if( IncludeRuntime)
            CreateFile(runtimeAssembly, Path.Combine(FullPath, "Runtime"));
        if (IncludeTests) {
            if( IncludeEditor)
                CreateFile(testsAssembly, Path.Combine(FullPath, "Tests", "Runtime"));
            if( IncludeRuntime)
                CreateFile(testsEditorAssembly, Path.Combine(FullPath, "Tests", "Editor"));
        }
            
        
        CreateFile(defaultPackage, FullPath);
        CreateFile(defaultReadme, FullPath);
        CreateFile(defaultChangeLog, FullPath);
        CreateFile(defaultLicense, FullPath);
        CreateFile(defaultThirdPartyNotices, FullPath);
        CreateFile(new Documentation(), FullPath);
    }

     void CreateFile<T>(T file, string path) where T : IFile
    {
        var fileCreator = new FileCreator<T>(file);
        fileCreator.CreateFile(path);
    }
    [Button]
    public void CreatePackage() {
        CreateDirectories();
        CreateFiles();
        AssetDatabase.Refresh();
    }
    
    [Button]
    public void SetOutputDirectory() {
        
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
}