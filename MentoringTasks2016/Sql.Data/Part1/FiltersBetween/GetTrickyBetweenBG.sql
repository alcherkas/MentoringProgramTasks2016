select CustomerID, Country 
from Customers
where Country between 'b' and CHAR(ASCII('g')+1)
order by Country