using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data.Entities
{
    [Table("CustomerDemographics")]
    public sealed class CustomerDemograhics
    {
        [Key]
        [Column("CustomerTyeID")]
        public int Id { get; set; }

        [Column("CustomerDesc")]
        public string Description { get; set; }
    }
}
