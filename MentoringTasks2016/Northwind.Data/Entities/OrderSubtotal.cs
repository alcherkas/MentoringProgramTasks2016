using LinqToDB.Mapping;

namespace Northwind.Data.Entities
{
    [Table(Schema = "dbo", Name = "Order Subtotals")]
    public class OrderSubtotal
    {
        [Column, NotNull]
        public int OrderID { get; set; } // int
        [Column, Nullable]
        public decimal? Subtotal { get; set; } // money
    }
}