using System;
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
        
        protected T Create<T>(params string[] classes) where T : VisualElement, new()
        {
            var element = new T();
        
            foreach (var className in classes)
                element.AddToClassList(className);

            return element;
        }
        
        protected VisualElement Create(params string[] classes) => Create<VisualElement>(classes);
        
        protected Button CreateButton(string text, Action onClick = null)
        {
            var button = Create<Button>("button", "button-text");
            button.text = text;

            if (onClick != null)
                button.clicked += onClick;
            
            return button;
        }

        protected Label CreateLabel(string text, params string[] classes)
        {
            var label = Create<Label>(classes);
            label.text = text;
            return label;
        }

        protected Slider CreateSlider(float minValue, float maxValue, params string[] classes)
        {
            var slider = Create<Slider>(classes);
            slider.AddToClassList("slider");
            slider.lowValue = minValue;
            slider.highValue = maxValue;
            return slider;
        }

        protected Box CreateBox(params string[] classes)
        {
            var box = Create<Box>(classes);
            box.AddToClassList("box");
            return box;
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

        protected virtual void OnEnable() => StartCoroutine(Repaint());

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