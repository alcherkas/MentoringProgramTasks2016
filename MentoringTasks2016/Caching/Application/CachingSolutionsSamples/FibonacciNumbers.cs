using System;
using System.Runtime.Caching;

namespace CachingSolutionsSamples
{
    public class FibonacciNumbers
    {
        private readonly MemoryCache _cache = new MemoryCache();

        public int Calculate(int i)
        {
            var fibonacciResult = _cache.Get(i);
            Console.WriteLine("Calculating");
            if (fibonacciResult.HasValue)
            {
                Console.WriteLine("cached");
                return fibonacciResult.Value;
            }

            Console.WriteLine();
            var result = Fibonacci(i);
            _cache.Set(i, result);
            return result;
        }

        private int Fibonacci(int n)
        {
            int a = 1;
            int b = 1;
            int fibonacci = 0;
            for (int i = 0; i < n; i++)
            {
                fibonacci = a + b;
                a = b;
                b = fibonacci;
            }

            return fibonacci;
        }

        internal class MemoryCache 
        {
            ObjectCache cache = System.Runtime.Caching.MemoryCache.Default;

            public int? Get(int key)
            {
                return (int?)cache.Get(key.ToString());
            }

            public void Set(int key, int value)
            {
                cache.Set(key.ToString(), value, DateTimeOffset.MaxValue);
            }
        }
    }
}