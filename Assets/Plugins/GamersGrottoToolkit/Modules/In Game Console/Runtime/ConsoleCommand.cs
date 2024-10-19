using System.Threading.Tasks;

namespace GamersGrotto.In_Game_Console
{
    public abstract class ConsoleCommand
    {
        public abstract Task Execute(string[] parameters);
    }
}