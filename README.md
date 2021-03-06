# Patterns

This code-base showcases the usage and implementation of the following categories of patterns:

1. Behavioral
2. Structural
3. Creational

As many things in the development world, it is good to know about them. Not in details, hence the idea for me to create the repository as a reference point, just about what they are and when you should use them. In the real-world scenarios I used a sub-set of this patterns. Some of them only in the "example" projects, like this one. That doesn't mean that there will not come the time when I will need one of them.

## Creational patterns

The creational design patterns are design patterns that deal with object creation mechanisms. Or remove the complexity of the object creation in an uniform way. Creational design patterns solve this problem by introducing a way to create the object and hide the complexity behind it.

### Singleton

This is one of the more "interesting" patterns. It is being used quite often (with every new job I do tend to see couple of them in the code-base). What is so "interesting" about them is that they are considered "anti-pattern". That being said, honestly it's usually such a trivial thing in most code-bases that this falls on the bottom of the discussion related to the particular code-base. Mostly as there is much worse things out there that demand some immediate attention.

Non the less, the cleanest way I managed to find and use over the recent years (in the .net ecosystem) is to use the `Lazy<T>` that has being introduced in .NET 4. This reduces a lot of "manual" code that you need to write and it is hidden by the framework itself. I am referring here to making the `Singleton` thread safe and all. The example above shows this implementation in action ([implementation](./src/singleton/Example.Singleton/LazySingleton.cs)):

```csharp
public sealed class LazySingleton
{
    private static readonly Lazy<LazySingleton> _lazy = new Lazy<LazySingleton>(() => new LazySingleton());

    private LazySingleton()
    {
        Console.WriteLine("In the constructor of LazySingleton");
    }

    public static LazySingleton Instance
    {
        get
        {
            Console.WriteLine("Instance called!");
            return _lazy.Value;
        }
    }

    public void Print(int number)
    {
        Console.WriteLine($"Printing {number}");
    }
}
```

If you run the test that is included within the reference project and that forces some parallel code to run and print a simple number out you will see something as following:

```bash
Starting test execution, please wait...

A total of 1 test files matched the specified pattern.
Instance called
Instance called
Instance called
Instance called
Instance called
In the constructor of LazySingleton
Printing 4
Printing 3
Printing 2
Printing 5
Printing 1

Test Run Successful.
Total tests: 1
     Passed: 1
 Total time: 0.8743 Seconds
```

And that is it... Simple and to the point.

## Behavioral patterns

The behavioral design patterns are design patterns that identify communication patterns between objects and "simplify" it (or streamline it). These patterns increase flexibility in carrying out this communication.

### Null Object

This is also one of the debatable patterns I have encountered over the years. There are multiple of different implementations I saw (and implemented by myself) so it also makes it opinionated about the "right" way to do it. There is a simple idea behind it. That is to explicitly model the "missing" of the value instead of relaying on the language/framework support for this (think using `null`/`nil`/whatever your language uses to indicate "absence" of the value). The following details one approach on how this can be done (in simplified manner, just to illustrate the point):

```csharp
public interface IMyObject
{
    public int Id { get; }
    public string Value { get; }
}

public class MyObject : IMyObject
{
  public MyObject(int id, string value)
  {
      Id = id;
      Value = value;
  }

  public int Id { get; }
  public string Value { get; }
}

public class MyNullObject : IMyObject
{
    public MyNullObject()
    {
        Id = -1;
        Value = "N/A";
    }

    public int Id { get; }
    public string Value { get; }
}
```

As already pointed out, this is one of the ways to implement it. Also one of the popular patterns is having an "empty" factory on the class itself:

```csharp
public class MyObject
{
    private static readonly Lazy<MyObject> _empty = new Lazy<MyObject>(() => new MyObject(-1, "N/A"));

    public MyObject(int id, string value)
    {
        Id = id;
        Value = value;
    }

    public int Id { get; }
    public string Value { get; }

    public static Empty { get; } => _empty.Value;
}
```

As you can imagine, this pattern opens a door to flood of the opinions and some are valid and some are not. This is something that always depends on the code-base and what team wants to do about this situations. There is a simple rule for me when I decide to implement this pattern in my code:

1. Implement it on the boundaries of your domain and don't use the `null` (or `null` checks) anywhere else.
2. Don't mix usage of the `null` and `Null Object` pattern it makes just even more confusing.

### Command

This is one of the patterns that I had opportunity to use from time to time (not to be mistaken with CQS Principle tho) on different projects. It allows separation of the command execution and the command executor. The command executor provides the command that needs to be run and passes it along to the middle party. This middle party is fully responsible to handle the command execution in an uniform way. The common contract that "middle-party" provides within this pattern are the following:

1. Can command be executed (precondition check).
2. Command execution.
3. Undo of the executed command.

The contract to describe a command:

```csharp
public interface ICommand
{
    bool CanExecute();
    void Execute();
    void Undo();
}
```

The "middle-party" (often referred to as Invoker or Command Manager) looks similar to this (simplified version):

```csharp
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
```

The internal workings of the `ICommand` contract implementation is not something that Command Manager/Invoker should care about. Same goes other way around. The `Command` should not be concerned about how it is gonna be executed. There are multiple flavours of this pattern I saw over the time. Sometimes a specific manager per feature/domain within the system. Sometimes a generic one that handles all different implementations of the `ICommand` (this is quite a good starting point, tho it usually hits the wall with different requirements in different parts of the system).

### Mediator

This is one of the more fun patterns that handles the communication between the objects. To handle this, we have the a "mediator" between the objects that is responsible to "abstract" away the communication between the objects in the system. This way we can add more and more objects and they will not know about each other. They will use the "mediator" to handle the communication and keep the separation and coupling to the minimum.

The example of the object and a simple contract (there are multiple ways to achieve this, this is just one way to this)

```csharp
public interface IMyObject
{
    void Register(Mediator mediator);
    void Send(string message);
    void Handle(string message);
}

public class MyObject : IMyObject
{
    private string _name;
    private Mediator _mediator;

    public MyObject(string name)
    {
        _name = name;
    }

    public void Register(Mediator mediator)
    {
        _mediator = mediator;
    }

    public void Send(string message)
    {
        _mediator.Send(message, this);
    }

    public void Handle(string message)
    {
        Console.WriteLine($"{_name} handled {message}");
    }
}
```

And now it is time for the "mediator":

```csharp
public class Mediator
{
    private readonly List<IMyObject> _objects;

    public Mediator()
    {
        _objects = new List<IMyObject>();
    }

    public void Register(IMyObject obj)
    {
        obj.Register(this);
        _objects.Add(obj);
    }

    public void Send(string message, IMyObject obj)
    {
        _objects
          .Where(o => o != obj)
          .ToList()
          .ForEach(obj => obj.Handle(message));
    }
}
```

Simple and to the point. Tho, if you ever need to use this pattern, there are quite a lot of good solutions out there that you can reference. Usually I end up with the [MediatR](https://github.com/jbogard/MediatR) from Jimmy Bogard. It is a quite popular project and handles a lot of this "intermediate" stuff for you.
