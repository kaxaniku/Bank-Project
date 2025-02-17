CREATE PROCEDURE sp_UpdateProductTag
	@OldTagID INT,
	@OldProductID INT,
	@NewTagID INT,
	@NewProductID INT
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS (SELECT 1 FROM ProductTags WHERE TagID = @OldTagID AND ProductID = @OldProductID)
	BEGIN
		RAISERROR('Record to update does not exist', 16, 1);
		RETURN 1;
	END

	IF EXISTS (SELECT 1 FROM ProductTags WHERE TagID = @NewTagID AND ProductID = @NewProductID)
	BEGIN
		RAISERROR('New record already exists', 16, 1);
		RETURN 1;
	END

	UPDATE ProductTags
	SET TagID = @NewTagID,
		ProductID = @NewProductID
	WHERE TagID = @OldTagID AND ProductID = @OldProductID;

	RETURN 0;
END