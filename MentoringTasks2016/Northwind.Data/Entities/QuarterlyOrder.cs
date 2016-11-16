using LinqToDB.Mapping;

namespace Northwind.Data.Entities
{
    [Table(Schema = "dbo", Name = "Quarterly Orders")]
    public class QuarterlyOrder
    {
        [Column, Nullable]
        public string CustomerID { get; set; } // nchar(5)
        [Column, Nullable]
        public string CompanyName { get; set; } // nvarchar(40)
        [Column, Nullable]
        public string City { get; set; } // nvarchar(15)
        [Column, Nullable]
        public string Country { get; set; } // nvarchar(15)
    }
}