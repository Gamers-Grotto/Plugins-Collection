using System.Threading.Tasks;
using GamersGrotto.Runtime.Core;
using UnityEngine;

namespace GamersGrotto.Runtime.Modules.InGameConsole.Commands
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