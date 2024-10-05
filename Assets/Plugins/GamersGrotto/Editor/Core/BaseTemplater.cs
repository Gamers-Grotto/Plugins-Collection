#if UNITY_EDITOR
using System.IO;
using UnityEditor;

public class BaseTemplater {
    static string TEMPLATE_BASE_PATH = @"Assets/Plugins/GamersGrotto/Editor/Core/Templates";
    protected const string ASSET_PATH = "Assets/Create/GamersGrotto/Templates/";
    private const string ASSET_PATH_SUFFIX = "Base/";

    [MenuItem(ASSET_PATH + ASSET_PATH_SUFFIX+ "Scriptable Object", priority = 30)]
    public static void CreateScriptableObjectMenuItem() {
        var filename = "ScriptableObjectTemplate.txt";
        CreateScriptAssetFromTemplateFile(TEMPLATE_BASE_PATH, filename);
    }

    [MenuItem(ASSET_PATH + ASSET_PATH_SUFFIX + "MonoBehaviour", priority = 30)]
    public static void CreateMonoBehaviourMenuItem() {
        var filename = "MonoBehaviourTemplate.txt";
        CreateScriptAssetFromTemplateFile(TEMPLATE_BASE_PATH, filename);
    }

    [MenuItem(ASSET_PATH + ASSET_PATH_SUFFIX + "Editor", priority = 30)]
    public static void CreateEditorScriptMenuItem() {
        var filename = "EditorScriptTemplate.txt";
        CreateScriptAssetFromTemplateFile(TEMPLATE_BASE_PATH, filename);
    }

    [MenuItem(ASSET_PATH + ASSET_PATH_SUFFIX + "Interface", priority = 30)]
    public static void CreateInterfaceMenuItem() {
        var filename = "InterfaceTemplate.txt";
        CreateScriptAssetFromTemplateFile(TEMPLATE_BASE_PATH, filename);
    }

    [MenuItem(ASSET_PATH + ASSET_PATH_SUFFIX + "Enum", priority = 30)]
    public static void CreateEnumMenuItem() {
        var filename = "EnumTemplate.txt";
        CreateScriptAssetFromTemplateFile(TEMPLATE_BASE_PATH, filename);
    }

    [MenuItem(ASSET_PATH + ASSET_PATH_SUFFIX + "Static Class", priority = 30)]
    public static void CreateStaticClassMenuItem() {
        var filename = "StaticClassTemplate.txt";
        CreateScriptAssetFromTemplateFile(TEMPLATE_BASE_PATH, filename);
    }

    [MenuItem(ASSET_PATH + ASSET_PATH_SUFFIX + "Singleton", priority = 30)]
    public static void CreateSingletonMenuItem() {
        var filename = "SingletonTemplate.txt";
        CreateScriptAssetFromTemplateFile(TEMPLATE_BASE_PATH, filename);
    }
    
    [MenuItem(ASSET_PATH + ASSET_PATH_SUFFIX + "Attribute", priority = 30)]
    public static void CreateAttributeMenuItem()
    {
        var filename = "AttributeTemplate.txt";
        CreateScriptAssetFromTemplateFile(TEMPLATE_BASE_PATH,filename);
    }

    public static void CreateScriptAssetFromTemplateFile(string templatePath, string templateName) {
        var createdFileName = templateName.Insert(0, "New")
            .Replace("Template.txt", ".cs");
        var fullTemplateFilePath = Path.Join(templatePath, templateName);
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(fullTemplateFilePath, createdFileName);
    }
}
#endif