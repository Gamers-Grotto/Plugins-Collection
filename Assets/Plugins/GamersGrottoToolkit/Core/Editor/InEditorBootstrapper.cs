﻿using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamersGrotto.Core.Editor
{
    public static class InEditorBootstrapper
    {
        // Rename this to match the name of the scene you would like to have loaded before your current scene
        private const string coreSceneName = "Core";
        
        private const string BootstrapperMenuItem = "GamersGrotto/Editor Bootstrapper/Load Core Scene on EnterPlayMode";
        private const string LoggingMenuItem = "GamersGrotto/Editor Bootstrapper/Logging...";
        private const string BootstrappleEnabledPrefKey = "BootstrapperToggle";
        private const string LoggingEnabledPrefKey = "EnableLogging";
        
        private static readonly string TAG = "[Bootstrapper]".Colorize("orange");
        
#if UNITY_EDITOR
        public static bool StartedFromNonCoreScene = false;
        
        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            if(EditorPrefs.GetBool(LoggingEnabledPrefKey, false) && EditorPrefs.GetBool(BootstrappleEnabledPrefKey, false))
                Debug.Log($"{TAG} Init");
            
            EditorApplication.playModeStateChanged -= EditorApplicationOnplayModeStateChanged;
            EditorApplication.playModeStateChanged += EditorApplicationOnplayModeStateChanged;
        }

        [MenuItem(LoggingMenuItem)]
        public static void EnableLogging()
        {
            var isToggled = EditorPrefs.GetBool(LoggingEnabledPrefKey, false);
            isToggled = !isToggled;
            EditorPrefs.SetBool(LoggingEnabledPrefKey, isToggled);
        }
        
        [MenuItem(LoggingMenuItem, true)]
        private static bool LoggingItemValidate()
        {
            var isToggled = EditorPrefs.GetBool(LoggingEnabledPrefKey, false);
            var bootstrapperIsEnabled = EditorPrefs.GetBool(BootstrappleEnabledPrefKey, false);
            Menu.SetChecked(LoggingMenuItem, isToggled);
            return bootstrapperIsEnabled;
        }
        
        [MenuItem(BootstrapperMenuItem)]
        public static void MenuItem()
        {
            var isToggled = EditorPrefs.GetBool(BootstrappleEnabledPrefKey, false);
            isToggled = !isToggled;
            EditorPrefs.SetBool(BootstrappleEnabledPrefKey, isToggled);
            
            if(!EditorPrefs.GetBool(LoggingEnabledPrefKey))
                return;
                
            var enabledText = isToggled 
                ? "Enabled".Colorize("green") 
                : "Disabled".Colorize("red");
            
            Debug.Log($"{TAG} Loading Scene({coreSceneName}) when entering Play Mode : {enabledText}");
        }
        
        [MenuItem(BootstrapperMenuItem, true)]
        private static bool ToggleItemValidate()
        {
            var isToggled = EditorPrefs.GetBool(BootstrappleEnabledPrefKey, false);
            Menu.SetChecked(BootstrapperMenuItem, isToggled);
            return true;
        }
        
        private static void EditorApplicationOnplayModeStateChanged(PlayModeStateChange playModeState)
        {
            var isEnabled = EditorPrefs.GetBool(BootstrappleEnabledPrefKey, false);
            
            if(!isEnabled)
                return;
            
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
                    
                    if(!EditorPrefs.GetBool(BootstrappleEnabledPrefKey, false))
                        return;
                    
                    if (!SceneManager.GetSceneByName(coreSceneName).isLoaded)
                    {
                        StartedFromNonCoreScene = true;
                        
                        if(loggingEnabled)
                            Debug.Log($"{TAG} Core Scene ({coreSceneName}) not loaded, Loading {coreSceneName} Scene");

                        if(IsInBuildList(coreSceneName))
                            SceneManager.LoadScene(coreSceneName, LoadSceneMode.Additive);
                        else Debug.LogWarning($"{TAG} Core Scene ({coreSceneName}) not in build list");
                    }
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    StartedFromNonCoreScene = false;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playModeState), playModeState, null);
            }
        }

        private static bool IsInBuildList(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                if (sceneName == SceneUtility.GetScenePathByBuildIndex(i))
                    return true;
            }

            return false;
        }
#endif
    }
}