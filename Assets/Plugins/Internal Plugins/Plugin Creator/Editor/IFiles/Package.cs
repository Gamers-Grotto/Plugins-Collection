using System.Collections.Generic;
using GamersGrotto.Plugin_Creator.Editor.IFiles.Extras;
using Newtonsoft.Json;

namespace GamersGrotto.Plugin_Creator.Editor.IFiles {
    public class Package : IFile {
        [JsonIgnore] public string FileName { get; set; } = "package.json";

        public string name { get; set; }
        public string version { get; set; }
        public string displayName { get; set; }
        public string description { get; set; }
        public string unity { get; set; }
        public string unityRelease { get; set; }
        public string documentationUrl { get; set; }
        public string changelogUrl { get; set; }
        public string licensesUrl { get; set; }
        public Dictionary<string, string> dependencies { get; set; }
        public List<string> keywords { get; set; }
        public Author author { get; set; }
        public Repository repository { get; set; }

        public Package(string companyName, string packageName, string version, List<string> keywords = null) {
            this.version = version;
            this.displayName = packageName;
            name = $"{companyName}.{packageName}".ToLower().Replace(" ", "-"); //Important to have the name in lowercase
            description = "Description of the package, what it does and how to use it.";
            unity = "6000.0";
            unityRelease = "20f1";
            //documentationUrl = "https://example.com/";
            //changelogUrl = "https://example.com/changelog.html";
            //licensesUrl = "https://example.com/licensing.html";
            dependencies = new Dictionary<string, string> {};
            this.keywords =keywords;
            author = new Author {
                name = "GamersGrotto",
                email = "gamersgrottodevelopers@gmail.com",
            };
            repository = new Repository {
                type = "git",
                url = "https://github.com/Gamers-Grotto/GamersGrottoToolkit.git"
            };
        }

        public Package(string name, string version, string displayName, string description, string unity,
            string unityRelease, string documentationUrl, string changelogUrl, string licensesUrl,
            Dictionary<string, string> dependencies, List<string> keywords, Author author) {
            this.name = name;
            this.version = version;
            this.displayName = displayName;
            this.description = description;
            this.unity = unity;
            this.unityRelease = unityRelease;
            this.documentationUrl = documentationUrl;
            this.changelogUrl = changelogUrl;
            this.licensesUrl = licensesUrl;
            this.dependencies = dependencies;
            this.keywords = keywords;
            this.author = author;
        }


        public string GenerateFileContent() {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}