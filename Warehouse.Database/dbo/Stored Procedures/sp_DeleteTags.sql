CREATE PROCEDURE sp_DeleteTags
    @TagID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Tags WHERE TagID = @TagID AND IsActive = 0)
    BEGIN
        RAISERROR('Record is already not active', 16, 1);
        RETURN 1;
    END

    UPDATE Tags
    SET IsActive = 0
    WHERE TagID = @TagID;

    RETURN 0;
END;