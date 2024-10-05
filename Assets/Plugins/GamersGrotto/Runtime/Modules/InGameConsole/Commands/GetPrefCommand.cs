using System.Threading.Tasks;
using GamersGrotto.Runtime.Core;
using UnityEngine;

namespace GamersGrotto.Runtime.Modules.InGameConsole.Commands
{
    [Command("GetPref")]
    public class GetPrefCommand : ConsoleCommand
    {
        public override Task Execute(string[] parameters)
        {
            if (parameters.Length < 2)
            {
                AdminConsole.Instance.Add(
                    "provide type and name for PlayerPref command. Eg. GetPref string highscore");
                return Task.CompletedTask;
            }

            if (parameters.Length >= 2)
            {
                var key = parameters[1];
                if (!PlayerPrefs.HasKey(key))
                {
                    AdminConsole.Instance.Add($"No PlayerPref found with Key : {parameters[1]}".Colorize("yellow"));
                    return Task.CompletedTask;
                }

                var type = parameters[0].ToLower();

                switch (type)
                {
                    case "string":
                        AdminConsole.Instance.Add($"{key.Bold()} : {PlayerPrefs.GetString(key).Italic()}");
                        break;
                    case "int":
                        AdminConsole.Instance.Add($"{key.Bold()} : {PlayerPrefs.GetInt(key).ToString().Italic()}");
                        break;
                    case "float":
                        AdminConsole.Instance.Add($"{key.Bold()} : {PlayerPrefs.GetFloat(key).ToString("0.000").Italic()}");
                        break;
                    default:
                        AdminConsole.Instance.Add($"Invalid PlayerPref type {type}".Colorize("red"));
                        break;
                }
            }

            return Task.CompletedTask;
        }
    }
}