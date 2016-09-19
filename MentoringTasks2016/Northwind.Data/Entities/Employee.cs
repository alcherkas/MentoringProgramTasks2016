using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Northwind.Data.Entities
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        [Column("EmployeeID")]
        public int Id { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("Title")]
        public string Title { get; set; }

        [Column("TitleOfCourtesy")]
        public string TitleOfCourtesy { get; set; }

        [Column("BirthDate")]
        public DateTime BirthDate { get; set; }

        [Column("HireDate")]
        public DateTime HireDate { get; set; }
    }
}
