using System;
using System.Collections.Generic;
using SampleQueries;
using Task.Data;
using Xunit;
using System.Linq;

namespace Task.UnitTests
{
    public class LinqSampleTests
    {
        [Fact]
        public void GetCustomersWithOrderSumMoreThan_AllCustomersHasMoreThanOrders()
        {
            var source = DataSourceFactory.Create();
            var samples = new LinqSamples(source);
            var moreThanValue = 400;

            var result = samples.GetCustomersWithOrderSumMoreThan(moreThanValue);

            Assert.True(result.All(x => x.Orders.Sum(xx => xx.Total) > moreThanValue));
        }

        [Fact]
        public void GetCustomersWithAnyOrderMoreThan_AllCustomersHasAnyOrderMoreThan()
        {
            var source = DataSourceFactory.Create();
            var samples = new LinqSamples(source);
            var moreThanValue = 100;

            var result = samples.GetCustomersWithAnyOrderMoreThan(moreThanValue);

            Assert.True(result.All(x => x.Orders.Any(xx => xx.Total > moreThanValue)));
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
                customers.OrderBy(x => x.StartDateTime)
                    .ThenByDescending(x => x.Customer.Orders.Count())
                    .OrderBy(s => s.Customer.CompanyName)
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
            var groupedProducts = samples.GetProductCategories();
            var expectedGrouping = GetExpectedGroup(source);

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
            var category = groupedProducts.First();

            var expectedGrouping = GetExpectedGroup(source);
            var expectedCategory = expectedGrouping.First();

            // Assert.
            Assert.True(category.Entities.SequenceEqual(expectedCategory.Entities));
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

            var orderedExpectedCategory = expectedCategory.Entities.Last().Entities.OrderBy(xx => xx.UnitPrice);

            // Assert.
            Assert.True(category.SequenceEqual(orderedExpectedCategory));
        }

        private static IEnumerable<ProductCategoryGroup> GetExpectedGroup(IDataSource source)
        {
            var expectedGrouping = source.Products
                .GroupBy(x => x.Category)
                .Select(
                    x => new ProductCategoryGroup
                    {
                        Key = x.Key,
                        Entities = x.GroupBy(xx => xx.UnitsInStock)
                                        .Select(xx => new ProductUnitGroup
                                        {
                                            Key = xx.Key,
                                            Entities = xx
                                        })
                    });

            return expectedGrouping;
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

                public List<Product> Products
                {
                    get
                    {
                        throw new NotImplementedException();
                    }
                }

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
