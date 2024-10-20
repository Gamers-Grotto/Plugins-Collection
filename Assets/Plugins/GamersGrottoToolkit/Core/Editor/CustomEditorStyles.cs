#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace GamersGrotto.Custom_Editor_Styles.Editor {
    public static class CustomEditorStyles {
        public static GUIStyle LargeButton;
        public static GUIStyle MediumButton;
        public static GUIStyle BasicButton;

        public static GUIStyle HeaderLabel;
        public static GUIStyle SubHeaderLabel;

        public static GUIStyle Foldout;

        private static Texture2D _logo;
        public static Texture2D Logo
        {
            get
            {
                if (_logo == null)
                    _logo = Resources.Load<Texture2D>("GGLogo");
                    
                return _logo;
            }
        }

        static CustomEditorStyles() {
            #region Button Styles
            
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
}

#endif