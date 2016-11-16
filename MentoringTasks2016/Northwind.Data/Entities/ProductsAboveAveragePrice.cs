using LinqToDB.Mapping;

namespace Northwind.Data.Entities
{
    [Table(Schema = "dbo", Name = "Products Above Average Price")]
    public class ProductsAboveAveragePrice
    {
        [Column, NotNull]
        public string ProductName { get; set; } // nvarchar(40)
        [Column, Nullable]
        public decimal? UnitPrice { get; set; } // money
    }
}