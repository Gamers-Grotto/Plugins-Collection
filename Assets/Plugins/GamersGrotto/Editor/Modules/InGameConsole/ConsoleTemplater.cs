#if UNITY_EDITOR
using UnityEditor;

public class ConsoleTemplater : BaseTemplater {
    const string TEMPLATE_PATHS = "Assets/Plugins/GamersGrotto/Editor/Modules/InGameConsole/Templates";
    private const string ASSET_PATH_SUFFIX = "Console/";

    [MenuItem(ASSET_PATH + ASSET_PATH_SUFFIX + "Console Command", priority = 30)]
    public static void CreateTEMPLATENAMEMenuItem() {
        var filename = "ConsoleCommandTemplate.txt";
        CreateScriptAssetFromTemplateFile(TEMPLATE_PATHS, filename);
    }
}
#endif