select FirstName, LastName from Employees
join EmployeeTerritories on Employees.EmployeeID = EmployeeTerritories.EmployeeID
join Territories on EmployeeTerritories.TerritoryID = Territories.TerritoryID
join Region on Territories.RegionID = Region.RegionID
where RegionDescription = 'Western'
group by FirstName, LastName