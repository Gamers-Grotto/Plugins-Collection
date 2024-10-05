using System;
using GamersGrotto.Runtime.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Plugins.GamersGrotto.Editor
{
    public static class InEditorBootstrapper
    {
        private static readonly string TAG = "Bootstrapper".Colorize("orange");
        private const string MenuItemName = "GamersGrotto/Editor Bootstrapper/Load Game Scene on EnterPlayMode";
        private const string TogglePrefKey = "BootstrapperToggle";

        private const string coreSceneName = "Core";
        
#if UNITY_EDITOR
        public static bool StartedFromNonGameScene = false;
        
        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            Debug.Log($"{TAG} Init");
            EditorApplication.playModeStateChanged -= EditorApplicationOnplayModeStateChanged;
            EditorApplication.playModeStateChanged += EditorApplicationOnplayModeStateChanged;
        }

        [MenuItem(MenuItemName)]
        public static void MenuItem()
        {
            var isToggled = EditorPrefs.GetBool(TogglePrefKey, false);;
            isToggled = !isToggled;
            EditorPrefs.SetBool(TogglePrefKey, isToggled);
            
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
                        StartedFromNonGameScene = true;
                        Debug.Log($"{TAG} Game Scene not loaded, Loading Game Scene");
                        SceneManager.LoadScene("Game", LoadSceneMode.Additive);
                    }
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    StartedFromNonGameScene = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playModeState), playModeState, null);
            }
        }
#endif
    }
}