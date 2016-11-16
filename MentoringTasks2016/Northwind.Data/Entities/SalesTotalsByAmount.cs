using System;
using LinqToDB.Mapping;

namespace Northwind.Data.Entities
{
    [Table(Schema = "dbo", Name = "Sales Totals by Amount")]
    public class SalesTotalsByAmount
    {
        [Column, Nullable]
        public decimal? SaleAmount { get; set; } // money
        [Column, NotNull]
        public int OrderID { get; set; } // int
        [Column, NotNull]
        public string CompanyName { get; set; } // nvarchar(40)
        [Column, Nullable]
        public DateTime? ShippedDate { get; set; } // datetime
    }
}