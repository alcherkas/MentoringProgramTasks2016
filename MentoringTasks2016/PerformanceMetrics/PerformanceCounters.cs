using System.Diagnostics;
using PerformanceCounterHelper;

namespace PerformanceMetrics
{
    [PerformanceCounterCategory("Music Store Counters", PerformanceCounterCategoryType.SingleInstance, "Information about pages visiting.")]
    internal enum PerformanceCounters
    {
        [PerformanceCounter("LoginCount", "Successfull login count", PerformanceCounterType.NumberOfItems64)]
        LoginCounter,
        [PerformanceCounter("LogoutCount", "Successfull logout count", PerformanceCounterType.NumberOfItems64)]
        LogoutCounter,
        [PerformanceCounter("HomeCount", "Successfull home page loadings count", PerformanceCounterType.NumberOfItems64)]
        HomeCounter
    }
}
