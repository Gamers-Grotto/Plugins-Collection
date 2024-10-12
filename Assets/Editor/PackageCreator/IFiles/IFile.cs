using Unity.Plastic.Newtonsoft.Json;

namespace Editor.PackageCreator {
    public interface IFile {
        [JsonIgnore] public string FileName { get; set; } // Put Attribute on every instance.
        public string GenerateFileContent();
    }
}