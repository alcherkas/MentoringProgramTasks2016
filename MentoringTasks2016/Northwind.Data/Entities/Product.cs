using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data.Entities
{
    [Table("Products")]
    public sealed class Product
    {
        [Column("ProductID")]
        public int Id { get; set; }

        [Column("ProductName")]
        public string ProductName { get; set; }

        [Column("SupplierID")]
        public int? SupplierId { get; set; }

        [Column("CategoryID")]
        public int? CategoryId { get; set; }

        [Column("Discontinued")]
        public bool Discontinued { get; set; }
    }
}
