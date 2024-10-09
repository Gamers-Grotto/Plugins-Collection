using GamersGrotto.Runtime.Core;

namespace GamersGrotto.Runtime.Modules.UISystem.Commands
{
    public class NavigateToPageCommand : ICommand
    {
        private Page currentPage;
        private Page targetPage;

        public NavigateToPageCommand(Page targetPage)
        {
            this.targetPage = targetPage;
            
            if(UIManager.Instance.CurrentPage != null)
                currentPage = UIManager.Instance.CurrentPage;
        }

        public void Execute()
        {
            UIManager.Instance.GotoPage(targetPage);
        }

        public void Undo()
        {
            if(currentPage != null)
                UIManager.Instance.GotoPage(currentPage);
        }
    }
}