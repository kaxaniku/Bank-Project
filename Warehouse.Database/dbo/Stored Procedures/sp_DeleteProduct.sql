CREATE PROCEDURE sp_DeleteProduct
	@ProductID INT
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS(SELECT 1 FROM Products WHERE ProductID = @ProductID AND IsActive = 1)
	BEGIN
		RAISERROR('Record was not found', 16, 1);
		RETURN 1;
	END

	UPDATE Products
	SET IsActive = 0
	WHERE ProductID = @ProductID;

	RETURN 0;
END