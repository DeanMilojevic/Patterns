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
