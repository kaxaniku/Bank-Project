CREATE PROCEDURE UpdateContract
    @ContractID INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(1000),
    @CustomerID INT,
    @EmployeeID INT,
    @Price MONEY
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM Contracts WHERE ContractID = @ContractID AND IsActive = 0)
	BEGIN
		RAISERROR('Record is not active', 16, 1);
		RETURN 1;
	END
    
    UPDATE Contracts
    SET 
        ContractID = @ContractID,
        Name = @Name,
        Description = @Description,
        CustomerID = @CustomerID,
        EmployeeID = @EmployeeID,
        Price = @Price,
        UpdateDate = GETDATE()
    WHERE
        ContractID = @ContractID 
        AND (
            Name != @Name 
            OR Description != @Description 
            OR CustomerID != @CustomerID 
            OR EmployeeID != @EmployeeID 
            OR Price != @Price
        );
END