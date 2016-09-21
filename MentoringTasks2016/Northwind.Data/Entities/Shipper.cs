using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Northwind.Data.Entities
{
    [Table(Schema = "dbo", Name = "Shippers")]
    public class Shipper
    {
        [PrimaryKey, Identity]
        public int ShipperID { get; set; } // int
        [Column, NotNull]
        public string CompanyName { get; set; } // nvarchar(40)
        [Column, Nullable]
        public string Phone { get; set; } // nvarchar(24)

        #region Associations

        /// <summary>
        /// FK_Orders_Shippers_BackReference
        /// </summary>
        [Association(ThisKey = "ShipperID", OtherKey = "ShipVia", CanBeNull = true, IsBackReference = true)]
        public IEnumerable<Order> Orders { get; set; }

        #endregion
    }
}