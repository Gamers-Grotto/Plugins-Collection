﻿using UnityEngine.UIElements;

namespace GamersGrotto.Runtime.Modules.UISystem.Commands
{
    public class ToggleVisibilityCommand : ICommand
    {
        private VisualElement uiElement;
        private bool isVisible;

        public ToggleVisibilityCommand(VisualElement uiElement, bool isVisible)
        {
            this.uiElement = uiElement;
            this.isVisible = isVisible;
        }

        public void Execute()
        {
            uiElement.style.display = isVisible ? DisplayStyle.Flex : DisplayStyle.None;
        }

        public void Undo()
        {
            uiElement.style.display = !isVisible ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}