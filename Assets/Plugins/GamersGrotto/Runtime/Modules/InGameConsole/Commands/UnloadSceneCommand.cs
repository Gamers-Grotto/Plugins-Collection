using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamersGrotto.Runtime.Modules.InGameConsole.Commands
{
    [Command("UnloadScene")]
    public class UnloadSceneCommand : ConsoleCommand
    {
        public override async Task Execute(string[] parameters)
        {
            var sceneName = parameters[0];
            var scene = SceneManager.GetSceneByName(sceneName);
            
            if (scene.IsValid() && scene.isLoaded)
                await SceneManager.UnloadSceneAsync(sceneName);
        }
    }
}