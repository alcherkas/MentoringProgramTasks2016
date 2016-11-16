using LinqToDB.Mapping;

namespace Northwind.Data.Entities
{
    [Table(Schema = "dbo", Name = "Products by Category")]
    public class ProductsByCategory
    {
        [Column, NotNull]
        public string CategoryName { get; set; } // nvarchar(15)
        [Column, NotNull]
        public string ProductName { get; set; } // nvarchar(40)
        [Column, Nullable]
        public string QuantityPerUnit { get; set; } // nvarchar(20)
        [Column, Nullable]
        public short? UnitsInStock { get; set; } // smallint
        [Column, NotNull]
        public bool Discontinued { get; set; } // bit
    }
}