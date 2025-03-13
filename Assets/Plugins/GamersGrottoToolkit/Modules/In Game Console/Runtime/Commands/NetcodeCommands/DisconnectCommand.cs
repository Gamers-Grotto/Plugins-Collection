using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace GamersGrotto.In_Game_Console.ConsoleCommands.NetcodeCommands
{
    [Command("Disconnect")]
    public class DisconnectCommand : ConsoleCommand
    {
        public override Task Execute(string[] parameters)
        {
            Awaitable.MainThreadAsync();
            
            if(!NetworkManager.Singleton.IsConnectedClient)
            {
                AdminConsole.Instance.Add("You are not connected to any server.");
                return Task.CompletedTask;
            }

            if(NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsServer)
                NetworkManager.Singleton.Shutdown();
            
            AdminConsole.Instance.Add("You have been disconnected.");
            return Task.CompletedTask;
        }
    }
}