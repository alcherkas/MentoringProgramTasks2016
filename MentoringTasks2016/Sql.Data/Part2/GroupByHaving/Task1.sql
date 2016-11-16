select Year(OrderDate) as 'Year', count(*) as 'Total'
from Orders
group by Year(OrderDate)