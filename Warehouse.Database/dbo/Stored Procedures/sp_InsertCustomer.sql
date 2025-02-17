CREATE PROCEDURE sp_InsertCustomer
    @Name NVARCHAR(100),
	@AddressLine1 NVARCHAR(100),
    @AddressLine2 NVARCHAR(100) = NULL,
	@CityID INT,
    @Phone VARCHAR(24),
	@Email VARCHAR(50),
    @CustomerID INT OUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Customers (Name,AddressLine1, AddressLine2, CityID, Phone, Email)
    VALUES (@Name,@AddressLine1, @AddressLine2, @CityID, @Phone, @Email);

    SET @CustomerID = SCOPE_IDENTITY();

    RETURN 0;
END