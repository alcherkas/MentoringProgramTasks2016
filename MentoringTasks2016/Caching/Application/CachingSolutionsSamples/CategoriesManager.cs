using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace CachingSolutionsSamples
{
    public class CategoriesManager<T>
        where T: class 
    {
        private ICache<T> cache;

        public CategoriesManager(ICache<T> cache)
        {
            this.cache = cache;
        }

        public string CurrentUser => Thread.CurrentPrincipal.Identity.Name;

        public IEnumerable<T> GetCategories()
        {
            Console.WriteLine("Get " + typeof(T));

            var user = CurrentUser;
            var list = cache.Get(user);

            if (list == null)
            {
                Console.WriteLine("From DB");

                list = PutEntitiesToCache(user);
            }

            return list;
        }

        private IEnumerable<T> PutEntitiesToCache(string user)
        {
            List<T> list;
            using (var dbContext = new Northwind())
            {
                dbContext.Configuration.LazyLoadingEnabled = false;
                dbContext.Configuration.ProxyCreationEnabled = false;
                list = dbContext.Set<T>().ToList();
                cache.Set(user, list);
            }
            return list;
        }
    }
}
