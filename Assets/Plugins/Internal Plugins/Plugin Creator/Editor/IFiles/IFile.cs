using Unity.Plastic.Newtonsoft.Json;

namespace GamersGrotto.Plugin_Creator.Editor.IFiles {
    public interface IFile {
        [JsonIgnore] public string FileName { get; set; } // Put Attribute on every instance.
        public string GenerateFileContent();
    }
}