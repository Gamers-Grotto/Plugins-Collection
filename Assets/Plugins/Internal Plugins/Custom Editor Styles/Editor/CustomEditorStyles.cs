#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

public static class CustomEditorStyles {
    public static GUIStyle LargeButton;
    public static GUIStyle MediumButton;
    public static GUIStyle BasicButton;

    public static GUIStyle HeaderLabel;
    public static GUIStyle SubHeaderLabel;

    public static GUIStyle Foldout;

    #region Button Styles

    static CustomEditorStyles() {
        LargeButton = new GUIStyle(GUI.skin.button) {
            fontSize = 16,
            fontStyle = FontStyle.Bold,
            fixedHeight = 50
        };

        MediumButton = new GUIStyle(GUI.skin.button) {
            fontSize = 14,
            fontStyle = FontStyle.Bold,
            fixedHeight = 30
        };

        BasicButton = new GUIStyle(GUI.skin.button) {
            fontSize = 12,
            fontStyle = FontStyle.Normal,
        };

        #endregion

        HeaderLabel = new GUIStyle(GUI.skin.label) {
            fontSize = 18,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleCenter
        };

        SubHeaderLabel = new GUIStyle(GUI.skin.label) {
            fontSize = 14,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleLeft
        };

        Foldout = new GUIStyle(EditorStyles.foldout) {
            fontSize = 14,
            fontStyle = FontStyle.Bold,
            alignment = TextAnchor.MiddleLeft
        };
    }
}

#endif