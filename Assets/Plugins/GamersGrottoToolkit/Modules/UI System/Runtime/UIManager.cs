using System.Collections;
using System.Collections.Generic;
using GamersGrotto.Ui_System.Commands;
using GamersGrotto.Ui_System.Pages;
using UnityEngine;

namespace GamersGrotto.Ui_System
{
    public class UIManager : Singleton<UIManager>
    {
        [Header("Startup Behaviour")]
        [SerializeField] private bool showSplashScreenOnStartup = true;
        [Tooltip("main menu will be displayed after splash screen, if its enabled")]
        [SerializeField] private bool openMainMenuOnStartup = true;
        [Header("Splash Screen")] 
        [SerializeField] private Page splashScreen;
        [SerializeField] private float splashScreenShowDuration = 3f;
        [Header("Pages")]
        [SerializeField] private Page mainMenuPage;
        [SerializeField] private SettingsPage settingsPage;
        [SerializeField] private LoadingScreen loadingScreen;
        
        private Stack<ICommand> commandStack = new ();
        
        public Page CurrentPage { get; private set; }

        #region MonoBahaviour
        private IEnumerator Start()
        {
            if (showSplashScreenOnStartup && splashScreen)
            {
                SwitchToPage(splashScreen);
                yield return new WaitForSeconds(splashScreenShowDuration);
            }
            
            if(openMainMenuOnStartup)
                GotoMainMenuPage();
        }
        #endregion

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            commandStack.Push(command);
        }
        
        public void SwitchToPage(Page newPage)
        {
            if(CurrentPage != null)
                CurrentPage.gameObject.SetActive(false);
            
            CurrentPage = newPage;
            newPage.gameObject.SetActive(true);
        }

        public void GotoMainMenuPage() => ExecuteCommand(new SwitchToPageCommand(mainMenuPage));
        
        public void GotoSettingsPage() => ExecuteCommand(new SwitchToPageCommand(settingsPage));

        public void ShowLoadingScreen() => ExecuteCommand(new SwitchToPageCommand(loadingScreen));

        public void HideLoadingScreen() => ExecuteCommand(new ClosePageCommand(loadingScreen));

        public void GoBack()
        {
            if (commandStack.Count > 0)
                commandStack.Pop().Undo();
        }
    }
}