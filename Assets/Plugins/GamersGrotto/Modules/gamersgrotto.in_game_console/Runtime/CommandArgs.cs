using System;

namespace GamersGrotto.Runtime.Modules.InGameConsole
{
    public class CommandArgs
    {
        public string Command { get; private set; }
        public string[] Parameters { get; private set; }

        
        public CommandArgs(string command)
        {
            if (string.IsNullOrEmpty(command))
                throw new Exception("Invalid Command, a command cannot be an empty string");
            
            var parameters = Parameterize(command.Trim());
            
            switch (parameters.Length)
            {
                case 1:
                    Command = parameters[0].ToLower();
                    Parameters = Array.Empty<string>();
                    return;
                case >= 2:
                    Command = parameters[0];
                    Parameters = parameters[1..];
                    break;
            }
        }
        
        private static string[] Parameterize(string command)
        {
            return string.IsNullOrEmpty(command) 
                ? Array.Empty<string>() 
                : command.Split();
        }
    }
}