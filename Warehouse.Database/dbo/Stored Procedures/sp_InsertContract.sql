CREATE PROCEDURE sp_InsertContract
    @Name NVARCHAR(100),
    @Description NVARCHAR(1000) = NULL,
    @CustomerID INT,
    @EmployeeID INT,
    @Price MONEY,
    @ContractID INT OUT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO Contracts (Name, Description, CustomerID, EmployeeID, Price)
    VALUES (@Name, @Description, @CustomerID, @EmployeeID, @Price);

    SET @ContractID = SCOPE_IDENTITY();

    RETURN 0;
END