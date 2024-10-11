namespace GamersGrotto.Runtime.Modules.UISystem.Commands
{
    public class SwitchToPageCommand : ICommand
    {
        private Page currentPage;
        private Page targetPage;

        public SwitchToPageCommand(Page targetPage)
        {
            this.targetPage = targetPage;
            
            if(UIManager.Instance.CurrentPage != null)
                currentPage = UIManager.Instance.CurrentPage;
        }

        public void Execute()
        {
            UIManager.Instance.SwitchToPage(targetPage);
        }

        public void Undo()
        {
            if(currentPage != null)
                UIManager.Instance.SwitchToPage(currentPage);
        }
    }
}