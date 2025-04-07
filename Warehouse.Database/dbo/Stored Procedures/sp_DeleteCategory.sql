CREATE PROCEDURE sp_DeleteCategory
    @CategoryID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS(SELECT 1 FROM Categories WHERE CategoryID = @CategoryID AND IsActive = 1)
    BEGIN
        RAISERROR('Record was not found', 16, 1);
        RETURN 1;
    END

    UPDATE Categories
    SET IsActive = 0
    WHERE CategoryID = @CategoryID;

    RETURN 0;
END