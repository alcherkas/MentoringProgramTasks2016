using System;

namespace Reflection2
{
    class Program
    {
        static void Main(string[] args)
        {
            var eventsStore = new Events();
            var eventInfo = eventsStore.GetType().GetEvent("OnClick");
            var program = new Program();
            var methodInfo = program.GetType().GetMethod(nameof(OnClickHandler), new Type[0]);
            var onClickEventHandler = Delegate.CreateDelegate(eventInfo.EventHandlerType, methodInfo);
            eventInfo.AddEventHandler(eventsStore, onClickEventHandler);

            eventsStore.PrintResult();
            Console.ReadLine();
        }
        public static void OnClickHandler()
        {
            Console.WriteLine("On click handler.");
        }

        public class Events
        {
            public event Action OnClick;

            public void PrintResult()
            {
                Console.WriteLine("Press enter");
                Console.ReadLine();

                OnClick?.Invoke();
            }
        }
    }
}
