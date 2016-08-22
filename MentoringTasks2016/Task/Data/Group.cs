using System.Collections.Generic;

namespace Task.Data
{
    public class Group<TKey, TEntity>
    {
        public TKey Key { get; set; }
        public IEnumerable<TEntity> Entities { get; set; }
    }

    public class ProductCategoryGroup : Group<string, ProductUnitGroup> { }

    public class ProductUnitGroup:Group<int, Product> { }
}
