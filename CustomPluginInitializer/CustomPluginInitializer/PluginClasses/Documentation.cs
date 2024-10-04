using System.Text.Json.Serialization;
using CustomPluginInitializer.Interfaces;

namespace CustomPluginInitializer;

public class Documentation : IFile
{
    [JsonIgnore]public string FileName { get; set; }

    public Documentation()
    {
        FileName = "Documentation.md";
    }
    public string GenerateFileContent()
    {
        return "# This is a documentation file";
    }
}