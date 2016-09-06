CREATE TABLE [dbo].[EmployeeCards] (
    [CardNumber] VARCHAR (16)  NULL,
    [Expire]     DATETIME2 (7) NULL,
    [CardHolder] VARCHAR (50)  NULL,
    [EmployeeID] INT           NULL,
    FOREIGN KEY ([EmployeeID]) REFERENCES [dbo].[Employees] ([EmployeeID])
);

