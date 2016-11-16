select OrderID, ShippedDate, ShipVia
from dbo.Orders
where ShippedDate > '1998-05-06 00:00:00.000' and ShipVia >= 2