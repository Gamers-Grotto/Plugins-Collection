using GamersGrotto.Runtime.Core;

namespace GamersGrotto.Runtime.Modules.UISystem.Commands
{
    public class ClosePageCommand : ICommand
    {
        private Page pageToHide;

        public ClosePageCommand(Page page) => pageToHide = page;
        public void Execute() => pageToHide.gameObject.SetActive(false);

        public void Undo() => pageToHide.gameObject.SetActive(true);
    }
}