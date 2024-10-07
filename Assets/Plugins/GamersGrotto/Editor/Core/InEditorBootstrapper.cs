using System;
using GamersGrotto.Runtime.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Plugins.GamersGrotto.Editor
{
    public static class InEditorBootstrapper
    {
        private static readonly string TAG = "[Bootstrapper]".Colorize("orange");
        private const string MenuItemName = "GamersGrotto/Editor Bootstrapper/Load Core Scene on EnterPlayMode";
        private const string LoggingMenuItemName = "GamersGrotto/Editor Bootstrapper/Logging...";
        private const string TogglePrefKey = "BootstrapperToggle";
        private const string LoggingEnabledPrefKey = "EnableLogging";
        private const string coreSceneName = "Core";
        
#if UNITY_EDITOR
        public static bool StartedFromNonCoreScene = false;
        
        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            if(EditorPrefs.GetBool(LoggingEnabledPrefKey, false))
                Debug.Log($"{TAG} Init");
            
            EditorApplication.playModeStateChanged -= EditorApplicationOnplayModeStateChanged;
            EditorApplication.playModeStateChanged += EditorApplicationOnplayModeStateChanged;
        }

        [MenuItem(LoggingMenuItemName)]
        public static void EnableLogging()
        {
            var isToggled = EditorPrefs.GetBool(LoggingEnabledPrefKey, false);
            isToggled = !isToggled;
            EditorPrefs.SetBool(LoggingEnabledPrefKey, isToggled);
        }
        
        [MenuItem(LoggingMenuItemName, true)]
        private static bool LoggingItemValidate()
        {
            var isToggled = EditorPrefs.GetBool(LoggingEnabledPrefKey, false);
            Menu.SetChecked(LoggingMenuItemName, isToggled);
            return true;
        }
        
        [MenuItem(MenuItemName)]
        public static void MenuItem()
        {
            var isToggled = EditorPrefs.GetBool(TogglePrefKey, false);
            isToggled = !isToggled;
            EditorPrefs.SetBool(TogglePrefKey, isToggled);
            
            if(!EditorPrefs.GetBool(LoggingEnabledPrefKey))
                return;
                
            var enabledText = isToggled 
                ? "Enabled".Colorize("green") 
                : "Disabled".Colorize("red");
            
            Debug.Log($"{TAG} Loading Game Scene when entering Play Mode : {enabledText}");
        }
        
        [MenuItem(MenuItemName, true)]
        private static bool ToggleItemValidate()
        {
            var isToggled = EditorPrefs.GetBool(TogglePrefKey, false);
            Menu.SetChecked(MenuItemName, isToggled);
            return true;
        }
        
        private static void EditorApplicationOnplayModeStateChanged(PlayModeStateChange playModeState)
        {
            var loggingEnabled = EditorPrefs.GetBool(LoggingEnabledPrefKey, true);
            
            if(loggingEnabled)
                Debug.Log($"{TAG} Editor Application State Changed : {playModeState}");
        
            switch (playModeState)
            {
                case PlayModeStateChange.EnteredEditMode:
                    break;
                case PlayModeStateChange.ExitingEditMode:
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    
                    if(!EditorPrefs.GetBool(TogglePrefKey, false))
                        return;
                    
                    if (!SceneManager.GetSceneByName(coreSceneName).isLoaded)
                    {
                        StartedFromNonCoreScene = true;
                        
                        if(loggingEnabled)
                            Debug.Log($"{TAG} Core Scene ({coreSceneName}) not loaded, Loading {coreSceneName} Scene");

                        SceneManager.LoadScene(coreSceneName, LoadSceneMode.Additive);

                    }
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    StartedFromNonCoreScene = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playModeState), playModeState, null);
            }
        }
#endif
    }
}