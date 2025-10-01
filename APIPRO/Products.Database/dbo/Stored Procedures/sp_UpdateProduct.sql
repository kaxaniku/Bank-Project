CREATE PROCEDURE sp_UpdateProduct
	@ProductID INT,
	@ProductName VARCHAR(100),
    @Price DECIMAL(10, 2),
    @Stock INT,
    @Photo VARBINARY(MAX) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS(SELECT 1 FROM Products WHERE ProductID = @ProductID)
	BEGIN
		RAISERROR('Record is inexistent', 16, 1);
		RETURN 1;
	END

	UPDATE Products
	SET	ProductName = @ProductName,
		Price = @Price,
		Stock = @Stock,
		Photo = @Photo
	WHERE 
		ProductID = @ProductID 
		AND (
		    ProductName != @ProductName OR
			Price != @Price OR
			Stock != @Stock OR
			Photo != @Photo
		);

	RETURN 0;
END