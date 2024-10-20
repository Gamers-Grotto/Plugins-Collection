using System;
using System.Reflection;
using GamersGrotto.Core.Extended_Attributes;
using GamersGrotto.Custom_Editor_Styles.Editor;
using UnityEditor;
using UnityEngine;

namespace GamersGrotto.Core.Editor
{
    [CustomEditor(typeof(MonoBehaviour), editorForChildClasses: true)]
    public class GGEditor : UnityEditor.Editor
    {
        private bool isInGamersGrottoNamespace;
        
        private void OnEnable()
        {
            var targetNamespace = target.GetType().Namespace;
            isInGamersGrottoNamespace = targetNamespace?.StartsWith("GamersGrotto") ?? false;
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var mono = target as MonoBehaviour;
            var type = target.GetType();
            
            var methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods)
            {
                var buttonAttribute = (ButtonAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));
                
                if (buttonAttribute == null) 
                    continue;

                var buttonText = buttonAttribute.ButtonText ?? method.Name;

                if (GUILayout.Button(buttonText))
                    method.Invoke(mono, null);
            }
            
            if (!isInGamersGrottoNamespace) 
                return;
            
            using (new GUILayout.HorizontalScope())
            {
                if (CustomEditorStyles.Logo != null)
                    GUILayout.Label(CustomEditorStyles.Logo, GUILayout.Width(64), GUILayout.Height(64));
                    
                using (new GUILayout.VerticalScope())
                {
                    GUILayout.FlexibleSpace();
                    GUILayout.Label("Gamers Grotto",CustomEditorStyles.SubHeaderLabel);
                    GUILayout.FlexibleSpace();
                }
            }
        }
    }
}