using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindLibrary;
using System.Runtime.Caching;

namespace CachingSolutionsSamples
{
	internal class CategoriesMemoryCache : ICategoriesCache
	{
		ObjectCache cache = MemoryCache.Default;
		string prefix  = "Cache_Categories";

		public IEnumerable<Category> Get(string forUser)
		{
			return (IEnumerable<Category>) cache.Get(prefix + forUser);
		}

		public void Set(string forUser, IEnumerable<Category> categories)
		{
			cache.Set(prefix + forUser, categories, ObjectCache.InfiniteAbsoluteExpiration);
		}
	}
}
