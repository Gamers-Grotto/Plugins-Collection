using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace GamersGrotto.In_Game_Console.ConsoleCommands.NetcodeCommands
{
    [Command("Clients")]
    public class ClientsCommand : ConsoleCommand
    {
        public override Task Execute(string[] parameters)
        {
            Awaitable.MainThreadAsync();
            
            if (!(NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsHost))
            {
                AdminConsole.Instance.Add($"Only the server is allowed to retrieve client ids");
                return Task.CompletedTask;
            }

            var clients = NetworkManager.Singleton.ConnectedClientsIds;

            AdminConsole.Instance.Add($"Client Count : {clients.Count}");
                
            foreach (var client in clients)
            {
                AdminConsole.Instance.Add($"Client Id : {client}");
            }
            
            return Task.CompletedTask;
        }
    }
}