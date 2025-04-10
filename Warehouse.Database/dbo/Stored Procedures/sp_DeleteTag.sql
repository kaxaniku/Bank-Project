CREATE PROCEDURE sp_DeleteTag
    @TagID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS(SELECT 1 FROM Tags WHERE TagID = @TagID AND IsActive = 1)
    BEGIN
		RAISERROR('Record was not found', 16, 1);
        RETURN 1;
    END

    UPDATE Tags
    SET IsActive = 0
    WHERE TagID = @TagID;

    RETURN 0;
END