﻿using UnityEditor;
using UnityEngine;

namespace GamersGrotto.Editor
{
    public static class EditorExtensions
    {
        public static float GetInspectorHeight(this UnityEditor.Editor editor)
        {
            var totalHeight = EditorGUIUtility.standardVerticalSpacing;

            var property = editor.serializedObject.GetIterator();
            property.NextVisible(true); // Skip the first item.

            while (property.NextVisible(false))
            {
                totalHeight += EditorGUI.GetPropertyHeight(property, true) + EditorGUIUtility.standardVerticalSpacing;
            }

            return totalHeight;
        }

        public static void OnInspectorGUI(this UnityEditor.Editor editor, Rect position)
        {
            position.y += EditorGUIUtility.standardVerticalSpacing;
            
            var property = editor.serializedObject.GetIterator();
            property.NextVisible(true);

            while (property.NextVisible(false))
            {
                position.height = EditorGUI.GetPropertyHeight(property, true);
                EditorGUI.PropertyField(position, property, true);
                position.y += position.height + EditorGUIUtility.standardVerticalSpacing;
            }
            
            position.y += EditorGUIUtility.standardVerticalSpacing;
        }
    }
}