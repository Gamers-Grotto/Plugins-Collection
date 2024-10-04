using System.Text.Json;
using System.Text.Json.Serialization;
using CustomPluginInitializer.Interfaces;

namespace CustomPluginInitializer;

public class AssemblyDefinition : IFile
{
    [JsonIgnore] public string FileName { get; set; }
    public string name { get; set; }
    public string[] includePlatforms { get; set; }
    public string rootNamespace { get; set; }
    [JsonIgnore] public bool TestAssembly { get; set; }
    [JsonIgnore] public bool EditorAssembly { get; set; }
    public AssemblyDefinition(string companyName, string packageName, bool testAssembly, bool editorAssembly)
    {

        var path = "";
        
        path = $"{companyName}.{packageName}".ToLower();
        path = path.Replace("-", "_");
        var pathEnding = ".asmdef";
        
        if(editorAssembly)
        {
            path += ".Editor";
            includePlatforms = new string[] { "Editor" };
            
        }
        
        if (testAssembly)
        {
            path += ".Tests";
        }
        
        FileName =$"{path}{pathEnding}".ToLower();
        name = path.ToLower();
        rootNamespace = path.ToLower();;
    }
    public string GenerateFileContent()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        return JsonSerializer.Serialize(this, options);
    }
}