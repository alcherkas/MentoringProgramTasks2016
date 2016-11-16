using LinqToDB.Mapping;

namespace Northwind.Data.Entities
{
    [Table(Schema = "dbo", Name = "Alphabetical list of products")]
    public class AlphabeticalListOfProduct
    {
        [Column, NotNull]
        public int ProductID { get; set; } // int
        [Column, NotNull]
        public string ProductName { get; set; } // nvarchar(40)
        [Column, Nullable]
        public int? SupplierID { get; set; } // int
        [Column, Nullable]
        public int? CategoryID { get; set; } // int
        [Column, Nullable]
        public string QuantityPerUnit { get; set; } // nvarchar(20)
        [Column, Nullable]
        public decimal? UnitPrice { get; set; } // money
        [Column, Nullable]
        public short? UnitsInStock { get; set; } // smallint
        [Column, Nullable]
        public short? UnitsOnOrder { get; set; } // smallint
        [Column, Nullable]
        public short? ReorderLevel { get; set; } // smallint
        [Column, NotNull]
        public bool Discontinued { get; set; } // bit
        [Column, NotNull]
        public string CategoryName { get; set; } // nvarchar(15)
    }
}