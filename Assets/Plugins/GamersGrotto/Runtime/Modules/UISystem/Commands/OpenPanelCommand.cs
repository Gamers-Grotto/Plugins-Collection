using GamersGrotto.Runtime.Core;
using UnityEngine.UIElements;

namespace GamersGrotto.Runtime.Modules.UISystem.Commands
{
    public class OpenPanelCommand : ICommand
    {
        private VisualElement panel;
        private bool wasPreviouslyOpen;

        public OpenPanelCommand(VisualElement panel)
        {
            this.panel = panel;
            this.wasPreviouslyOpen = panel.style.display == DisplayStyle.Flex;
        }

        public void Execute() => panel.style.display = DisplayStyle.Flex;

        public void Undo() => panel.style.display = wasPreviouslyOpen ? DisplayStyle.Flex : DisplayStyle.None;
    }
}