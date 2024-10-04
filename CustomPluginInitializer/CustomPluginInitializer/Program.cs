using CustomPluginInitializer;

class Program
{
    static string basePath = @"D:\Projects\Unity\CustomPlugins\Packages\";
    static string companyName = "partisanprogrammer";
    static string packageName = "Better-Singletons"; //Avoid spaces, dashes
    static string version = "0.0.1";

    static string FullName => $"{companyName}.{packageName}".ToLower();
    static string FullPath => $"{basePath}{FullName}".ToLower();
    static void Main(string[] args)
    {
        
        var defaultPackage = new Package(companyName, packageName,version);
        var defaultReadme = new Readme();
        var defaultChangeLog = new ChangeLog( version, "Initial release");
        var defaultLicense = new License();
        var partisanThirdParty = new ThirdParty();
        var defaultThirdPartyNotices = new ThirdPartyNotices(partisanThirdParty);
        
        var editorAssembly = new AssemblyDefinition(companyName, packageName, false, true);
        var runtimeAssembly = new AssemblyDefinition(companyName, packageName, false, false);
        var testsAssembly = new AssemblyDefinition(companyName, packageName, true, false);
        var testsEditorAssembly = new AssemblyDefinition(companyName, packageName, true, true);
        
        var editorDirectory = Path.Combine(FullPath, "Editor");
        var runtimeDirectory = Path.Combine(FullPath, "Runtime");
        var testsDirectory = Path.Combine(FullPath, "Tests");
        var testsEditorDirectory = Path.Combine(testsDirectory, "Editor");
        var testsRuntimeDirectory = Path.Combine(testsDirectory, "Runtime");
        Directory.CreateDirectory(Path.Combine(FullPath, "Samples"));
        
        //var defaultSamples = new Samples();
        //var defaultDocumentation = new Documentation();
        
        var fileCreator = new FileCreator<Package>(defaultPackage);
        var readmeCreator = new FileCreator<Readme>(defaultReadme);
        var changeLogCreator = new FileCreator<ChangeLog>(defaultChangeLog);
        var licenseCreator = new FileCreator<License>(defaultLicense);
        var thirdPartyNoticesCreator = new FileCreator<ThirdPartyNotices>(defaultThirdPartyNotices);
        
        var editorAssemblyCreator = new FileCreator<AssemblyDefinition>(editorAssembly);
        var runtimeAssemblyCreator = new FileCreator<AssemblyDefinition>(runtimeAssembly);
        var testsAssemblyCreator = new FileCreator<AssemblyDefinition>(testsAssembly);
        var testsEditorAssemblyCreator = new FileCreator<AssemblyDefinition>(testsEditorAssembly);
        
        var documentationCreator = new FileCreator<Documentation>(new Documentation());
        
        fileCreator.CreateFile(FullPath);
        readmeCreator.CreateFile(FullPath);
        changeLogCreator.CreateFile(FullPath);
        licenseCreator.CreateFile(FullPath);
        thirdPartyNoticesCreator.CreateFile(FullPath);
        
        editorAssemblyCreator.CreateFile(editorDirectory);
        runtimeAssemblyCreator.CreateFile(runtimeDirectory);
        testsAssemblyCreator.CreateFile(testsRuntimeDirectory);
        testsEditorAssemblyCreator.CreateFile(testsEditorDirectory);
        documentationCreator.CreateFile(FullPath);
    }

    public void DeleteCurrentPackage()
    {
        Directory.Delete(FullPath, true);
    }
}
