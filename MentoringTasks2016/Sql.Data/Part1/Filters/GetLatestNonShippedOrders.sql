select OrderID as 'Order Number', 
(case 
    when ShippedDate is null then 'Non shipped'
    else Convert(VARCHAR, ShippedDate)
end) as 'Shipped Date'
from Orders
where ShippedDate >= '1998-05-07 00:00:00.000'