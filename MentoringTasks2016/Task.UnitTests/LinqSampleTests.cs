using System;
using System.Collections.Generic;
using SampleQueries;
using Task.Data;
using Xunit;
using System.Linq;
using Task.Data.ReadModels;

namespace Task.UnitTests
{
    public class LinqSampleTests
    {
        [Fact]
        public void GetCustomersWithOrderSumMoreThan_AllCustomersHasMoreThanOrders()
        {
            var source = DataSourceFactory.Create();
            var samples = new LinqSamples(source);
            var moreThanValue = 500;

            var result = samples.GetCustomersWithOrderSumMoreThan();

            Assert.True(result.All(customer => customer.Orders.Sum(order => order.Total) > moreThanValue));
        }

        [Fact]
        public void GetCustomersWithAnyOrderMoreThan_AllCustomersHasAnyOrderMoreThan()
        {
            var source = DataSourceFactory.Create();
            var samples = new LinqSamples(source);
            var moreThanValue = 100;

            var result = samples.GetCustomersWithAnyOrderMoreThan();

            Assert.True(result.All(customer => customer.Orders.Any(order => order.Total > moreThanValue)));
        }

        [Fact]
        public void GetGustomersStartDate_CustomersWithOrderedDate()
        {
            // Arrange.
            var source = DataSourceFactory.Create();
            var samples = new LinqSamples(source);

            // Act.
            var customers = samples.GetGustomersStartDate();

            var expectedCustomersOrder =
                customers.OrderBy(cs => cs.StartDateTime)
                    .ThenByDescending(cs => cs.Customer.Orders.Count())
                    .OrderBy(cs => cs.Customer.CompanyName)
                    .ToList();

            // Assert.
            Assert.True(customers.SequenceEqual(expectedCustomersOrder));
        }

        [Fact]
        public void GetProductCategories_ProductGroupedByCategory()
        {
            // Arrange.
            var source = DataSourceFactory.Create();
            var samples = new LinqSamples(source);

            // Act.
            var groupedProducts = samples.GetProductCategories().Select(categoryGroup => categoryGroup.Key).ToList();
            var expectedGrouping = GetExpectedGroup(source).Select(categoryGroup => categoryGroup.Key).ToList();

            // Assert.
            Assert.True(groupedProducts.SequenceEqual(expectedGrouping));
        }

        [Fact]
        public void GetProductCategories_CategoryGroupedByUnitsInStock()
        {
            // Arrange.
            var source = DataSourceFactory.Create();
            var samples = new LinqSamples(source);

            // Act.
            var groupedProducts = samples.GetProductCategories();
            var category = groupedProducts.First().Entities.Select(unitGroup => unitGroup.Key).ToList();

            var expectedGrouping = GetExpectedGroup(source);
            var expectedCategory = expectedGrouping.First().Entities.Select(unitGroup => unitGroup.Key).ToList();

            // Assert.
            Assert.True(category.SequenceEqual(expectedCategory));
        }

        [Fact]
        public void GetProductCategories_LastOrderedByPrice()
        {
            // Arrange.
            var source = DataSourceFactory.Create();
            var samples = new LinqSamples(source);

            // Act.
            var groupedProducts = samples.GetProductCategories();
            var category = groupedProducts.Last().Entities.Last().Entities;

            var expectedGrouping = GetExpectedGroup(source);
            var expectedCategory = expectedGrouping.Last();

            var orderedExpectedCategory = expectedCategory.Entities.Last().Entities.OrderBy(product => product.UnitPrice);

            // Assert.
            Assert.True(category.SequenceEqual(orderedExpectedCategory));
        }

        [Fact]
        public void GetAverageProfitability_GroupedByCity()
        {
            // Arrange.
            var source = DataSourceFactory.Create();
            var samples = new LinqSamples(source);

            // Act.
            var averageProfitability = samples.GetAverageProfitability()
                                            .Select(ap=>ap.City)
                                            .ToList();

            var expectedGrouping = source.Customers
                                    .GroupBy(customer => customer.City)
                                    .Select(customerGroup => customerGroup.Key)
                                    .ToList();

            //Assert.
            Assert.True(averageProfitability.SequenceEqual(expectedGrouping));
        }

        [Fact]
        public void GetAverageProfitability_HasCorrectAverageValue()
        {
            // Arrange.
            var source = DataSourceFactory.Create();
            var samples = new LinqSamples(source);

            // Act.
            var averageProfitability = samples.GetAverageProfitability()
                                            .Select(ap => ap.Value)
                                            .ToList();

            var expectedGrouping = source.Customers.GroupBy(customer => customer.City)
                                            .Select(customerGroup => 
                                                customerGroup.Average(customer => customer.Orders.Sum(order => order.Total)))
                                            .ToList();

            //Assert.
            Assert.True(averageProfitability.SequenceEqual(expectedGrouping));
        }

