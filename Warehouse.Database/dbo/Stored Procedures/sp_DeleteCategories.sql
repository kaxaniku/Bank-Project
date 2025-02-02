CREATE OR ALTER PROCEDURE sp_DeleteCategories
    @CategoryID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM Categories WHERE CategoryID = @CategoryID AND IsActive = 0)
    BEGIN
        RAISERROR('Record is already not active', 16, 1);
        RETURN 1;
    END

    UPDATE Categories
    SET IsActive = 0
    WHERE CategoryID = @CategoryID;

    RETURN 0;
END;
