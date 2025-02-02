CREATE OR ALTER PROCEDURE sp_UpdateCustomer
    @Name NVARCHAR(100),
    @AdressLine1 NVARCHAR(100),
    @CityID INT,
    @Phone NVARCHAR(20),
    @Email NVARCHAR(20),
    @CustomerID INT OUT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Customers WHERE CustomerID = @CustomerID AND IsActive = 0)
    BEGIN
        RAISERROR('Record is not active', 16, 1);
        RETURN 1;
    END

    UPDATE Customers
    SET 
        Name = @Name,
        Phone = @Phone,
        AddressLine1 = @AdressLine1,
        CityID = @CityID,
        Email = @Email,
        UpdateDate = GETDATE()
    WHERE CustomerID = @CustomerID 
      AND (Name != @Name OR Phone != @Phone OR AddressLine1 != @AdressLine1 OR CityID != @CityID OR Email != @Email);

    RETURN 0;
END;