using System.Threading.Tasks;

namespace GamersGrotto.In_Game_Console.Commands
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