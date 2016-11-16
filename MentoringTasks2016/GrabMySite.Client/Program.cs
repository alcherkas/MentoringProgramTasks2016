using System;

namespace GrabMySite.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var address = "http://www.w3schools.com/";
            var path = @"D:\1";
            var isTrasingEnabled = true;

            IProgress<string> progress = isTrasingEnabled ? (IProgress<string>) new ConsoleProgressReporter<string>() : new  NullProgressReporter<string>();

            var grabber = new Grabber(address, path, GrabbingWidth.Unlimited, progress) {Depth = 1};
            grabber.Grab();
        }

        public sealed class ConsoleProgressReporter<T> : IProgress<T>
        {
            public void Report(T value) => Console.WriteLine(value);
        }

        public sealed class NullProgressReporter<T> : IProgress<T>
        {
            public void Report(T value) { }
        }
    }
}
