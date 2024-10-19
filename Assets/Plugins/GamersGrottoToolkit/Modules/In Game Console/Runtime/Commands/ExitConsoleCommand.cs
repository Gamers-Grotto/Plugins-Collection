using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace GamersGrotto.In_Game_Console.Commands
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