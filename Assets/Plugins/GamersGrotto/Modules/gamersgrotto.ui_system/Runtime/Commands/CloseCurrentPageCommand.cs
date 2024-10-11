namespace GamersGrotto.Runtime.Modules.UISystem.Commands
{
    public class CloseCurrentPageCommand : ICommand
    {
        private readonly Page _currentPage = UIManager.Instance.CurrentPage;

        public void Execute()
        {
            if(_currentPage)
                _currentPage.gameObject.SetActive(false);
        }

        public void Undo()
        {
            if(_currentPage)
                _currentPage.gameObject.SetActive(true);
        }
    }
}