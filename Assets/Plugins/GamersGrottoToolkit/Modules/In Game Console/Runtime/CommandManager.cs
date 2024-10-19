using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

namespace GamersGrotto.In_Game_Console
{
    public class CommandManager
    {
        private const string TAG = "<color=lightblue>CommandManager</color>";
        private Dictionary<string, ConsoleCommand> commandDictionary = new Dictionary<string, ConsoleCommand>();
        public List<string> CommandList => commandDictionary.Keys.ToList();
    
        public void RegisterCommands()
        {
            var commandTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => t.IsSubclassOf(typeof(ConsoleCommand)));
        
            foreach (var type in commandTypes)
            {
                var attributes = type.GetCustomAttributes(typeof(CommandAttribute), true);
                if (attributes.Length > 0)
                {
                    var commandName = ((CommandAttribute)attributes[0]).CommandName;
                    var commandInstance = (ConsoleCommand)Activator.CreateInstance(type);
                    commandDictionary.Add(commandName.ToLower(), commandInstance);
                }
            }
            Debug.Log($"{TAG} Console Commands Added : {commandDictionary.Count}");
        }

        public bool HasCommand(string commandName) => commandDictionary.ContainsKey(commandName.ToLower());

        public async Task RunCommand(string commandName, string[] parameters)
        {
            if(HasCommand(commandName))
                await commandDictionary[commandName.ToLower()].Execute(parameters);

        }
    }
}