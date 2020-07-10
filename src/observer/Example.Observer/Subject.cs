using System.Collections.Generic;

namespace Example.Observer
{
    public class Subject
    {
        private readonly List<Observer> _observers;

        public Subject()
        {
            _observers = new List<Observer>();
        }

        internal void Add(Observer observer)
        {
            _observers.Add(observer);
        }

        public void Notify(string message)
        {
            _observers.ForEach(observer => observer.Update(message));
        }
    }
}