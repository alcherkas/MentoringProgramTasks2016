using System;
using System.Text;

namespace BCL3
{
    internal static class Concatenator
    {
        public static string SpecificConcatenate(string[] strings)
        {
            var symbolsCount = 0;
            for (int i = 0; i < strings.Length; i += 2)
                symbolsCount += strings[i].Length;

            var builder = new StringBuilder(symbolsCount);
            for (int i = 0; i < strings.Length; i += 2)
                builder.AppendLine(strings[i]);

            return builder.ToString();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var strings = new string[short.MaxValue];
            for (int i = 0; i < strings.Length; i++)
            {
                strings[i] = i.ToString();
            }

            var concatenatedString = Concatenator.SpecificConcatenate(strings);
            Console.WriteLine(concatenatedString);
            Console.ReadLine();
        }
    }
}
