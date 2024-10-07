namespace GamersGrotto.Runtime.Core
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }
}