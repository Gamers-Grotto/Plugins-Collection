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

            var singleLineHeight = EditorGUIUtility.singleLineHeight;

            var foldoutPosition = new Rect(position.x, position.y, position.width, singleLineHeight);

            if (property.isExpanded = EditorGUI.Foldout(foldoutPosition, property.isExpanded, GUIContent.none))
            {
                EditorGUI.indentLevel++;

                if (editor == null)
                    UnityEditor.Editor.CreateCachedEditor(property.objectReferenceValue, null, ref editor);

                editor.OnInspectorGUI();

                EditorGUI.indentLevel--;
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var height = EditorGUI.GetPropertyHeight(property, label, true);

            if (property.isExpanded && property.objectReferenceValue != null)
                height += EditorGUIUtility.singleLineHeight;

            return height;
        }
    }
}