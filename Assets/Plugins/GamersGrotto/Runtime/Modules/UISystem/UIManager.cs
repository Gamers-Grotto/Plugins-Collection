using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GamersGrotto.Runtime.Core;
using GamersGrotto.Runtime.Modules.UISystem.Commands;
using GamersGrotto.Runtime.Modules.UISystem.Pages;

namespace GamersGrotto.Runtime.Modules.UISystem
{
    public class UIManager : Singleton<UIManager>
    {
        private Stack<ICommand> commandStack = new ();

        [Header("Splash Screen")]
        [SerializeField] private Page splashScreen;
        [SerializeField] private float splashScreenShowDuration = 4f;
        [Header("Pages")]
        [SerializeField] private Page mainMenuPage;
        [SerializeField] private SettingsPage settingsPage;
        
        public Page CurrentPage { get; private set; }

        #region MonoBahaviour
        private IEnumerator Start()
        {
            ExecuteCommand(new NavigateToPageCommand(splashScreen));
            yield return new WaitForSeconds(splashScreenShowDuration);
            ExecuteCommand(new NavigateToPageCommand(mainMenuPage));
        }
        #endregion

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            commandStack.Push(command);
        }
        
        public void GotoPage(Page newPage)
        {
            if(CurrentPage != null)
                CurrentPage.gameObject.SetActive(false);
            
            CurrentPage = newPage;
            newPage.gameObject.SetActive(true);
        }

        public void GotoMainMenuPage() => ExecuteCommand(new NavigateToPageCommand(mainMenuPage));
        
        public void GotoSettingsPage() => ExecuteCommand(new NavigateToPageCommand(settingsPage));

        public void GoBack()
        {
            if (commandStack.Count > 0)
                commandStack.Pop().Undo();
        }
    }
}