        [Fact]
        public void GetAverageIntensity_GroupedByCity()
        {
            // Arrange.
            var source = DataSourceFactory.Create();
            var samples = new LinqSamples(source);

            // Act.
            var averageIntensity = samples.GetAverageIntensity()
                                        .Select(ai => ai.City)
                                        .ToList();

            var expectedGrouping = source.Customers
                                       .GroupBy(customer => customer.City)
                                       .Select(customerGroup => customerGroup.Key)
                                       .ToList();

            //Assert.
            Assert.True(averageIntensity.SequenceEqual(expectedGrouping));
        }

        [Fact]
        public void GetAverageIntensity_HasCorrectIntensityValue()
        {
            // Arrange.
            var source = DataSourceFactory.Create();
            var samples = new LinqSamples(source);

            // Act.
            var averageIntensity = samples.GetAverageIntensity()
                                        .Select(x => x.Value)
                                        .ToList();

            var expectedGrouping = source.Customers.GroupBy(x => x.City)
                                            .Select(x => x.Average(xx => xx.Orders.Count()))
                                            .ToList();

            //Assert.
            Assert.True(averageIntensity.SequenceEqual(expectedGrouping));
        }

        private static IEnumerable<ProductCategoryGroup> GetExpectedGroup(IDataSource source)
        {
            var expectedGrouping = source.Products
                .GroupBy(product => product.Category)
                .Select(
                    categoryGroup => new ProductCategoryGroup
                    {
                        Key = categoryGroup.Key,
                        Entities = categoryGroup.GroupBy(product => product.UnitsInStock)
                                        .Select(unitGroup => new ProductUnitGroup
                                        {
                                            Key = unitGroup.Key,
                                            Entities = unitGroup.ToList()
                                        }).ToList()
                    });

            foreach(var item in expectedGrouping)
            {
                var lastGroup = item.Entities.Last();
                lastGroup.Entities = lastGroup.Entities.OrderBy(product => product.UnitPrice).ToList();
            }

            return expectedGrouping.ToList();
        }

        private static class DataSourceFactory
        {
            public static IDataSource Create() => new DataSource();

            private class DataSource : IDataSource
            {
                private List<Customer> _customers;
                private List<Product> _products;

                public DataSource()
                {
                    Initialize();
                }

                private void Initialize()
                {
                    _customers = GetCustomers();
                    _products = GetProducts();
                }

                private List<Product> GetProducts()
                {
                    var products = new List<Product>(5)
                    {
                        new Product { Category = ProductCategories.Food, UnitsInStock = 10, UnitPrice = 10 },
                        new Product { Category = ProductCategories.Furniture, UnitsInStock = 1000, UnitPrice = 5 },
                        new Product { Category = ProductCategories.Phone, UnitsInStock = 300, UnitPrice = 30 },
                        new Product { Category = ProductCategories.Furniture, UnitsInStock = 500, UnitPrice = 13 },
                        new Product { Category = ProductCategories.Food, UnitsInStock = 7, UnitPrice = 18 }
                    };

                    return products;
                }

                private static List<Customer> GetCustomers()
                {
                    var customers = new List<Customer>(5)
                    {
                        new Customer() { CompanyName = "BBC", Orders = new[] { new Order { Total = 10, OrderDate = new DateTime(2015, 10, 11) }, new Order { Total = 10 }, new Order { Total = 10 } } },
                        new Customer() { Orders = new[] { new Order { Total = 500, OrderDate = new DateTime(2016, 10, 11) }, new Order { Total = 10 }, new Order { Total = 10 } } },
                        new Customer() { Orders = new[] { new Order { Total = 10, OrderDate = new DateTime(2013, 10, 11) }, new Order { Total = 10 }, new Order { Total = 10 } } },
                        new Customer() { CompanyName = "ABC", Orders = new[] { new Order { Total = 10, OrderDate = new DateTime(2015, 10, 11) }, new Order { Total = 10 }, new Order { Total = 10 } } },
                        new Customer() { Orders = new[] { new Order { Total = 10 }, new Order { Total = 10 }, new Order { Total = 10 } } }
                    };

                    return customers;
                }

                public List<Customer> Customers => _customers;

                public List<Product> Products => _products;

                public List<Supplier> Suppliers
                {
                    get
                    {
                        throw new NotImplementedException();
                    }
                }

                private static class ProductCategories
                {
                    public const string Furniture = "Furniture";
                    public const string Phone = "Phone";
                    public const string Food = "Food";
                }
            }
        }
    }
}
