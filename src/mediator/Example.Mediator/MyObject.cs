using System;

namespace Example.Mediator
{
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
}