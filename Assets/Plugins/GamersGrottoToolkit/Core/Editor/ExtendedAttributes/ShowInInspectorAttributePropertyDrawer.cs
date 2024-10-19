using UnityEditor;
using UnityEngine;
using GamersGrotto.ExtendedAttributes;

namespace GamersGrotto.Editor.ExtendedAttributes
{
    [CustomPropertyDrawer(typeof(ShowInInspectorAttribute))]
    public class ShowInInspectorAttributePropertyDrawer : PropertyDrawer
    {
        private UnityEditor.Editor editor;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label, true);

            if (property.objectReferenceValue == null)
                return;

            if (property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, GUIContent.none))
            {
                EditorGUI.indentLevel++;
                EditorGUILayout.BeginVertical(GUI.skin.box);
                
                if (!editor)
                    UnityEditor.Editor.CreateCachedEditor(property.objectReferenceValue, null, ref editor);

                editor.OnInspectorGUI();

                EditorGUI.indentLevel--;
                EditorGUILayout.EndVertical();
            }
        }
    }
}