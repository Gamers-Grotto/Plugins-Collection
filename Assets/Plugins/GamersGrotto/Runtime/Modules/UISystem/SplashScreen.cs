using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace GamersGrotto.Runtime.Modules.UISystem
{
    public class SplashScreen : Page
    {
        [SerializeField] private Sprite background;
        
        protected override IEnumerator DrawUI(VisualElement root)
        {
            var backgroundImage = Create<VisualElement>("splash-screen");
            backgroundImage.style.backgroundImage = new StyleBackground(background);
            
            root.Add(backgroundImage);
            yield break;
        }
    }
}