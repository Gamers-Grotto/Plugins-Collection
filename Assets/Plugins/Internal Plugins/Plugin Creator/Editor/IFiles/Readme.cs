using Newtonsoft.Json;

namespace GamersGrotto.Plugin_Creator.Editor.IFiles {
    internal class Readme : IFile {
        [JsonIgnore] public string FileName { get; set; } = "README.md";

        public string GenerateFileContent() {
            return "# This is a README file";
        }
    }
}