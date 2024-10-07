using System.Collections;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

namespace GamersGrotto.Runtime.Modules.InGameConsole.Commands
{
    /// <summary>
    /// Ways to improve:
    /// 1. One Command to store and display all methods with the ConsoleCommand attribute
    /// One method to display all methods found with the first command
    /// and one method to execute the method by name
    /// </summary>
    [Command("UpdateMethods")]
    public class UpdateMethodsListCommand : ConsoleCommand
    {
       // public StringListSO commandsList;
        public override Task Execute(string[] parameters)
        {
            
           // commandsList = Resources.Load<StringListSO>("CommandsList");
            var commands = System.AppDomain.CurrentDomain.GetAssemblies()
                .AsParallel()
                .SelectMany(assembly => assembly.GetTypes())
                .AsParallel()
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
                .Where(method => method.GetCustomAttributes<ConsoleCommandAttribute>().Any())
                .Select(method => method.Name)
                .ToList(); 

            var commandsString = string.Join(", ", commands);
           // commands.ForEach(command => ((IList)commandsList.list).Add(command));
            AdminConsole.Instance.Add(commandsString);
            return Task.CompletedTask;
        }
    }
}
