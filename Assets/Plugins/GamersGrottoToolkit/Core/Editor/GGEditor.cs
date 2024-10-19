using GamersGrotto.Custom_Editor_Styles.Editor;
using UnityEditor;
using UnityEngine;

namespace GamersGrotto.Core.Editor
{
    [CustomEditor(typeof(MonoBehaviour), editorForChildClasses: true)]
    public class GGEditor : UnityEditor.Editor
    {
        private static Texture2D logo;

        private bool isInGamersGrottoNamespace;
        
        private void OnEnable()
        {
            var targetNamespace = target.GetType().Namespace;
            isInGamersGrottoNamespace = targetNamespace?.StartsWith("GamersGrotto") ?? false;
            
            if(logo == null)
                logo = Resources.Load<Texture2D>("GGLogo");
        }
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!isInGamersGrottoNamespace) 
                return;
            
            using (new GUILayout.HorizontalScope())
            {
                if (logo != null)
                    GUILayout.Label(logo, GUILayout.Width(64), GUILayout.Height(64));
                    
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