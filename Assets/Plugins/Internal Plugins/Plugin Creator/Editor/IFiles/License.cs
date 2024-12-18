﻿using GamersGrotto.Plugin_Creator.Editor.IFiles.Extras;
using Newtonsoft.Json;

namespace GamersGrotto.Plugin_Creator.Editor.IFiles {
    public class License : IFile {
        [JsonIgnore] public string FileName { get; set; }
        public string HeaderText { get; set; }
        public ThirdParty ThirdParty { get; set; }

        public License( ThirdParty thirdParty ) {
            FileName = "LICENSE.md";
            HeaderText = "This package is licensed under the following license:";
            ThirdParty = thirdParty;
        }
        //new ThirdParty {
        //    Name = "MIT License",
        //    LicenseType = "MIT",
        //    Url = "https://opensource.org/licenses/MIT"
        //};
        public string GenerateFileContent() {
            return $"# {HeaderText}\n\n" + ThirdParty.ToString();
        }
    }
}