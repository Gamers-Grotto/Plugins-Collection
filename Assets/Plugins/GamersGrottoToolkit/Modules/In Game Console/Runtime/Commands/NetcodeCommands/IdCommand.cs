using System.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;

namespace GamersGrotto.In_Game_Console.ConsoleCommands.NetcodeCommands
{
    [Command("Id")]
    public class IdCommand : ConsoleCommand
    {
        public override Task Execute(string[] parameters)
        {
            Awaitable.MainThreadAsync();
            
            AdminConsole.Instance.Add(NetworkManager.Singleton.IsConnectedClient 
                ? $"Client Id : {NetworkManager.Singleton.LocalClientId}" 
                : "Not Connected Client");
            
            return Task.CompletedTask;
        }
    }
}