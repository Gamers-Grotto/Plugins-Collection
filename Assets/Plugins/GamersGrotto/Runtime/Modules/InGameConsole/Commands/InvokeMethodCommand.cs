using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;

namespace GamersGrotto.Runtime.Modules.InGameConsole.Commands {
    /// <summary>
    /// Does not work on Scriptable Objects
    /// Works on MonoBehaviours
    /// Works on Static Methods
    /// </summary>
    [Command("InvokeMethod")]
    public class InvokeMethodCommand : ConsoleCommand {
        public override Task Execute(string[] parameters) {
            if (parameters.Length == 0) {
                AdminConsole.Instance.Add("No method name provided.");
                return Task.CompletedTask;
            }

            string methodName = parameters[0];
            var method = System.AppDomain.CurrentDomain.GetAssemblies()
                .AsParallel()
                .SelectMany(assembly => assembly.GetTypes())
                .AsParallel()
                .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public |
                                                    BindingFlags.NonPublic))
                .FirstOrDefault(m => m.Name == methodName && m.GetCustomAttributes<ConsoleCommandAttribute>().Any());

            if (method == null) {
                AdminConsole.Instance.Add($"Method '{methodName}' not found.");
                return Task.CompletedTask;
            }

            if (method.IsStatic) {
                try {
                    method.Invoke(null, null);
                    AdminConsole.Instance.Add($"Static method '{methodName}' invoked successfully.");
                }
                catch (Exception ex) {
                    AdminConsole.Instance.Add($"Error invoking static method '{methodName}': {ex.Message}");
                }
            }
            else {
                var declaringType = method.DeclaringType;
                var objects = UnityEngine.Object.FindObjectsOfType(declaringType);

                if (objects.Length == 0) {
                    AdminConsole.Instance.Add(
                        $"No objects found with script '{declaringType.Name}' containing method '{methodName}'.");
                }

                foreach (var obj in objects) {
                    try {
                        method.Invoke(obj, null);
                        AdminConsole.Instance.Add(
                            $"Method '{methodName}' invoked successfully on object '{((Component)obj).gameObject.name}'.");
                    }
                    catch (Exception ex) {
                        AdminConsole.Instance.Add(
                            $"Error invoking method '{methodName}' on object '{((Component)obj).gameObject.name}': {ex.Message}");
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}