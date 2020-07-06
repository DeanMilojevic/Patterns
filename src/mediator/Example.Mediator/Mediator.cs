using System.Linq;
using System.Collections.Generic;

namespace Example.Mediator
{
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
}