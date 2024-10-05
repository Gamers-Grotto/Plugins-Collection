#if UNITY_EDITOR
using System.IO;
using UnityEditor;

public static class BaseTemplateCreator
{
    static string TEMPLATE_BASE_PATH =  @"Assets/Plugins/GamersGrotto/Editor/Core/Templates";
    const string ASSET_PATH = "Assets/Create/GamersGrotto/Templates/Base/";

    [MenuItem(ASSET_PATH + "Scriptable Object", priority = 30)]
    public static void CreateMessageMenuItem()
    {
        var filename = "ScriptableObjectTemplate.txt";
        CreateScriptAssetFromTemplateFile(filename);
    }
    
    [MenuItem(ASSET_PATH + "MonoBehaviour", priority = 30)]
    
    
    public static void CreateMonoBehaviourMenuItem()
    {
        var filename = "MonoBehaviourTemplate.txt";
        CreateScriptAssetFromTemplateFile(filename);
    }
    
    [MenuItem(ASSET_PATH + "Editor", priority = 30)]
    public static void CreateEditorScriptMenuItem()
    {
        var filename = "EditorScriptTemplate.txt";
        CreateScriptAssetFromTemplateFile(filename);
    }
    
    [MenuItem(ASSET_PATH + "Interface", priority = 30)]
    public static void CreateInterfaceMenuItem()
    {
        var filename = "InterfaceTemplate.txt";
        CreateScriptAssetFromTemplateFile(filename);
    }
    
    [MenuItem(ASSET_PATH + "Enum", priority = 30)]
    public static void CreateEnumMenuItem()
    {
        var filename = "EnumTemplate.txt";
        CreateScriptAssetFromTemplateFile(filename);
    }
    
    [MenuItem(ASSET_PATH + "Static Class", priority = 30)]
    public static void CreateStaticClassMenuItem()
    {
        var filename = "StaticClassTemplate.txt";
        CreateScriptAssetFromTemplateFile(filename);
    }
    
    
    
    [MenuItem(ASSET_PATH + "Singleton", priority = 30)]
    public static void CreateSingletonMenuItem()
    {
        var filename = "SingletonTemplate.txt";
        CreateScriptAssetFromTemplateFile(filename);
    }
    public static void CreateScriptAssetFromTemplateFile(string templateName)
    {
        var createdFileName = templateName.Insert(0, "New")
            .Replace("Template.txt", ".cs");
        var fullTemplateFilePath = Path.Join (TEMPLATE_BASE_PATH, templateName);
        ProjectWindowUtil.CreateScriptAssetFromTemplateFile(fullTemplateFilePath, createdFileName);
    }
}
#endif