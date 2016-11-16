if object_id('Regions') is null
begin
	exec sp_rename 'Region', 'Regions'
	print 'table renamed'
end
if col_length('Customers','SinceDate') is null
begin
	alter table Customers
		add SinceDate datetime2

	print 'column added'
end