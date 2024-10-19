using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GamersGrotto.In_Game_Console.Commands
{
    [Command("Timescale")]
    public class TimeScaleConsoleCommand : ConsoleCommand
    {
        public override Task Execute(string[] parameters)
        {
            if (parameters.Length < 1)
            {
                AdminConsole.Instance.Add($"<color=orange>Timescale</color> is {Time.timeScale}");
                return Task.CompletedTask;
            }

            try
            {
                var speed = float.Parse(parameters[0]);
                Time.timeScale = MathF.Abs(speed);
                AdminConsole.Instance.Add($"<color=orange>Timescale</color> set to {Time.timeScale}");
            }
            catch (Exception)
            {
                AdminConsole.Instance.Add("Invalid parameter for <color=orange>Timescale</color>.");
            }

            return Task.CompletedTask;
        }
    }
}