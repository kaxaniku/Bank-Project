CREATE PROCEDURE sp_UpdateCustomer
    @CustomerID INT,
    @Name NVARCHAR(100),
    @AddressLine1 NVARCHAR(100),
    @AddressLine2 NVARCHAR(100) = NULL,
    @CityID INT,
    @Phone VARCHAR(24),
	@Email VARCHAR(50)
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
        AddressLine1 = @AddressLine1,
        AddressLine2 = @AddressLine2,
        CityID = @CityID,
        Email = @Email,
        UpdateDate = GETDATE()
	WHERE CustomerID = @CustomerID
        AND (
            Name != @Name 
            OR Phone != @Phone 
            OR AddressLine1 != @AddressLine1 
            OR COALESCE(AddressLine2, '') != COALESCE(@AddressLine2, '')
            OR CityID != @CityID 
            OR Email != @Email
        );

    RETURN 0;
END