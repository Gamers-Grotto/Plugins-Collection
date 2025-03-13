using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace GamersGrotto.In_Game_Console.ConsoleCommands.NetcodeCommands
{
    [Command("Kick")]
    public class KickCommand : ConsoleCommand
    {
        public override Task Execute(string[] parameters)
        {
            Awaitable.MainThreadAsync();

            if (NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsHost)
            {
                if (parameters.Length < 1)
                {
                    AdminConsole.Instance.Add("Please provide an ClientId to kick. eg. Kick 1");
                    return Task.CompletedTask;
                }
                
                var clientId = ulong.Parse(parameters[0]);
                NetworkManager.Singleton.DisconnectClient(clientId);
                AdminConsole.Instance.Add($"{clientId} has been kicked.");
                return Task.CompletedTask;
            }
            
            AdminConsole.Instance.Add("You do not have permission to kick users. Only the server can kick users.");
            return Task.CompletedTask;
        }
    }
}