using System.Collections.Generic;

namespace CachingSolutionsSamples
{
    public interface ICache<T>
        where T : class
    {
        IEnumerable<T> Get(string forUser);
        void Set(string forUser, IEnumerable<T> categories);
    }
}
