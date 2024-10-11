using System.Threading.Tasks;

namespace GamersGrotto.Runtime.Modules.InGameConsole.Commands
{
    [Command("Commands")]
    public class CommandsConsoleCommand : ConsoleCommand
    {
        public override Task Execute(string[] parameters)
        {
            var commands = AdminConsole.Instance.CommandList;
            var separated = string.Join(", ", commands);
            AdminConsole.Instance.Add(separated);
            return Task.CompletedTask;
        }
    }
}