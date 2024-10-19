using System;
using Newtonsoft.Json;

namespace GamersGrotto.Plugin_Creator.Editor.IFiles {
    public class ChangeLog : IFile {
        [JsonIgnore] public string FileName { get; set; }
        public string Version { get; set; }
        public string Date { get; set; }
        public string Changes { get; set; }

        public ChangeLog(string version, string changes) {
            FileName = "CHANGELOG.md";
            Version = version;
            Date = DateTime.Now.ToString("yyyy-MM-dd");
            Changes = changes;
        }

        public string GenerateFileContent() {
            return $"# Version {Version} ({Date})\n\n{Changes}";
        }
    }
}