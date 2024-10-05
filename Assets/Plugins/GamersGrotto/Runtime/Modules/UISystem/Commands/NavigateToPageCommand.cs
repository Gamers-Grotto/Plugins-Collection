using GamersGrotto.Runtime.Core;

namespace GamersGrotto.Runtime.Modules.UISystem.Commands
{
    public class NavigateToPageCommand : ICommand
    {
        private Page currentPage;
        private Page targetPage;

        public NavigateToPageCommand(Page currentPage, Page targetPage)
        {
            this.currentPage = currentPage;
            this.targetPage = targetPage;
        }

        public void Execute()
        {
            if(currentPage != null)
                currentPage.gameObject.SetActive(false);
            
            UIManager.Instance.NavigateToPage(targetPage);
        }

        public void Undo() => UIManager.Instance.NavigateToPage(currentPage);
    }
}