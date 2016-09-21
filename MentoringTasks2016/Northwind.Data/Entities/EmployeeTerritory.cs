using LinqToDB.Mapping;

namespace Northwind.Data.Entities
{
    [Table(Schema = "dbo", Name = "EmployeeTerritories")]
    public class EmployeeTerritory
    {
        [PrimaryKey(1), NotNull]
        public int EmployeeID { get; set; } // int
        [PrimaryKey(2), NotNull]
        public string TerritoryID { get; set; } // nvarchar(20)

        #region Associations

        /// <summary>
        /// FK_EmployeeTerritories_Employees
        /// </summary>
        [Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID", CanBeNull = false, KeyName = "FK_EmployeeTerritories_Employees", BackReferenceName = "EmployeeTerritories")]
        public Employee Employee { get; set; }

        /// <summary>
        /// FK_EmployeeTerritories_Territories
        /// </summary>
        [Association(ThisKey = "TerritoryID", OtherKey = "TerritoryID", CanBeNull = false, KeyName = "FK_EmployeeTerritories_Territories", BackReferenceName = "EmployeeTerritories")]
        public Territory Territory { get; set; }

        #endregion
    }
}