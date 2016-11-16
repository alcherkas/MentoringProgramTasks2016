select EmployeeID, CustomerID 
from Orders 
where (select City from Customers where CustomerID = Orders.CustomerID) 
    = (select City from Employees where EmployeeID = Orders.EmployeeID)