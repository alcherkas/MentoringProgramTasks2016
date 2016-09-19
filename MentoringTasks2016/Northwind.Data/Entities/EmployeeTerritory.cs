using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data.Entities
{
    [Table("EmployeeTerritories")]
    public sealed class EmployeeTerritory
    {
        [Column("EmployeeID")]
        public int EmpoyeeId { get; set; }

        [Column("TerritoryID")]
        public string TerritoryId { get; set; }
    }
}
