select CustomerID, Country 
from Customers
where Country >= 'b' and Country <= char(ascii('g') + 1)