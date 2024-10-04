using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace GamersGrotto.Runtime.Modules.InGameConsole.Commands
{
    [Command("Exit")]
    public class ExitConsoleCommand : ConsoleCommand
    {
        public override async Task Execute(string[] parameters)
        {
            AdminConsole.Instance.Add("Exiting...");
            await Awaitable.EndOfFrameAsync();

#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
        }
    }
}