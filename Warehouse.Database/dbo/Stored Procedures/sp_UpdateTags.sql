CREATE PROCEDURE sp_UpdateTags
    @TagID INT,
    @Name NVARCHAR(50),
    @Description NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Tags WHERE TagID = @TagID AND IsActive = 0)
    BEGIN
        RAISERROR('Record is not active', 16, 1);
        RETURN 1;
    END

    UPDATE Tags
    SET Name = @Name,
        Description = @Description,
        UpdateDate = GETDATE()
    WHERE TagID = @TagID AND (Name != @Name OR Description != @Description);

    RETURN 0;
END;