using System;

namespace Example.Command
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var manager = new CommandManager();
            var firstCommand = new MyFirstCommand("my first command");
            var secondCommand = new MySecondCommand(1, "my second command");
            manager.Invoke(firstCommand);
            manager.Invoke(secondCommand);

            manager.Undo();
        }
    }
}
