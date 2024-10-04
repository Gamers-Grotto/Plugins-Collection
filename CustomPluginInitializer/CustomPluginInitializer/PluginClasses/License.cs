using System.Text.Json.Serialization;
using CustomPluginInitializer.Interfaces;

namespace CustomPluginInitializer;

public class License : IFile
{
   [JsonIgnore] public string FileName { get; set; }
   public string HeaderText { get; set; }
   public ThirdParty ThirdParty { get; set; }

   public License()
   {
       FileName = "LICENSE.md";
       HeaderText = "This package is licensed under the following license:";
       ThirdParty = new ThirdParty
       {
           Name = "MIT License",
           LicenseType = "MIT",
           Url = "https://opensource.org/licenses/MIT"
       };

   }
   public string GenerateFileContent()
   {
         return $"# {HeaderText}\n\n" + ThirdParty.ToString();
   }
}