using GamersGrotto.Runtime.Core;
using UnityEngine.UIElements;

namespace GamersGrotto.Runtime.Modules.UISystem.Commands
{
    public class ChangeTextCommand : ICommand
    {
        private Label label;
        private string newText;
        private string previousText;

        public ChangeTextCommand(Label label, string newText)
        {
            this.label = label;
            this.newText = newText;
            this.previousText = label.text;
        }

        public void Execute() => label.text = newText;

        public void Undo() => label.text = previousText;
    }
}