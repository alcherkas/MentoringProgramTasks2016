select 
(select top 1 concat(FirstName, ' ', LastName) from Employees where EmployeeID = e2.ReportsTo) as 'BossName'
from Employees as e2
where ReportsTo is not null
group by ReportsTo