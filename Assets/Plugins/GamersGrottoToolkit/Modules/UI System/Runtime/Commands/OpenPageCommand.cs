using GamersGrotto.Core;

namespace GamersGrotto.Ui_System.Commands
{
    public class OpenPageCommand : ICommand
    {
        private Page _page;
        private bool openedPage;
        
        public OpenPageCommand(Page page)
        {
            _page = page;
        }
        public void Execute()
        {
            if (_page.IsOpen) 
                return;
            
            _page.gameObject.SetActive(true);
            openedPage = true;
        }

        public void Undo()
        {
            if(!openedPage)
                return;
            
            _page.gameObject.SetActive(false);
        }
    }
}