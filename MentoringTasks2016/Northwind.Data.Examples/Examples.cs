using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataModels;
using LinqToDB;

namespace Northwind.Data.Examples
{
    [TestClass]
    public class Examples
    {
        [TestMethod]
        public void GetProductsWithCategoryAndSupplier()
        {
            using (var db = new NorthwindDB())
            {
                var products = db.Products
                    .Select(p => new
                                    {
                                        p.ProductName,
                                        p.Supplier.CompanyName,
                                        p.Category.CategoryName
                                    })
                    .ToList();

                // db.OrderDetails.Delete()
                // db.BulkCopy()
                foreach (var product in products)
                {
                    Debug.WriteLine(product);
                }
            }
        }

        [TestMethod]
        public void GetEmployeeWithRegion()
        {
            using (var db = new NorthwindDB())
            {
                var employeeTerritories =
                    db.EmployeeTerritories.Select(
                        et => new
                        {
                            et.Employee.FirstName,
                            et.Employee.LastName,
                            et.Territory.TerritoryDescription
                        })
                        .ToList();

                foreach (var employeeTerritory in employeeTerritories)
                {
                    Debug.WriteLine(employeeTerritory);
                }
            }
        }

        [TestMethod]
        public void GetEmployeesCountForEachRegion()
        {
            using (var db = new NorthwindDB())
            {
                var regionStatistics =
                    db.EmployeeTerritories.Select(
                        et => new {et.Territory.Region.RegionDescription, et.Employee.EmployeeID})
                        .GroupBy(x => x.RegionDescription)
                        .Select(x => new {Region = x.Key, EmployeeCount = x.Count()})
                        .ToList();

                foreach (var regionStatistic in regionStatistics)
                {
                    Debug.WriteLine(regionStatistic);
                }
            }
        }

        [TestMethod]
        public void GetEmployeeShipVia()
        {
            using (var db = new NorthwindDB())
            {
                var orderShip = db.Orders.Select(o => new {o.ShipVia, o.Employee.FirstName, o.Employee.LastName})
                    .GroupBy(os => new {os.FirstName, os.LastName})
                    .Select(
                        x =>
                            new
                            {
                                x.Key.FirstName,
                                x.Key.LastName,
                                EmployeeShipViaList = string.Join(",", x.Select(sh => sh.ShipVia.ToString()).ToArray())
                            })
                    .ToList();

                foreach (var order in orderShip)
                {
                    Debug.WriteLine(order);
                }
            }
        }

        [TestMethod]
        public void InsertNewEmployeeWithTerritory()
        {
            using (var db = new NorthwindDB())
            {
                var employee = new Employee
                {
                    FirstName = "Huan",
                    LastName = "Carlos",
                    Address = "Chihuahua",
                    BirthDate = DateTime.Now,
                    City = "BuenosAeros",
                    Country = "Brazil",
                    Photo = new byte[0],
                    Extension = string.Empty,
                    HireDate = DateTime.Now,
                    HomePhone = string.Empty,
                    Notes = string.Empty,
                    PhotoPath = string.Empty,
                    Region = string.Empty,
                    PostalCode = string.Empty,
                    Title = string.Empty,
                    TitleOfCourtesy = string.Empty
                };

                var employeeId = db.Insert(employee);
                var territory = db.Territories.First();
                var employeeTerritory = new EmployeeTerritory
                {
                    EmployeeID = employeeId,
                    TerritoryID = territory.TerritoryID
                };

                db.Insert(employeeTerritory);

                var newEmployee = db.EmployeeTerritories.First(x => x.Employee.FirstName == "Huan");
                Debug.WriteLine(newEmployee);
            }
        }

        [TestMethod]
        public void MoveToAnotherCategory()
        {
            using (var db = new NorthwindDB())
            {
                var products = db.Products.Where(p => p.CategoryID == 3).ToList();
                var newCategoryId = db.Categories.First().CategoryID;

                foreach (var product in products)
                {
                    product.CategoryID = newCategoryId;
                    db.Update(product);
                }

                var isAnyWithThirdCategory = db.Products.Any(x => x.CategoryID == 3);
                Assert.IsFalse(isAnyWithThirdCategory);
            }
        }

        [TestMethod]
        public void InsertNewProducts()
        {
            var suppliers = new List<Supplier>(3)
            {
                new Supplier { CompanyName = "Huan", Address = "", City = "", ContactName = "", ContactTitle = "",  Country = "", Fax = "", HomePage = "", Phone = "", PostalCode = "", Region = "" },
                new Supplier { CompanyName = "Carlos", Address = "", City = "", ContactName = "", ContactTitle = "",  Country = "", Fax = "", HomePage = "", Phone = "", PostalCode = "", Region = "" },
                new Supplier { CompanyName = "Maria", Address = "", City = "", ContactName = "", ContactTitle = "",  Country = "", Fax = "", HomePage = "", Phone = "", PostalCode = "", Region = "" },
            };

            var products = new List<Product>(3)
            {
                new Product {CategoryID = 1, Discontinued = true, ProductName = "qq", QuantityPerUnit = "122"},
                new Product {CategoryID = 1, Discontinued = true, ProductName = "qwe", QuantityPerUnit = "122"},
                new Product {CategoryID = 1, Discontinued = true, ProductName = "Product", QuantityPerUnit = "122"},
            };

            using (var db = new NorthwindDB())
            {
                var counter = 0;
                foreach (var supplier in suppliers)
                {
                    var id = db.Insert(supplier);
                    products[counter].SupplierID = id;

                    var product = db.Products.FirstOrDefault(x => x.ProductName == products[counter].ProductName);
                    if (product == null)
                        db.Insert(products[counter]);
                    else
                    {
                        product.SupplierID = id;
                        db.Update(product);
                    }

                    counter++;
                }

            }
        }

        [TestMethod]
        public void ChangeProductForNonShippedOrders()
        {
            using (var db = new NorthwindDB())
            {
                db.BeginTransaction();
                var orderDetails = db.OrderDetails.Where(x => x.OrderDetailsOrder.ShippedDate == null).ToList();
                var product = db.Products.First();
                foreach (var orderDetail in orderDetails)
                {
                    orderDetail.ProductID = product.ProductID;
                    db.Update(orderDetail);
                }
                db.CommitTransaction();
                var ordersWithNewProducts = db.OrderDetails.Where(x => x.OrderDetailsOrder.ShippedDate == null).ToList();
                Assert.IsTrue(ordersWithNewProducts.All(x => x.ProductID == product.ProductID));
            }
        }
    }
}
