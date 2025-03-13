using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace GamersGrotto.In_Game_Console.ConsoleCommands.NetcodeCommands
{
    [Command("Join")]
    public class JoinCommand : ConsoleCommand
    {
        public override async Task Execute(string[] parameters)
        {
            await Awaitable.MainThreadAsync();
            
            if(NetworkManager.Singleton.IsClient)
            {
                AdminConsole.Instance.Add($"Already connected to server : {NetworkManager.Singleton.ConnectedHostname}");
                return;
            }

            if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsServer)
            {
                AdminConsole.Instance.Add("You are already the host/server");
                return;
            }

            await Awaitable.NextFrameAsync();
            NetworkManager.Singleton.StartClient();
            AdminConsole.Instance.Add("Starting client...");
        }
    }
}