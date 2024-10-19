using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace GamersGrotto.Ui_System
{
    [RequireComponent(typeof(UIDocument))]
    public abstract class Page : MonoBehaviour
    {
        [Header("UI Settings")]
        [SerializeField] private UIDocument document;
        [SerializeField] private StyleSheet styleSheet;

        private WaitForEndOfFrame waitForEndOfFrame;

        public bool IsOpen => isActiveAndEnabled;
        
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
            var button = Create<Button>("button", "bordered");
            button.text = text;

            if (onClick != null)
                button.clicked += onClick;
            
            return button;
        }

        protected Label CreateLabel(string text, params string[] classes)
        {
            var label = Create<Label>(classes);
            label.AddToClassList("default-text");
            label.text = text;
            return label;
        }

        protected Slider CreateSlider(float minValue, float maxValue, string label = null, params string[] classes)
        {
            var slider = Create<Slider>(classes);
            slider.AddToClassList("labeled-slider-container");
            if (label != null) slider.label = label;
            slider.lowValue = minValue;
            slider.highValue = maxValue;
            return slider;
        }

        protected Box CreateBox(params string[] classes)
        {
            var box = Create<Box>(classes);
            box.AddToClassList("bordered");
            box.AddToClassList("box");
            return box;
        }

        protected abstract IEnumerator DrawUI(VisualElement root);
    
        protected IEnumerator Repaint()
        {
            if(!document)
                yield break; 
                
            yield return waitForEndOfFrame;
        
            var root = document.rootVisualElement;
            root.Clear();
        
            if(styleSheet)
                root.styleSheets.Add(styleSheet);

            yield return DrawUI(root);
        }

        #region Monobehaviour

        protected virtual void Awake()
        {
            waitForEndOfFrame = new WaitForEndOfFrame();
            document = GetComponent<UIDocument>();
        }

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