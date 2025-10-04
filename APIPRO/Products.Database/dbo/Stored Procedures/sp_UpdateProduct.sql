CREATE PROCEDURE sp_UpdateProduct
	@ProductID INT,
	@ProductName VARCHAR(100),
    @Price DECIMAL(10, 2),
    @Stock INT,
    @Photo VARBINARY(MAX) = NULL
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS(SELECT 1 FROM Products WHERE ProductID = @ProductID)
	BEGIN
		RAISERROR('Record is inexistent which is %d', 16, 1, @ProductID);
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