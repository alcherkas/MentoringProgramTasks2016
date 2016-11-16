if OBJECT_ID('EmployeeCards') is null
begin
	create table EmployeeCards
	(
		CardNumber varchar(16),
		Expire datetime2,
		CardHolder varchar(50),
		EmployeeID int,
		Foreign key(EmployeeID) references Employees(EmployeeID)
	)	
	print 'table created'
end