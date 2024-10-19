using Newtonsoft.Json;

namespace GamersGrotto.Plugin_Creator.Editor.IFiles {
    public class Documentation : IFile {
        [JsonIgnore] public string FileName { get; set; } = "Documentation.md";

        public string GenerateFileContent() {
            return "# This is a Documentation file";
        }
    }
}