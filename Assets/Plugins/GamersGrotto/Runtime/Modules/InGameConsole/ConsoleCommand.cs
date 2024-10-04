using System.Threading.Tasks;

namespace GamersGrotto.Runtime.Modules.InGameConsole
{
    public abstract class ConsoleCommand
    {
        public abstract Task Execute(string[] parameters);
    }
}