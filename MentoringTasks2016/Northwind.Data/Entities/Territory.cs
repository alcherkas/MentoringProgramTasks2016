using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Northwind.Data.Entities
{
    [Table(Schema = "dbo", Name = "Territories")]
    public class Territory
    {
        [PrimaryKey, NotNull]
        public string TerritoryID { get; set; } // nvarchar(20)
        [Column, NotNull]
        public string TerritoryDescription { get; set; } // nchar(50)
        [Column, NotNull]
        public int RegionID { get; set; } // int

        #region Associations

        /// <summary>
        /// FK_Territories_Region
        /// </summary>
        [Association(ThisKey = "RegionID", OtherKey = "RegionID", CanBeNull = false, KeyName = "FK_Territories_Region", BackReferenceName = "Territories")]
        public Region Region { get; set; }

        /// <summary>
        /// FK_EmployeeTerritories_Territories_BackReference
        /// </summary>
        [Association(ThisKey = "TerritoryID", OtherKey = "TerritoryID", CanBeNull = true, IsBackReference = true)]
        public IEnumerable<EmployeeTerritory> EmployeeTerritories { get; set; }

        #endregion
    }
}