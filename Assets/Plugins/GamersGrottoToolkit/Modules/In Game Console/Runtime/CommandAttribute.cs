using System;

namespace GamersGrotto.In_Game_Console
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