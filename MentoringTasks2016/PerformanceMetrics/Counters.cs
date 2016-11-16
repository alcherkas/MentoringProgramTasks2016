using System;
using PerformanceCounterHelper;

namespace PerformanceMetrics
{
    public sealed class Counters
    {
        private static readonly CounterHelper<PerformanceCounters> counterHelper =
            PerformanceHelper.CreateCounterHelper<PerformanceCounters>();

        public void IncrementLogin() => IncrementCounter(PerformanceCounters.LoginCounter);
        public void IncrementLogout() => IncrementCounter(PerformanceCounters.LogoutCounter);
        public void IncrementHome() => IncrementCounter(PerformanceCounters.HomeCounter);

        //static Counters()
        //{
        //    PerformanceHelper.Install(typeof(PerformanceCounters));
        //}

        private void IncrementCounter(PerformanceCounters counter)
        {
            counterHelper.Increment(counter);
        }
    }
}
