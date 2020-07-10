using System;
using Xunit;

namespace Example.Observer.Tests
{
    public class ObserverTests
    {
        [Fact(DisplayName = "Passing some notifications")]
        public void Test1()
        {
            var subject = new Subject();
            var firstObserver = new FirstObserver();
            var secondObserver = new SecondObserver();
            firstObserver.Subscribe(subject);
            secondObserver.Subscribe(subject);

            subject.Notify("Something happened");
        }
    }
}
