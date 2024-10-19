    namespace GamersGrotto.Core {
        public interface ICommand
        {
            void Execute();
            void Undo();
        }
    }
