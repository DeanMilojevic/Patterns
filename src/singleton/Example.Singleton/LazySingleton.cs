using System;

namespace Example.Singleton
{
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
}