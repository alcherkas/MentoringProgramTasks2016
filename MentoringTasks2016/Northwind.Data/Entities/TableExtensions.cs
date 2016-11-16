using System.Linq;
using LinqToDB;

namespace Northwind.Data.Entities
{
    public static class TableExtensions
    {
        public static Category Find(this ITable<Category> table, int CategoryID)
        {
            return table.FirstOrDefault(t =>
                t.CategoryID == CategoryID);
        }

        public static Customer Find(this ITable<Customer> table, string CustomerID)
        {
            return table.FirstOrDefault(t =>
                t.CustomerID == CustomerID);
        }

        public static CustomerCustomerDemo Find(this ITable<CustomerCustomerDemo> table, string CustomerID, string CustomerTypeID)
        {
            return table.FirstOrDefault(t =>
                t.CustomerID == CustomerID &&
                t.CustomerTypeID == CustomerTypeID);
        }

        public static CustomerDemographic Find(this ITable<CustomerDemographic> table, string CustomerTypeID)
        {
            return table.FirstOrDefault(t =>
                t.CustomerTypeID == CustomerTypeID);
        }

        public static Employee Find(this ITable<Employee> table, int EmployeeID)
        {
            return table.FirstOrDefault(t =>
                t.EmployeeID == EmployeeID);
        }

        public static EmployeeTerritory Find(this ITable<EmployeeTerritory> table, int EmployeeID, string TerritoryID)
        {
            return table.FirstOrDefault(t =>
                t.EmployeeID == EmployeeID &&
                t.TerritoryID == TerritoryID);
        }

        public static Order Find(this ITable<Order> table, int OrderID)
        {
            return table.FirstOrDefault(t =>
                t.OrderID == OrderID);
        }

        public static OrderDetail Find(this ITable<OrderDetail> table, int OrderID, int ProductID)
        {
            return table.FirstOrDefault(t =>
                t.OrderID == OrderID &&
                t.ProductID == ProductID);
        }

        public static Product Find(this ITable<Product> table, int ProductID)
        {
            return table.FirstOrDefault(t =>
                t.ProductID == ProductID);
        }

        public static Region Find(this ITable<Region> table, int RegionID)
        {
            return table.FirstOrDefault(t =>
                t.RegionID == RegionID);
        }

        public static Shipper Find(this ITable<Shipper> table, int ShipperID)
        {
            return table.FirstOrDefault(t =>
                t.ShipperID == ShipperID);
        }

        public static Supplier Find(this ITable<Supplier> table, int SupplierID)
        {
            return table.FirstOrDefault(t =>
                t.SupplierID == SupplierID);
        }

        public static Territory Find(this ITable<Territory> table, string TerritoryID)
        {
            return table.FirstOrDefault(t =>
                t.TerritoryID == TerritoryID);
        }
    }
}
