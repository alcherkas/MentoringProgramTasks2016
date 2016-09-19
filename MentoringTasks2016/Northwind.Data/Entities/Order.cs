using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data.Entities
{
    [Table("Orders")]
    public sealed class Order
    {
        [Column("OrderID")]
        public int Id { get; set; }

        [Column("CustomerID")]
        public string CustomerId { get; set; }

        [Column("EmployeeID")]
        public int? EmployeeId { get; set; }

        [Column("OrderDate")]
        public DateTime? OrderDate { get; set; }

        [Column("ShippedDate")]
        public DateTime? ShippedDate { get; set; }

        [Column("ShipVia")]
        public int? ShipVia { get; set; }
    }
}
