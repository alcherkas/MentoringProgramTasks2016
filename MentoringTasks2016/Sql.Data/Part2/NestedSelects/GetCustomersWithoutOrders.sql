select distinct CompanyName
from Customers 
where not exists
(select distinct Orders.CustomerID 
 from Orders
 where Orders.CustomerID = Customers.CustomerID 
 group by CustomerID
 having count(CustomerID) >= 0 )