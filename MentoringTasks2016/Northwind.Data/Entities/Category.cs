using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data.Entities
{
    [Table("Categories")]
    public sealed class Category
    {
        [Key]
        [Column("CategoryID")]
        public int Id { get; set; }

        [MaxLength(15)]
        [Column("CategoryName")]
        public string Name { get; set; }

        [Column("Description")]
        public string Description { get; set; }

        [Column("Picture")]
        public byte[] Picture { get; set; }
    }
}
