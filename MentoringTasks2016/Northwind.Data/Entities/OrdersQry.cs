using System;
using LinqToDB.Mapping;

namespace Northwind.Data.Entities
{
    [Table(Schema = "dbo", Name = "Orders Qry")]
    public class OrdersQry
    {
        [Column, NotNull]
        public int OrderID { get; set; } // int
        [Column, Nullable]
        public string CustomerID { get; set; } // nchar(5)
        [Column, Nullable]
        public int? EmployeeID { get; set; } // int
        [Column, Nullable]
        public DateTime? OrderDate { get; set; } // datetime
        [Column, Nullable]
        public DateTime? RequiredDate { get; set; } // datetime
        [Column, Nullable]
        public DateTime? ShippedDate { get; set; } // datetime
        [Column, Nullable]
        public int? ShipVia { get; set; } // int
        [Column, Nullable]
        public decimal? Freight { get; set; } // money
        [Column, Nullable]
        public string ShipName { get; set; } // nvarchar(40)
        [Column, Nullable]
        public string ShipAddress { get; set; } // nvarchar(60)
        [Column, Nullable]
        public string ShipCity { get; set; } // nvarchar(15)
        [Column, Nullable]
        public string ShipRegion { get; set; } // nvarchar(15)
        [Column, Nullable]
        public string ShipPostalCode { get; set; } // nvarchar(10)
        [Column, Nullable]
        public string ShipCountry { get; set; } // nvarchar(15)
        [Column, NotNull]
        public string CompanyName { get; set; } // nvarchar(40)
        [Column, Nullable]
        public string Address { get; set; } // nvarchar(60)
        [Column, Nullable]
        public string City { get; set; } // nvarchar(15)
        [Column, Nullable]
        public string Region { get; set; } // nvarchar(15)
        [Column, Nullable]
        public string PostalCode { get; set; } // nvarchar(10)
        [Column, Nullable]
        public string Country { get; set; } // nvarchar(15)
    }
}