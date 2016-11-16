select EmployeeID, CustomerID, count(*) from Orders
where year(OrderDate) = 1998
group by EmployeeID, CustomerID