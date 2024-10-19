using System.Threading.Tasks;
using GamersGrotto.Core;
using UnityEngine.Device;

namespace GamersGrotto.In_Game_Console.Commands
{
    [Command("Device")]
    public class DeviceCommand : ConsoleCommand
    {
        public override Task Execute(string[] parameters)
        {
            var deviceName = SystemInfo.deviceName;
            var deviceId = SystemInfo.deviceUniqueIdentifier;
            var os = SystemInfo.operatingSystem;
            var memSize = SystemInfo.systemMemorySize;
            var graphicsMemSize = SystemInfo.graphicsMemorySize;
            var gpuVendor = SystemInfo.graphicsDeviceVendor;
            var gpuDeviceName = SystemInfo.graphicsDeviceName;
            
            AdminConsole.Instance.Add($"Device Info \n \n".Bold() +
                                      $"Device Name  : {deviceName} \n" +
                                      $"Device Id : {deviceId} \n" +
                                      $"Operating System  : {os} \n" +
                                      $"Memory : {memSize} \n" +
                                      $"GPU : {gpuVendor} - {gpuDeviceName} \n" +
                                      $"GPU Memory Size : {graphicsMemSize}");
            
            return Task.CompletedTask;
        }
    }
}