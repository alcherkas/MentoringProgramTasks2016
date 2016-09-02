using System;
using System.Collections.Generic;

namespace Reflection1
{
    class Program
    {
        static void Main()
        {
            var list = CreateList<object>();
            AddItem(list, new object());
            AddItem(list, "string");
            AddItem(list, 10);
            AddItem(list, new object());
            AddItem(list, "string");

            foreach (var item in list)
            {
                Console.WriteLine(item);
            }

            Console.ReadLine();
        }

        static List<T> CreateList<T>()
        {
            return (List<T>)Activator.CreateInstance(typeof(List<T>));
        }

        static void AddItem<T>(List<T> items, T item)
        {
            var mi = items.GetType().GetMethod("Add", new[] { typeof(T) });
            mi.Invoke(items, new object[] { item });
        }
    }
}
