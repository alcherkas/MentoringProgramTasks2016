using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Northwind.Data.Entities
{
    [Table(Schema = "dbo", Name = "Region")]
    public class Region
    {
        [PrimaryKey, NotNull]
        public int RegionID { get; set; } // int
        [Column, NotNull]
        public string RegionDescription { get; set; } // nchar(50)

        #region Associations

        /// <summary>
        /// FK_Territories_Region_BackReference
        /// </summary>
        [Association(ThisKey = "RegionID", OtherKey = "RegionID", CanBeNull = true, IsBackReference = true)]
        public IEnumerable<Territory> Territories { get; set; }

        #endregion
    }
}