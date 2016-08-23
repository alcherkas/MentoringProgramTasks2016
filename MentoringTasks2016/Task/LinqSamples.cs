// Copyright © Microsoft Corporation.  All Rights Reserved.
// This code released under the terms of the 
// Microsoft Public License (MS-PL, http://opensource.org/licenses/ms-pl.html.)
//
//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Linq;
using SampleSupport;
using Task.Data;
using System.Collections.Generic;

// Version Mad01

namespace SampleQueries
{
    public interface ISamples
    {
        List<Customer> GetCustomersWithOrderSumMoreThan();
        List<Customer> GetCustomersWithAnyOrderMoreThan();
        List<CustomerStatistic> GetGustomersStartDate();
        IEnumerable<ProductCategoryGroup> GetProductCategories();
    }

    [Title("LINQ Module")]
    [Prefix("Get")]
    public class LinqSamples : SampleHarness, ISamples
    {
        private IDataSource _dataSource;

        private const string CategoryName = "LINQ";

        public LinqSamples(IDataSource dataSource)
        {
            _dataSource = dataSource;
        }

        [Category(CategoryName)]
        [Title("Task 1")]
        [Description("Selects customers with total orders sum greater than 500")]
        public List<Customer> GetCustomersWithOrderSumMoreThan()
        {
            decimal totalOrderSum = 500;
            var customers = _dataSource.Customers
                                .Where(customer => customer.Orders.Sum(order => order.Total) > totalOrderSum)
                                .ToList();

            foreach(var customer in customers)
                ObjectDumper.Write(customer);

            return customers;
        }

        [Category(CategoryName)]
        [Title("Task 3")]
        [Description("Selects customer with any order with total value greater than 100")]
        public List<Customer> GetCustomersWithAnyOrderMoreThan()
        {
            decimal orderTotal = 100;
            var customers = _dataSource.Customers
                                .Where(customer=>customer.Orders.Any(order=> order.Total > orderTotal))
                                .ToList();

            foreach (var customer in customers) ObjectDumper.Write(customer);

            return customers;
        }

        [Category(CategoryName)]
        [Title("Task 5")]
        [Description("Returns clients with first order date.")]
        public List<CustomerStatistic> GetGustomersStartDate()
        {
            var customersWithOrders = _dataSource.Customers.Where(c => c.Orders.Any());
            var customersWithOrderDate = customersWithOrders.Select(
                    c =>
                        new CustomerStatistic
                        {
                            Customer = c,
                            StartDateTime = c.Orders.OrderBy(o => o.OrderDate).First().OrderDate
                        });



            var orderedCustomers = customersWithOrderDate.OrderBy(x => x.StartDateTime)
                    .ThenByDescending(x => x.Customer.Orders.Count())
                    .ThenBy(x => x.Customer.CompanyName)
                    .ToList();

            foreach (var customerInfo in orderedCustomers)
            {
                ObjectDumper.Write(customerInfo.Customer);
                ObjectDumper.Write(customerInfo.StartDateTime);
            }

            return orderedCustomers;
        }

        [Category(CategoryName)]
        [Title("Task 7")]
        [Description("Selects products grouped by category. In category grouped by units in stock. And for the last group products should be ordered by price.")]
        public IEnumerable<ProductCategoryGroup> GetProductCategories()
        {
            var groupedProducts = _dataSource.Products
                .GroupBy(p => p.Category)
                .Select(gp => new ProductCategoryGroup
                              {
                                  Key = gp.Key,
                                  Entities = gp.GroupBy(p => p.UnitsInStock)
                                               .Select(x => new ProductUnitGroup
                                                            {
                                                                Key = x.Key,
                                                                Entities = x.ToList()
                                                            })
                                               .ToList()
                              })
                .ToList();

            foreach(var item in groupedProducts)
            {
                var lastProductGroup = item.Entities.Last();
                lastProductGroup.Entities = lastProductGroup.Entities.OrderBy(x => x.UnitPrice).ToList();
            }

            foreach(var categoryGroup in groupedProducts)
            {
                Console.WriteLine(categoryGroup.Key);
                foreach(var unitGroup in categoryGroup.Entities)
                {
                    Console.WriteLine(unitGroup.Key);
                    foreach (var product in unitGroup.Entities) ObjectDumper.Write(product);
                    Console.WriteLine();
                }
                Console.WriteLine();
            }

            return groupedProducts;
        }

        [Category(CategoryName)]
        [Title("Task 9 - 1")]
        [Description("Calculates average profitability foreach city.")]
        public List<AverageProfitability> GetAverageProfitability()
        {
            var profitabilities = _dataSource.Customers.GroupBy(x => x.City).Select(x => new AverageProfitability(x.Key, x.Average(xx => xx.Orders.Sum(xxx => xxx.Total)))).ToList();

            foreach (var profitability in profitabilities) ObjectDumper.Write(profitability);

            return profitabilities;
        }

        [Category(CategoryName)]
        [Title("Task 9 - 2")]
        [Description("Calculates average intensity foreach city.")]
        public List<AverageIntensity> GetAverageIntensity()
        {
            var intensities = _dataSource.Customers.GroupBy(x => x.City).Select(x => new AverageIntensity(x.Key, x.Average(xx => xx.Orders.Count()))).ToList();

            foreach (var intensity in intensities) ObjectDumper.Write(intensity);

            return intensities;
        }
    }
}