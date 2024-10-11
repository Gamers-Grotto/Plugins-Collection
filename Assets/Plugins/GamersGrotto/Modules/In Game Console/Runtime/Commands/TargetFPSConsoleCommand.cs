using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GamersGrotto.Runtime.Modules.InGameConsole.Commands
{
    [Command("TargetFPS")]
    public class TargetFPSConsoleCommand : ConsoleCommand
    {
        public override Task Execute(string[] parameters)
        {
            if (parameters.Length < 1)
            {
                AdminConsole.Instance.Add($"<color=orange>TargetFPS</color> is {Application.targetFrameRate}");
                return Task.CompletedTask;
            }

            try
            {
                var target = int.Parse(parameters[0]);
                
                if (target < -1) target = -1;
                
                Application.targetFrameRate = target;
            }
            catch (Exception)
            {
                Debug.LogError($"Invalid target frame rate parameter, integer expected");
            }

            return Task.CompletedTask;
        }
    }
}