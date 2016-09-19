using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data.Entities
{
    [Table("Customers")]
    public sealed class Customer
    {
        [Key]
        [Column("CustomerID")]
        public string Id { get; set; }

        [Required]
        [Column("CompanyName")]
        public string Name { get; set; }

        [Column("ContactName")]
        public string ContactName { get; set; }

        [Column("ContactTitle")]
        public string ContactTitle { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("Region")]
        public string City { get; set; }

        [Column("PostalCode")]
        public string PostalCode { get; set; }

        [Column("Country")]
        public string Country { get; set; }

        [Column("Phone")]
        public string Phone { get; set; }

        [Column("Fax")]
        public string Fax { get; set; }
    }
}
