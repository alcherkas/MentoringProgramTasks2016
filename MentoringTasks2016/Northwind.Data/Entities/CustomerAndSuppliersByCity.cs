using LinqToDB.Mapping;

namespace Northwind.Data.Entities
{
    [Table(Schema = "dbo", Name = "Customer and Suppliers by City")]
    public class CustomerAndSuppliersByCity
    {
        [Column, Nullable]
        public string City { get; set; } // nvarchar(15)
        [Column, NotNull]
        public string CompanyName { get; set; } // nvarchar(40)
        [Column, Nullable]
        public string ContactName { get; set; } // nvarchar(30)
        [Column, NotNull]
        public string Relationship { get; set; } // varchar(9)
    }
}