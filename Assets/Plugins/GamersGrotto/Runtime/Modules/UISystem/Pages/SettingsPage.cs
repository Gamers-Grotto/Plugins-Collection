using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace GamersGrotto.Runtime.Modules.UISystem.Pages
{
    public class SettingsPage : Page
    {
        protected override IEnumerator DrawUI(VisualElement root)
        {
            Debug.Log("Settings Page");

            var background = Create("full-screen");
            root.Add(background);
            
            var backButton = CreateButton("Back", () => UIManager.Instance.GoBack());
            backButton.AddToClassList("bottom-right-button");
            root.Add(backButton);
            
            yield break;
        }
    }
}