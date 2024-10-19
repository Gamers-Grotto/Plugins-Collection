using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace GamersGrotto.Ui_System.Pages
{
    public class LoadingScreen : Page
    {
        [SerializeField] private Sprite background;
        [SerializeField] private Color backgroundColor;
        
        protected override IEnumerator DrawUI(VisualElement root)
        {
            var backgroundImage = Create("full-screen");

            backgroundImage.style.backgroundColor = new StyleColor(backgroundColor);
            
            if(background)
                backgroundImage.style.backgroundImage = new StyleBackground(background);
            
            root.Add(backgroundImage);

            var loadingText = CreateLabel("Loading...", "loading-screen-text");
            loadingText.RemoveFromClassList("default-text");
            root.Add(loadingText);
            yield break;
        }
    }
}