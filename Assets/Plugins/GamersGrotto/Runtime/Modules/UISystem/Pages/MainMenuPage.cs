using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

namespace GamersGrotto.Runtime.Modules.UISystem.Pages
{
    public class MainMenuPage : Page
    {
        [SerializeField] private Sprite backgroundImage;
        
        public UnityEvent playButtonClicked;
        
        protected override IEnumerator DrawUI(VisualElement root)
        {
            var background = Create<VisualElement>("full-screen");
            if (backgroundImage)
            {
                background.style.color = new StyleColor(Color.clear);
                background.style.backgroundImage = new StyleBackground(backgroundImage);
            }
            
            var title = CreateLabel("Gamers Grotto");
            title.AddToClassList("title-text");
            
            var playButton = CreateButton("Play", () => playButtonClicked?.Invoke());
            var settingsButton = CreateButton("Settings", () => UIManager.Instance.GotoSettingsPage());
            var quitButton = CreateButton("Quit", OnQuitButtonClicked);
            
            var box = CreateBox();
            
            box.Add(playButton);
            box.Add(settingsButton);
            box.Add(quitButton);
            
            background.Add(title);
            background.Add(box);
            
            root.Add(background);
            
            yield return null;
        }

        private void OnQuitButtonClicked()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}