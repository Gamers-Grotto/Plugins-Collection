using Newtonsoft.Json;

namespace Editor.PluginCreator {
    internal class Readme : IFile {
        [JsonIgnore] public string FileName { get; set; } = "README.md";

        public string GenerateFileContent() {
            return "# This is a README file";
        }
    }
}