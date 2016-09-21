using LinqToDB.Mapping;

namespace Northwind.Data.Entities
{
    [Table(Schema = "dbo", Name = "Product Sales for 1997")]
    public class ProductSalesFor1997
    {
        [Column, NotNull]
        public string CategoryName { get; set; } // nvarchar(15)
        [Column, NotNull]
        public string ProductName { get; set; } // nvarchar(40)
        [Column, Nullable]
        public decimal? ProductSales { get; set; } // money
    }
}