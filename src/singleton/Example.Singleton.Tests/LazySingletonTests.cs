using System.Threading.Tasks;
using System.Linq;
using Xunit;

namespace Example.Singleton.Tests
{
  public class LazySingletonTests
    {
        [Fact(DisplayName = "Running in the parallel")]
        public async Task Test1()
        {
            var tasks = new int[] { 1, 2, 3, 4, 5 }
                        .Select(i => Task.Run(() => 
                        {
                            LazySingleton.Instance.Print(i);
                        }));

            await Task.WhenAll(tasks);
        }
    }
}
