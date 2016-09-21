using LinqToDB.Mapping;

namespace Northwind.Data.Entities
{
    [Table(Schema = "dbo", Name = "Order Details Extended")]
    public class OrderDetailsExtended
    {
        [Column, NotNull]
        public int OrderID { get; set; } // int
        [Column, NotNull]
        public int ProductID { get; set; } // int
        [Column, NotNull]
        public string ProductName { get; set; } // nvarchar(40)
        [Column, NotNull]
        public decimal UnitPrice { get; set; } // money
        [Column, NotNull]
        public short Quantity { get; set; } // smallint
        [Column, NotNull]
        public float Discount { get; set; } // real
        [Column, Nullable]
        public decimal? ExtendedPrice { get; set; } // money
    }
}