using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data.Entities
{
    [Table("Suppliers")]
    public sealed class Supplier
    {
        [Column("SupplierID")]
        public int Id { get; set; }

        [Column("CompanyName")]
        public string CompanyName { get; set; }
    }
}
