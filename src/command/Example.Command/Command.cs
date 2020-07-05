using System;

namespace Example.Command
{
  public interface ICommand
  {
      bool CanExecute();
      void Execute();
      void Undo();
  }

  public class MyFirstCommand : ICommand
  {
      private readonly string _message;

      public MyFirstCommand(string message)
      {
          _message = message;
      }

      public bool CanExecute()
      {
          return !string.IsNullOrEmpty(_message);
      }

      public void Execute()
      {
          Console.WriteLine(_message);
      }

      public void Undo()
      {
          Console.WriteLine($"Undo: {_message}");
      }
  }

  public class MySecondCommand : ICommand
  {
      private readonly int _id;
      private readonly string _value;

      public MySecondCommand(int id, string value)
      {
          _id = id;
          _value = value;
      }

      public bool CanExecute()
      {
          return _id > 0 && !string.IsNullOrEmpty(_value);
      }

      public void Execute()
      {
          Console.WriteLine($"The id is {_id} and value is {_value}");
      }

      public void Undo()
      {
          Console.WriteLine($"Undo: {_id} - {_value}");
      }
  }
}