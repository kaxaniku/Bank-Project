CREATE OR ALTER PROCEDURE sp_DeleteProduct
	@ProductID INT
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS(SELECT 1 FROM Products WHERE ProductID = @ProductID AND IsActive = 0)
	BEGIN
		RAISERROR('Record is already not active', 16, 1);
		RETURN 1;
	END

	UPDATE Products
	SET IsActive = 0
	WHERE ProductID = @ProductID;

	RETURN 0;
END