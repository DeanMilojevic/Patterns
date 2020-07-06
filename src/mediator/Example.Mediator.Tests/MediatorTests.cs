using System;
using Xunit;

namespace Example.Mediator.Tests
{
    public class MediatorTests
    {
        [Fact(DisplayName = "Mediator handles the messaging")]
        public void Test1()
        {
            var mediator = new Mediator();
            var obj1 = new MyObject("obj1");
            var obj2 = new MyObject("obj2");

            mediator.Register(obj1);
            mediator.Register(obj2);

            obj1.Send("Hello");
            obj2.Send("Hello back");
        }
    }
}
