using System;
using System.Linq;
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
            
            DrawButtons();

            if (isInGamersGrottoNamespace)
            {
                DrawLogo();
            }
        }

        private void DrawButtons()
        {
            var mono = target as MonoBehaviour;
            var methods = target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var method in methods.Where(m => Attribute.IsDefined(m, typeof(ButtonAttribute))))
            {
                var buttonAttribute = (ButtonAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));
                var buttonText = buttonAttribute?.ButtonText ?? method.Name;

                if (GUILayout.Button(buttonText))
                {
                    method.Invoke(mono, null);
                }
            }
        }

        private void DrawLogo()
        {
            using (new GUILayout.HorizontalScope())
            {
                if (CustomEditorStyles.Logo != null)
                {
                    GUILayout.Label(CustomEditorStyles.Logo, GUILayout.Width(64), GUILayout.Height(64));
                }

                using (new GUILayout.VerticalScope())
                {
                    GUILayout.FlexibleSpace();
                    GUILayout.Label("Gamers Grotto", CustomEditorStyles.SubHeaderLabel);
                    GUILayout.FlexibleSpace();
                }
            }
        }
    }
}