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
	END;
	GO

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
	END;
	GO

	CREATE PROCEDURE sp_UpdateProductTags
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
		SET TagID = @NewTagID, ProductID = @NewProductID
		WHERE TagID = @OldTagID AND ProductID = @OldProductID;

		RETURN 0;
	END;
	GO
