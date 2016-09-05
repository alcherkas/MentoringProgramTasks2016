select (select Concat(LastName, ' ', FirstName) from Employees where Employees.EmployeeID = Orders.EmployeeID) as 'Seller',
count(*) as 'Amount'
from Orders
group by EmployeeID
order by Amount desc