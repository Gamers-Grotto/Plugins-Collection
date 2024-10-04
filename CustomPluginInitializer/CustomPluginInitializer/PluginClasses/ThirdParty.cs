namespace CustomPluginInitializer;

public class ThirdParty
{
    public string Name { get; set; }
    public string LicenseType { get; set; }
    public string Url { get; set; }
    
    public ThirdParty(string name, string licenseType, string url)
    {
        Name = name;
        LicenseType = licenseType;
        Url = url;
    }

    public ThirdParty()
    {
        Name = "MIT License";
        LicenseType = "MIT";
        Url = "https://opensource.org/licenses/MIT";
        
    }


    public override string ToString()
    {
        return $"# {Name}\n\nLicense Type: {LicenseType}\n\n[More Info]({Url})\n\n---\n";
    }
}