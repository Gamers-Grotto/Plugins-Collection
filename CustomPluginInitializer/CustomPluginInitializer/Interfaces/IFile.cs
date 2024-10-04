using System.Text.Json.Serialization;

namespace CustomPluginInitializer.Interfaces;

public interface IFile
{
    [JsonIgnore] public string FileName { get; set; } // Put Attribute on every instance.
    public string GenerateFileContent();
}