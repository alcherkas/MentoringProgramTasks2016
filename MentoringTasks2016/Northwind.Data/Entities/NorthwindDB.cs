using System;
using System.Linq.Expressions;
using System.Reflection;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.DataProvider.SqlServer;

namespace Northwind.Data.Entities
{
    /// <summary>
    /// Database       : Northwind
    /// Data Source    : localhost
    /// Server Version : 12.00.4213
    /// </summary>
    public partial class NorthwindDB : DataConnection
    {
        public ITable<AlphabeticalListOfProduct> AlphabeticalListOfProducts { get { return this.GetTable<AlphabeticalListOfProduct>(); } }
        public ITable<Category> Categories { get { return this.GetTable<Category>(); } }
        public ITable<CategorySalesFor1997> CategorySalesFor1997 { get { return this.GetTable<CategorySalesFor1997>(); } }
        public ITable<CurrentProductList> CurrentProductLists { get { return this.GetTable<CurrentProductList>(); } }
        public ITable<Customer> Customers { get { return this.GetTable<Customer>(); } }
        public ITable<CustomerAndSuppliersByCity> CustomerAndSuppliersByCities { get { return this.GetTable<CustomerAndSuppliersByCity>(); } }
        public ITable<CustomerCustomerDemo> CustomerCustomerDemoes { get { return this.GetTable<CustomerCustomerDemo>(); } }
        public ITable<CustomerDemographic> CustomerDemographics { get { return this.GetTable<CustomerDemographic>(); } }
        public ITable<Employee> Employees { get { return this.GetTable<Employee>(); } }
        public ITable<EmployeeTerritory> EmployeeTerritories { get { return this.GetTable<EmployeeTerritory>(); } }
        public ITable<Invoice> Invoices { get { return this.GetTable<Invoice>(); } }
        public ITable<Order> Orders { get { return this.GetTable<Order>(); } }
        public ITable<OrderDetail> OrderDetails { get { return this.GetTable<OrderDetail>(); } }
        public ITable<OrderDetailsExtended> OrderDetailsExtendeds { get { return this.GetTable<OrderDetailsExtended>(); } }
        public ITable<OrdersQry> OrdersQries { get { return this.GetTable<OrdersQry>(); } }
        public ITable<OrderSubtotal> OrderSubtotals { get { return this.GetTable<OrderSubtotal>(); } }
        public ITable<Product> Products { get { return this.GetTable<Product>(); } }
        public ITable<ProductsAboveAveragePrice> ProductsAboveAveragePrices { get { return this.GetTable<ProductsAboveAveragePrice>(); } }
        public ITable<ProductSalesFor1997> ProductSalesFor1997 { get { return this.GetTable<ProductSalesFor1997>(); } }
        public ITable<ProductsByCategory> ProductsByCategories { get { return this.GetTable<ProductsByCategory>(); } }
        public ITable<QuarterlyOrder> QuarterlyOrders { get { return this.GetTable<QuarterlyOrder>(); } }
        public ITable<Region> Regions { get { return this.GetTable<Region>(); } }
        public ITable<SalesByCategory> SalesByCategories { get { return this.GetTable<SalesByCategory>(); } }
        public ITable<SalesTotalsByAmount> SalesTotalsByAmounts { get { return this.GetTable<SalesTotalsByAmount>(); } }
        public ITable<Shipper> Shippers { get { return this.GetTable<Shipper>(); } }
        public ITable<SummaryOfSalesByQuarter> SummaryOfSalesByQuarters { get { return this.GetTable<SummaryOfSalesByQuarter>(); } }
        public ITable<SummaryOfSalesByYear> SummaryOfSalesByYears { get { return this.GetTable<SummaryOfSalesByYear>(); } }
        public ITable<Supplier> Suppliers { get { return this.GetTable<Supplier>(); } }
        public ITable<Territory> Territories { get { return this.GetTable<Territory>(); } }

        public NorthwindDB()
        {
            InitDataContext();
        }

        public NorthwindDB(string configuration)
            : base(configuration)
        {
            InitDataContext();
        }

        partial void InitDataContext();

        #region FreeTextTable

        public class FreeTextKey<T>
        {
            public T Key;
            public int Rank;
        }

        [FreeTextTableExpression]
        public ITable<FreeTextKey<TKey>> FreeTextTable<TTable, TKey>(string field, string text)
        {
            return this.GetTable<FreeTextKey<TKey>>(
                this,
                ((MethodInfo)(MethodBase.GetCurrentMethod())).MakeGenericMethod(typeof(TTable), typeof(TKey)),
                field,
                text);
        }

        [FreeTextTableExpression]
        public ITable<FreeTextKey<TKey>> FreeTextTable<TTable, TKey>(Expression<Func<TTable, string>> fieldSelector, string text)
        {
            return this.GetTable<FreeTextKey<TKey>>(
                this,
                ((MethodInfo)(MethodBase.GetCurrentMethod())).MakeGenericMethod(typeof(TTable), typeof(TKey)),
                fieldSelector,
                text);
        }

        #endregion
    }
}