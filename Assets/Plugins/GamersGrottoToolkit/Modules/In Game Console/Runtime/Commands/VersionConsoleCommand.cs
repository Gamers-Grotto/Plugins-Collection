using System.Threading.Tasks;
using GamersGrotto.Core;
using UnityEngine;

namespace GamersGrotto.In_Game_Console.Commands
{
    [Command("Version")]
    public class VersionConsoleCommand : ConsoleCommand
    {
        public override Task Execute(string[] parameters)
        {
            AdminConsole.Instance.Add($"Version: {Application.version}".Colorize("yellow"));
            return Task.CompletedTask;
        }
    }
}