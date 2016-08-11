using System;

namespace BCL2
{
    internal static class SortAlgorithms
    {
        public static void SelectionSort(string[] array)
        {
            for (int i = 0; i < array.Length - 1; i++)
            {
                var minimum = i;
                for (int j = i + 1; j < array.Length; j++)
                {
                    if (string.Compare(array[minimum], array[j], StringComparison.Ordinal) > 0) minimum = j;

                    var temp = array[i];
                    array[i] = array[minimum];
                    array[minimum] = temp;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var array = new[] {"a", "cccccc", "cc", "ca", "c0", "dds", "yy", "bb"};
            SortAlgorithms.SelectionSort(array);
            foreach (var item in array)
                Console.WriteLine(item);

            Console.ReadLine();
        }
    }
}
