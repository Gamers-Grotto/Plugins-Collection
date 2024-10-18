#if UNITY_EDITOR

using UnityEditor;

public class GameEventTemplater : BaseTemplater {
    private const string TEMPLATE_PATHS = "Assets/Plugins/GamersGrotto/Editor/Core/GameEvents/Templates";
    private const string ASSET_PATH_SUFFIX = "Game Events/";

    [MenuItem(ASSET_PATH + ASSET_PATH_SUFFIX + "Game Event", priority = 30)]
    public static void CreateGameEventMenuItem() {
        var filename = "GameEventTemplate.txt";
        CreateScriptAssetFromTemplateFile(TEMPLATE_PATHS, filename);
    }
}
#endif