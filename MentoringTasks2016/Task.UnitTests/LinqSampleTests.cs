﻿using System;
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
            // Arrange
            var source = DataSourceFactory.Create();
            var samples = new LinqSamples(source);

            // Act
            var customers = samples.GetGustomersStartDate();

            var expectedCustomersOrder =
                customers.OrderBy(x => x.StartDateTime)
                    .ThenByDescending(x => x.Customer.Orders.Count())
                    .OrderBy(s => s.Customer.CompanyName)
                    .ToList();
            Assert.True(customers.SequenceEqual(expectedCustomersOrder));
        }

        private static class DataSourceFactory
        {
            public static IDataSource Create() => new DataSource();

            private class DataSource : IDataSource
            {
                private List<Customer> _customers;

                public DataSource()
                {
                    Initialize();
                }

                private void Initialize()
                {
                    _customers = GetCustomers();
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
            }
        }
    }
}
