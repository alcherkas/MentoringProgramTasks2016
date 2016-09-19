using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data.Entities
{
    [Table("Order Details")]
    public sealed class OrderDetail
    {
        [Column("OrderID")]
        public int OrderId { get; set; }

        [Column("ProductID")]
        public int ProductId { get; set; }

        [Column("UnitPrice")]
        public decimal UnitPrice { get; set; }

        // ToDo: smallint
        [Column("Quantity")]
        public short Quantity { get; set; }

        [Column("Discount")]
        public double Discount { get; set; }
    }
}
