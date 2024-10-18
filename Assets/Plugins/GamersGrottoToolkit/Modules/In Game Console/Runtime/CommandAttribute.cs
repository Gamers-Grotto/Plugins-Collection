using System;

namespace GamersGrotto.Runtime.Modules.InGameConsole
{
    public class CommandAttribute : Attribute
    {
        public string CommandName { get; }
    
        public CommandAttribute(string commandName)
        {
            CommandName = commandName;
        }
    }
}