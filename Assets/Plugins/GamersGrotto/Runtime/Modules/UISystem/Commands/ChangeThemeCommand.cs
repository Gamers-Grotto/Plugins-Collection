using GamersGrotto.Runtime.Core;
using UnityEngine.UIElements;

namespace GamersGrotto.Runtime.Modules.UISystem.Commands
{
    public class ChangeThemeCommand : ICommand
    {
        private VisualElement root;
        private StyleSheet newTheme;
        private StyleSheet previousTheme;

        public ChangeThemeCommand(VisualElement root, StyleSheet newTheme)
        {
            this.root = root;
            this.newTheme = newTheme;
            this.previousTheme = root.styleSheets.count > 0 ? root.styleSheets[0] : null;
        }

        public void Execute()
        {
            if (previousTheme != null) 
                root.styleSheets.Remove(previousTheme);
            
            root.styleSheets.Add(newTheme);
        }

        public void Undo()
        {
            root.styleSheets.Remove(newTheme);
            
            if (previousTheme != null) 
                root.styleSheets.Add(previousTheme);
        }
    }
}