using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace GamersGrotto.Runtime.Modules.UISystem
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class Page : MonoBehaviour
    {
        [Header("UI Settings")]
        [SerializeField] private UIDocument document;
        [SerializeField] private StyleSheet styleSheet;
    
        public T Create<T>(params string[] classes) where T : VisualElement, new()
        {
            var element = new T();
        
            foreach (var className in classes)
                element.AddToClassList(className);

            return element;
        }
    
        protected abstract IEnumerator DrawUI(VisualElement root);
    
        protected IEnumerator Repaint()
        {
            if(!document)
                yield break; 
                
            yield return null; //have had issues in the past with UI toolkit if you dont wait a frame
        
            var root = document.rootVisualElement;
            root.Clear();
        
            if(styleSheet)
                root.styleSheets.Add(styleSheet);

            yield return DrawUI(root);
        }

        #region Monobehaviour

        protected void Awake() => document = GetComponent<UIDocument>();

        protected virtual void Start() => StartCoroutine(Repaint());

        //This is here so that the UI will be drawn in editor too 
        private void OnValidate()  
        {
            if(Application.isPlaying)
                return;
            
            if(isActiveAndEnabled)
                StartCoroutine(Repaint());
        }
        #endregion
    }
}