CREATE PROCEDURE sp_InsertProduct
	@CategoryID INT,
	@ProductID INT OUT,
	@Barcode VARCHAR(100),
	@Name NVARCHAR(100),
	@Description NVARCHAR(1000),
	@Dimensions NVARCHAR(255),
	@Weight Float
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO Products(CategoryID, Barcode, Name, Description, Dimensions, Weight)
	VALUES (@CategoryID, @Barcode, @Name, @Description, @Dimensions, @Weight);

	SET @ProductID = SCOPE_IDENTITY();

	RETURN 0;
END