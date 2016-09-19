using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data.Entities
{
    [Table("CustomerCustomerDemo")]
    public sealed class CustomerCustomerDemo
    {
        [Column("CustomerID")]
        public string CustomerId { get; set; }

        [Column("CustomerTypeID")]
        public string CustomerTypeId { get; set; }
    }
}
