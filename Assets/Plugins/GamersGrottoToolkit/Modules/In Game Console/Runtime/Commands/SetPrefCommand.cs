using System;
using System.Threading.Tasks;
using GamersGrotto.Core;
using UnityEngine;

namespace GamersGrotto.In_Game_Console.Commands
{
    [Command("SetPref")]
    public class SetPrefCommand : ConsoleCommand
    {
        public override Task Execute(string[] parameters)
        {
            if (parameters.Length < 3)
            {
                AdminConsole.Instance.Add(
                    "provide type and name for PlayerPref command. Eg. SetPref string highscore");
                return Task.CompletedTask;
            }

            var type = parameters[0].ToLower();
            var key = parameters[1];

            switch (type)
            {
                case "string":
                    var strValue = string.Join(" ", parameters[2..]);

                    if (string.IsNullOrEmpty(strValue) || string.IsNullOrWhiteSpace(strValue))
                        break;

                    PlayerPrefs.SetString(key, strValue);
                    AdminConsole.Instance.Add($"{key.Bold()}:{type.Italic().Colorize("orange")} - {strValue}");
                    break;
                case "int":
                    try
                    {
                        var intValue = int.Parse(parameters[2]);
                        PlayerPrefs.SetInt(key, intValue);
                        AdminConsole.Instance.Add($"{key.Bold()}: {type.Italic().Colorize("orange")} - {intValue}");
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }

                    break;
                case "float":
                    try
                    {
                        var floatValue = float.Parse(parameters[2]);
                        PlayerPrefs.SetFloat(key, floatValue);
                        AdminConsole.Instance.Add($"{key.Bold()}: {type.Italic().Colorize("orange")} - {floatValue}");
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                    }

                    break;
                default:
                    AdminConsole.Instance.Add($"Invalid PlayerPref type {type}".Colorize("red"));
                    break;
            }

            return Task.CompletedTask;
        }
    }
}