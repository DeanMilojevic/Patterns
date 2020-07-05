using System.Linq;
using System.Collections.Generic;

namespace Example.Command
{
    public class CommandManager
    {
        private readonly Stack<ICommand> _executed;

        public CommandManager()
        {
            _executed = new Stack<ICommand>();
        }

        public void Invoke(ICommand command)
        {
            if (command.CanExecute())
            {
                _executed.Push(command);
                command.Execute();
            }
        }

        public void Undo()
        {
            while(_executed.Any())
            {
                var command = _executed.Pop();
                command.Undo();
            }
        }
    }
}