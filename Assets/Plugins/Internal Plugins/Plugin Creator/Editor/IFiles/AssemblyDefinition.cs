using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditorInternal;

namespace GamersGrotto.Plugin_Creator.Editor.IFiles {
    public class AssemblyDefinition : IFile {
        [JsonIgnore] public string FileName { get; set; }
        public string name { get; set; }
        public string[] references { get; set; }
        public string[] includePlatforms { get; set; }
        public string rootNamespace { get; set; }

        public AssemblyDefinition(string companyName, string packageName, bool testAssembly, bool editorAssembly,
            List<AssemblyDefinitionAsset> references) {
            
            var path = "";

            path = $"{companyName}.{packageName}".ToLower();
            path = path.Replace("-", "_").Replace(" ", "_");
         
            var pathEnding = ".asmdef";

            if (editorAssembly) {
                path += ".Editor";
                includePlatforms = new string[] { "Editor" };
            }


            if (testAssembly) {
                path += ".Tests";
            }

            if (!testAssembly && !editorAssembly) {
                path += ".Runtime";
            }

            FileName = $"{path}{pathEnding}".ToLower();
            name = path.ToLower();
            rootNamespace = path.ToLower();
            ;
            this.references = references.ConvertAll(x => x.name).ToArray();
        }

        public string GenerateFileContent() {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}