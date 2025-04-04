﻿using System.Collections.Generic;
using GamersGrotto.Plugin_Creator.Editor.IFiles.Extras;
using Newtonsoft.Json;

namespace GamersGrotto.Plugin_Creator.Editor.IFiles {
    public class ThirdPartyNotices : IFile {
        [JsonIgnore] public string FileName { get; set; }
        public string HeaderText { get; set; }

        public List<ThirdParty> ThirdParties { get; set; }

        public ThirdPartyNotices(ThirdParty thirdParty) {
            FileName = "Third_Party_Notices.md";
            HeaderText =
                "This package contains third-party software components governed by the license(s) indicated below:";
            ThirdParties = new List<ThirdParty> { thirdParty };
        }

        public ThirdPartyNotices(List<ThirdParty> thirdParties) {
            FileName = "Third_Party_Notices.md";
            HeaderText =
                "This package contains third-party software components governed by the license(s) indicated below:";
            ThirdParties = thirdParties;
        }

        public string GenerateFileContent() {
            var content = HeaderText + "\n\n";
            foreach (var thirdParty in ThirdParties) {
                content += thirdParty.ToString();
            }

            return content;
        }
    }
}