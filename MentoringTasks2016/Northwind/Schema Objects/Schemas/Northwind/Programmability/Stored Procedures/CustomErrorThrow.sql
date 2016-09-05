CREATE PROCEDURE [Northwind].[CustomErrorThrow]
AS
	DECLARE @msg nvarchar(2048); 
	SET @msg = 'Custom error: ' + 'first param';

	THROW 50001, @msg , 1;

RETURN 0


