CREATE PROCEDURE sp_InsertProductTags
	@TagID INT,
	@ProductID INT
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS (SELECT 1 FROM ProductTags WHERE TagID = @TagID AND ProductID = @ProductID)
	BEGIN
		RAISERROR('Record already exists', 16, 1);
		RETURN 1;
	END

	INSERT INTO ProductTags (TagID, ProductID)
	VALUES (@TagID, @ProductID);

	RETURN 0;
END