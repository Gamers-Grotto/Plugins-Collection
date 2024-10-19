using System.Threading.Tasks;

namespace GamersGrotto.In_Game_Console.Commands
{
    [Command("Clear")]
    public class ClearConsoleCommand : ConsoleCommand
    {
        public override Task Execute(string[] parameters)
        {
            AdminConsole.Instance.Clear();
            return Task.CompletedTask;
        }
    }
}