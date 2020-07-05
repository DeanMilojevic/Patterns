using System;
using System.Collections.Generic;
using Xunit;

namespace Example.NullObject.Tests
{
    public class NullObjectTests
    {
        [Fact(DisplayName = "Using nulls")]
        public void Test1()
        {
            var data = ProvideNullData();

            foreach (var obj in data)
            {
                var msg = obj == null
                ? $"-1 - N/A"
                : $"{obj.Id} - {obj.Value}";
                
                Console.WriteLine(msg);
            }
        }

        [Fact(DisplayName = "Using null-objects")]
        public void Test2()
        {
            var data = ProvideNullObjectData();

            foreach (var obj in data)
            {
                Console.WriteLine($"{obj.Id} - {obj.Value}");
            }
        }

        private IList<IMyObject> ProvideNullData()
        {
            var list = new List<IMyObject>(100);

            for (var i = 0; i <= 100; i++)
            {
                if (i % 10 == 0)
                {
                    list.Add(null);
                }

                list.Add(new MyObject(i, $"{i}"));
            }

            return list;
        }

        private IList<IMyObject> ProvideNullObjectData()
        {
            var list = new List<IMyObject>(100);

            for (var i = 0; i <= 100; i++)
            {
                if (i % 10 == 0)
                {
                    list.Add(new MyNullObject());
                }

                list.Add(new MyObject(i, $"{i}"));
            }

            return list;
        }
    }
}
