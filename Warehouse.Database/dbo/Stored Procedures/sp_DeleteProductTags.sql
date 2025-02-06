CREATE PROCEDURE sp_DeleteProductTags
	@TagID INT,
	@ProductID INT
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM ProductTags WHERE TagID = @TagID AND ProductID = @ProductID)
	BEGIN
		RAISERROR('Record does not exist', 16, 1);
		RETURN 1;
	END

	DELETE FROM ProductTags
	WHERE TagID = @TagID AND ProductID = @ProductID;

	RETURN 0;
END