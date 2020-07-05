using System;
using Xunit;

namespace Example.Command.Tests
{
    public class CommandTests
    {
        [Fact(DisplayName = "Commands can be executed")]
        public void Test1()
        {
            var manager = new CommandManager();

            var command = new MyFirstCommand("my command");

            manager.Invoke(command);
        }

        [Fact(DisplayName = "Commands can't be executed")]
        public void Test2()
        {
            var manager = new CommandManager();

            var command = new MyFirstCommand(string.Empty);

            manager.Invoke(command);
        }

        [Fact(DisplayName = "Undo of the commands")]
        public void Test3()
        {
            var manager = new CommandManager();
            var firstCommand = new MyFirstCommand("my first command");
            var secondCommand = new MySecondCommand(1, "my second command");
            manager.Invoke(firstCommand);
            manager.Invoke(secondCommand);

            manager.Undo();
        }
    }
}
