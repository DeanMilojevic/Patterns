using System;

namespace Example.Observer
{
    public abstract class Observer
    {
        private Subject _subject;
        
        public void Subscribe(Subject subject)
        {
            _subject = subject;
            _subject.Add(this);
        }

        public abstract void Update(string message);
    }

    public class FirstObserver : Observer
    {
        public override void Update(string message)
        {
            Console.WriteLine($"{nameof(FirstObserver)} received a message: {message}");
        }
    }

    public class SecondObserver : Observer
    {
        public override void Update(string message)
        {
            Console.WriteLine($"{nameof(SecondObserver)} received a message: {message}");
        }
    }
}