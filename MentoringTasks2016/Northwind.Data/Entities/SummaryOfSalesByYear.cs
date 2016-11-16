using System;
using LinqToDB.Mapping;

namespace Northwind.Data.Entities
{
    [Table(Schema = "dbo", Name = "Summary of Sales by Year")]
    public class SummaryOfSalesByYear
    {
        [Column, Nullable]
        public DateTime? ShippedDate { get; set; } // datetime
        [Column, NotNull]
        public int OrderID { get; set; } // int
        [Column, Nullable]
        public decimal? Subtotal { get; set; } // money
    }
}