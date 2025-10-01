CREATE PROCEDURE sp_InsertProduct
	@ProductID INT OUT,
	@ProductName VARCHAR(100) NOT NULL,
    @Price DECIMAL(10, 2) NOT NULL,
    @Stock INT NOT NULL,
    @Photo VARBINARY(MAX) NULL
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Products(ProductName, Price, Stock, Photo)
	VALUES (@ProductName, @Price, @Stock, @Photo);

	SET @ProductID = SCOPE_IDENTITY();

	RETURN 0;
END