using System;
using System.Collections.Generic;
using LinqToDB.Mapping;

namespace Northwind.Data.Entities
{
    [Table(Schema = "dbo", Name = "Employees")]
    public class Employee
    {
        [PrimaryKey, Identity]
        public int EmployeeID { get; set; } // int
        [Column, NotNull]
        public string LastName { get; set; } // nvarchar(20)
        [Column, NotNull]
        public string FirstName { get; set; } // nvarchar(10)
        [Column, Nullable]
        public string Title { get; set; } // nvarchar(30)
        [Column, Nullable]
        public string TitleOfCourtesy { get; set; } // nvarchar(25)
        [Column, Nullable]
        public DateTime? BirthDate { get; set; } // datetime
        [Column, Nullable]
        public DateTime? HireDate { get; set; } // datetime
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
        [Column, Nullable]
        public string HomePhone { get; set; } // nvarchar(24)
        [Column, Nullable]
        public string Extension { get; set; } // nvarchar(4)
        [Column, Nullable]
        public byte[] Photo { get; set; } // image
        [Column, Nullable]
        public string Notes { get; set; } // ntext
        [Column, Nullable]
        public int? ReportsTo { get; set; } // int
        [Column, Nullable]
        public string PhotoPath { get; set; } // nvarchar(255)

        #region Associations

        /// <summary>
        /// FK_Employees_Employees
        /// </summary>
        [Association(ThisKey = "ReportsTo", OtherKey = "EmployeeID", CanBeNull = true, KeyName = "FK_Employees_Employees", BackReferenceName = "FK_Employees_Employees_BackReferences")]
        public Employee FK_Employees_Employee { get; set; }

        /// <summary>
        /// FK_Employees_Employees_BackReference
        /// </summary>
        [Association(ThisKey = "EmployeeID", OtherKey = "ReportsTo", CanBeNull = true, IsBackReference = true)]
        public IEnumerable<Employee> FK_Employees_Employees_BackReferences { get; set; }

        /// <summary>
        /// FK_Orders_Employees_BackReference
        /// </summary>
        [Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID", CanBeNull = true, IsBackReference = true)]
        public IEnumerable<Order> Orders { get; set; }

        /// <summary>
        /// FK_EmployeeTerritories_Employees_BackReference
        /// </summary>
        [Association(ThisKey = "EmployeeID", OtherKey = "EmployeeID", CanBeNull = true, IsBackReference = true)]
        public IEnumerable<EmployeeTerritory> EmployeeTerritories { get; set; }

        #endregion
    }
}