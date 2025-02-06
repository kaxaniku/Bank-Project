CREATE PROCEDURE sp_InsertCustomer
    @Name NVARCHAR(100),
	@AdressLine1  NVARCHAR(100),
	@CityID INT,
    @Phone NVARCHAR(20),
	@Email NVARCHAR(20),
    @CustomerID INT OUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Customers (Name,AddressLine1, CityID, Phone, Email)
    VALUES (@Name,@AdressLine1, @CityID, @Phone, @Email);

    SET @CustomerID = SCOPE_IDENTITY();

    RETURN 0;
END