using CustomPluginInitializer;
using CustomPluginInitializer.Interfaces;

class Program
{
    static string basePath = @"D:\Projects\Unity\Gamers' Grotto\Plugins-Collection\Assets\Plugins\GamersGrotto\Core/";
    static string companyName = "gamersgrotto";
    static string packageName = "Extended Attributes"; // Gets converted to lowercase // Space or dash will be replaced with underscore
    static string[] editorReferences = new string[] { "gamersgrotto.core.runtime", "gamersgrotto.core.editor" };
    static string[] runtimeReferences = new string[] { "gamersgrotto.core.runtime" };
    static string[] testsEditorReferences = new string[] { };
    static string[] testsRuntimeReferences = new string[] { };
    static string version = "0.0.1";

    static string FullName => $"{companyName}.{packageName}".ToLower().Replace(" ", "_").Replace("-", "_");
    static string FullPath => $"{basePath}{FullName}".ToLower();

    static void Main(string[] args)
    {
        CreateDirectories();
        CreateFiles();
    }

    static void CreateDirectories()
    {
        var editorDirectory = Path.Combine(FullPath, "Editor");
        var runtimeDirectory = Path.Combine(FullPath, "Runtime");
        var testsDirectory = Path.Combine(FullPath, "Tests");
        var testsEditorDirectory = Path.Combine(testsDirectory, "Editor");
        var testsRuntimeDirectory = Path.Combine(testsDirectory, "Runtime");

        Directory.CreateDirectory(Path.Combine(FullPath, "Samples"));
        Directory.CreateDirectory(editorDirectory);
        Directory.CreateDirectory(runtimeDirectory);
        Directory.CreateDirectory(testsDirectory);
        Directory.CreateDirectory(testsEditorDirectory);
        Directory.CreateDirectory(testsRuntimeDirectory);
    }

    static void CreateFiles()
    {
        var defaultPackage = new Package(companyName, packageName, version);
        var defaultReadme = new Readme();
        var defaultChangeLog = new ChangeLog(version, "Initial release");
        var defaultLicense = new License();
        var exampleThirdParty = new ThirdParty();
        var defaultThirdPartyNotices = new ThirdPartyNotices(exampleThirdParty);

        var editorAssembly = new AssemblyDefinition(companyName, packageName, false, true, editorReferences);
        var runtimeAssembly = new AssemblyDefinition(companyName, packageName, false, false, runtimeReferences);
        var testsAssembly = new AssemblyDefinition(companyName, packageName, true, false, testsEditorReferences);
        var testsEditorAssembly = new AssemblyDefinition(companyName, packageName, true, true, testsRuntimeReferences);

        CreateFile(defaultPackage, FullPath);
        CreateFile(defaultReadme, FullPath);
        CreateFile(defaultChangeLog, FullPath);
        CreateFile(defaultLicense, FullPath);
        CreateFile(defaultThirdPartyNotices, FullPath);

        CreateFile(editorAssembly, Path.Combine(FullPath, "Editor"));
        CreateFile(runtimeAssembly, Path.Combine(FullPath, "Runtime"));
        CreateFile(testsAssembly, Path.Combine(FullPath, "Tests", "Runtime"));
        CreateFile(testsEditorAssembly, Path.Combine(FullPath, "Tests", "Editor"));
        CreateFile(new Documentation(), FullPath);
    }

    static void CreateFile<T>(T file, string path) where T : IFile
    {
        var fileCreator = new FileCreator<T>(file);
        fileCreator.CreateFile<T>(path);
    }

    public void DeleteCurrentPackage()
    {
        Directory.Delete(FullPath, true);
    }
}