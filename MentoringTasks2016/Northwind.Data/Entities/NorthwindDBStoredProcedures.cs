using System;
using System.Collections.Generic;
using LinqToDB;
using LinqToDB.Data;

namespace Northwind.Data.Entities
{
    public static class NorthwindDBStoredProcedures
    {
        #region CustOrderHist

        public static IEnumerable<CustOrderHistResult> CustOrderHist(this DataConnection dataConnection, string @CustomerID)
        {
            return dataConnection.QueryProc<CustOrderHistResult>("[dbo].[CustOrderHist]",
                new DataParameter("@CustomerID", @CustomerID, DataType.NChar));
        }

        public class CustOrderHistResult
        {
            public string ProductName { get; set; }
            public int? Total { get; set; }
        }

        #endregion

        #region CustOrdersDetail

        public static IEnumerable<CustOrdersDetailResult> CustOrdersDetail(this DataConnection dataConnection, int? @OrderID)
        {
            return dataConnection.QueryProc<CustOrdersDetailResult>("[dbo].[CustOrdersDetail]",
                new DataParameter("@OrderID", @OrderID, DataType.Int32));
        }

        public class CustOrdersDetailResult
        {
            public string ProductName { get; set; }
            public decimal UnitPrice { get; set; }
            public short Quantity { get; set; }
            public int? Discount { get; set; }
            public decimal? ExtendedPrice { get; set; }
        }

        #endregion

        #region CustOrdersOrders

        public static IEnumerable<CustOrdersOrdersResult> CustOrdersOrders(this DataConnection dataConnection, string @CustomerID)
        {
            return dataConnection.QueryProc<CustOrdersOrdersResult>("[dbo].[CustOrdersOrders]",
                new DataParameter("@CustomerID", @CustomerID, DataType.NChar));
        }

        public class CustOrdersOrdersResult
        {
            public int OrderID { get; set; }
            public DateTime? OrderDate { get; set; }
            public DateTime? RequiredDate { get; set; }
            public DateTime? ShippedDate { get; set; }
        }

        #endregion

        #region EmployeeSalesByCountry

        public static IEnumerable<EmployeeSalesByCountryResult> EmployeeSalesByCountry(this DataConnection dataConnection, DateTime? @Beginning_Date, DateTime? @Ending_Date)
        {
            return dataConnection.QueryProc<EmployeeSalesByCountryResult>("[dbo].[Employee Sales by Country]",
                new DataParameter("@Beginning_Date", @Beginning_Date, DataType.DateTime),
                new DataParameter("@Ending_Date", @Ending_Date, DataType.DateTime));
        }

        public class EmployeeSalesByCountryResult
        {
            public string Country { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public DateTime? ShippedDate { get; set; }
            public int OrderID { get; set; }
            public decimal? SaleAmount { get; set; }
        }

        #endregion

        #region SalesByYear

        public static IEnumerable<SalesByYearResult> SalesByYear(this DataConnection dataConnection, DateTime? @Beginning_Date, DateTime? @Ending_Date)
        {
            return dataConnection.QueryProc<SalesByYearResult>("[dbo].[Sales by Year]",
                new DataParameter("@Beginning_Date", @Beginning_Date, DataType.DateTime),
                new DataParameter("@Ending_Date", @Ending_Date, DataType.DateTime));
        }

        public class SalesByYearResult
        {
            public DateTime? ShippedDate { get; set; }
            public int OrderID { get; set; }
            public decimal? Subtotal { get; set; }
            public string Year { get; set; }
        }

        #endregion

        #region SalesByCategory

        public static IEnumerable<SalesByCategoryResult> SalesByCategory(this DataConnection dataConnection, string @CategoryName, string @OrdYear)
        {
            return dataConnection.QueryProc<SalesByCategoryResult>("[dbo].[SalesByCategory]",
                new DataParameter("@CategoryName", @CategoryName, DataType.NVarChar),
                new DataParameter("@OrdYear", @OrdYear, DataType.NVarChar));
        }

        public class SalesByCategoryResult
        {
            public string ProductName { get; set; }
            public decimal? TotalPurchase { get; set; }
        }

        #endregion

        #region TenMostExpensiveProducts

        public static IEnumerable<TenMostExpensiveProductsResult> TenMostExpensiveProducts(this DataConnection dataConnection)
        {
            return dataConnection.QueryProc<TenMostExpensiveProductsResult>("[dbo].[Ten Most Expensive Products]");
        }

        public class TenMostExpensiveProductsResult
        {
            public string TenMostExpensiveProducts { get; set; }
            public decimal? UnitPrice { get; set; }
        }

        #endregion
    }
}