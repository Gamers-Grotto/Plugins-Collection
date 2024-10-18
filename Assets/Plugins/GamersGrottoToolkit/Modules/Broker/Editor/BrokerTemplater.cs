#if UNITY_EDITOR
using UnityEditor;


public class BrokerTemplater : BaseTemplater {
    private const string TEMPLATE_PATHS = "Assets/Plugins/GamersGrottoToolkit/Modules/Broker/Editor/Templates/";
    private const string ASSET_PATH_SUFFIX = "Broker/";

    [MenuItem(ASSET_PATH + ASSET_PATH_SUFFIX + "Empty Message", priority = 30)]
    public static void CreateEmptyMessageMenuItem() {
        var filename = "EmptyMessageTemplate.txt";
        CreateScriptAssetFromTemplateFile(TEMPLATE_PATHS, filename);
    }

    [MenuItem(ASSET_PATH + ASSET_PATH_SUFFIX + "Message", priority = 30)]
    public static void CreateMessageMenuItem() {
        var filename = "MessageTemplate.txt";
        CreateScriptAssetFromTemplateFile(TEMPLATE_PATHS, filename);
    }
}
#endif