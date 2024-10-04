using System.Text.Json.Serialization;
using CustomPluginInitializer.Interfaces;

namespace CustomPluginInitializer;

internal class Readme : IFile
{
    [JsonIgnore] public string FileName { get; set; } = "README.md";

    public string GenerateFileContent()
    {
        return "# This is a README file";
    }
}