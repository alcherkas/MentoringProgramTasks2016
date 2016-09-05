select CompanyName, Country
from Customers
where Country in ('usa', 'canada')
order by CompanyName, Country, Region, City, Address