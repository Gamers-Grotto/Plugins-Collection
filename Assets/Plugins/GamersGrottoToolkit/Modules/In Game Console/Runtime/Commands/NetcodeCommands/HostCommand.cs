using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace GamersGrotto.In_Game_Console.ConsoleCommands.NetcodeCommands
{
    [Command("Host")]
    public class HostCommand : ConsoleCommand
    {
        public override async Task Execute(string[] parameters)
        {
            await Awaitable.MainThreadAsync();
            
            if(NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsConnectedClient)
            {
                AdminConsole.Instance.Add("You are already hosting");
                NetworkManager.Singleton.Shutdown();
                return;
            }
            
            NetworkManager.Singleton.StartHost();
            AdminConsole.Instance.Add("Host is running...");
        }
    }
}