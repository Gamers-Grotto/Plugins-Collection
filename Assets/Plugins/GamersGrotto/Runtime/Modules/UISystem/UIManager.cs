using System;
using System.Collections;
using System.Collections.Generic;
using GamersGrotto.Runtime.Core;
using GamersGrotto.Runtime.Modules.UISystem.Commands;
using UnityEngine;

namespace GamersGrotto.Runtime.Modules.UISystem
{
    public class UIManager : Singleton<UIManager>
    {
        private Stack<ICommand> commandStack = new ();

        [SerializeField] private Page splashScreen;
        [SerializeField] private float splashScreenShowDuration = 4f;
        private IEnumerator Start()
        {
            ExecuteCommand(new NavigateToPageCommand(null, splashScreen));
            yield return new WaitForSeconds(splashScreenShowDuration);
            ExecuteCommand(new ClosePageCommand(splashScreen));
        }

        public void NavigateToPage(Page newPage)
        {
            newPage.gameObject.SetActive(true);
        }

        public void ExecuteCommand(ICommand command)
        {
            command.Execute();
            commandStack.Push(command);
        }

        public void GoBack()
        {
            if (commandStack.Count > 0)
            {
                var lastCommand = commandStack.Pop();
                lastCommand.Undo();
            }
        }
    }
}