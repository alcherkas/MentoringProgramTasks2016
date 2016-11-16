select CompanyName, Country
from Customers
where Country not in ('usa', 'canada')
order by CompanyName