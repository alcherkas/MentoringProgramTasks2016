select FirstName, LastName, count(Orders.OrderID) as Total
from Employees 
left join Orders on Employees.EmployeeID = Orders.EmployeeID
group by FirstName, LastName
order by Total