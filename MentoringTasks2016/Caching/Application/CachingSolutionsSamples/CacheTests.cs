using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading;
using NorthwindLibrary;

namespace CachingSolutionsSamples
{
    [TestClass]
    public class CacheTests
    {
        [TestMethod]
        public void MemoryCache()
        {
            var categoryManager = new CategoriesManager<Category>(new MemoryCache<Category>());

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(categoryManager.GetCategories().Count());
                Thread.Sleep(400);
            }
        }

        [TestMethod]
        public void RedisCache()
        {
            var categoryManager = new CategoriesManager<Category>(new RedisCache<Category>("localhost"));

            for (var i = 0; i < 10; i++)
            {
                Console.WriteLine(categoryManager.GetCategories().Count());
                Thread.Sleep(100);
            }
        }

        [TestMethod]
        public void Fibonacci()
        {
            var numbers = new FibonacciNumbers();
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(numbers.Calculate(10));
            }
        }
    }
}
