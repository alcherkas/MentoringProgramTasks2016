using LinqToDB.Mapping;

namespace Northwind.Data.Entities
{
    [Table(Schema = "dbo", Name = "Sales by Category")]
    public class SalesByCategory
    {
        [Column, NotNull]
        public int CategoryID { get; set; } // int
        [Column, NotNull]
        public string CategoryName { get; set; } // nvarchar(15)
        [Column, NotNull]
        public string ProductName { get; set; } // nvarchar(40)
        [Column, Nullable]
        public decimal? ProductSales { get; set; } // money
    }
}