CREATE PROCEDURE sp_UpdateProduct
	@ProductID INT,
	@CategoryID INT,
	@Barcode VARCHAR(100),
	@Name NVARCHAR(100),
	@Description NVARCHAR(1000) = NULL,
	@Dimensions NVARCHAR(255),
	@Weight Float
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS(SELECT 1 FROM Products WHERE ProductID = @ProductID AND IsActive = 0)
	BEGIN
		RAISERROR('Record is not active', 16, 1);
		RETURN 1;
	END

	UPDATE Products
	SET CategoryID = @CategoryID,
	    Barcode = @Barcode,
		Name = @Name,
		Description = @Description,
	    Dimensions = @Dimensions,
		Weight = @Weight,
		UpdateDate = GETDATE()
	WHERE 
		ProductID = @ProductID 
		AND (
		    CategoryID != @CategoryID 
		    OR Barcode != @Barcode
		    OR Name != @Name
			OR COALESCE(Description, '') != COALESCE(@Description, '')
		    OR Dimensions != @Dimensions
		    OR Weight != @Weight
		);

	RETURN 0;
END