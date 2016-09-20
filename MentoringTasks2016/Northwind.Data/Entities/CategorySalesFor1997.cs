using LinqToDB.Mapping;

namespace DataModels
{
    [Table(Schema = "dbo", Name = "Category Sales for 1997")]
    public partial class CategorySalesFor1997
    {
        [Column, NotNull]
        public string CategoryName { get; set; } // nvarchar(15)
        [Column, Nullable]
        public decimal? CategorySales { get; set; } // money
    }
}