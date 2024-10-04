using System.Threading.Tasks;

namespace GamersGrotto.Runtime.Modules.InGameConsole.Commands
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