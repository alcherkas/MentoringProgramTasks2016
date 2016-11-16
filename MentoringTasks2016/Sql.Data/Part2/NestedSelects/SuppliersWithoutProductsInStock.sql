select distinct CompanyName 
from Suppliers 
where SupplierID in (select distinct Products.SupplierID from Products where UnitsInStock = 0)