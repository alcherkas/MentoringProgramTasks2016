select distinct FirstName, LastName
from Employees 
where EmployeeID in 
(select distinct Orders.EmployeeID from Orders group by Orders.EmployeeID having count(EmployeeID) > 150)