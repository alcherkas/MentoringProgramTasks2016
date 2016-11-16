using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace CachingSolutionsSamples
{
    internal class MemoryCache<T> : ICache<T>
        where T : class
    {
        ObjectCache cache = System.Runtime.Caching.MemoryCache.Default;
        string prefix = "Cache_"+typeof(T);

        public IEnumerable<T> Get(string forUser)
        {
            return (IEnumerable<T>) cache.Get(prefix + forUser);
        }

        public void Set(string forUser, IEnumerable<T> categories)
        {
            cache.Set(prefix + forUser, categories, DateTimeOffset.Now.AddSeconds(1));
        }
    }
}
