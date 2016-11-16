using LinqToDB.Mapping;

namespace Northwind.Data.Entities
{
    [Table(Schema = "dbo", Name = "Current Product List")]
    public class CurrentProductList
    {
        [Identity]
        public int ProductID { get; set; } // int
        [Column, NotNull]
        public string ProductName { get; set; } // nvarchar(40)
    }
}