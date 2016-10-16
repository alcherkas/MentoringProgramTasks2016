using NorthwindLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CachingSolutionsSamples
{
	public interface ICategoriesCache
	{
		IEnumerable<Category> Get(string forUser);
		void Set(string forUser, IEnumerable<Category> categories);
	}
}
