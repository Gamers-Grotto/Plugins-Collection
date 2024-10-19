using GamersGrotto.Core.Extended_Attributes;
using UnityEditor;
using UnityEngine;

namespace GamersGrotto.Core.Editor.ExtendedAttributes
{
    [CustomPropertyDrawer(typeof(ShowInInspectorAttribute))]
    public class ShowInInspectorAttributePropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.isExpanded && property.objectReferenceValue != null)
            {
                var editor = UnityEditor.Editor.CreateEditor(property.objectReferenceValue);
                return EditorGUI.GetPropertyHeight(property, label, true) + editor.GetInspectorHeight();
            }

            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = EditorGUI.GetPropertyHeight(property, label, true);
            EditorGUI.PropertyField(position, property, label, true);

            if (property.objectReferenceValue == null)
                return;

            var foldoutRect = new Rect(position.x, position.y + position.height - EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
            property.isExpanded = EditorGUI.Foldout(foldoutRect, property.isExpanded, GUIContent.none);

            if (property.isExpanded)
            {
                EditorGUI.indentLevel++;

                var editor = UnityEditor.Editor.CreateEditor(property.objectReferenceValue);
                if (editor != null)
                {
                    var inspectorRect = new Rect(position.x, foldoutRect.y + EditorGUIUtility.singleLineHeight, position.width, editor.GetInspectorHeight());
                    editor.OnInspectorGUI(inspectorRect);
                }

                EditorGUI.indentLevel--;
            }
        }
    }
}
