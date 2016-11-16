select OrderID, 
case 
    when ShippedDate is null then 'Non shipped'
    else Convert(VARCHAR, ShippedDate)
end
from Orders