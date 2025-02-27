CREATE PROCEDURE sp_UpdateCategory
    @CategoryID INT,
    @Name NVARCHAR(100),
    @Description NVARCHAR(1000) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM Categories WHERE CategoryID = @CategoryID AND IsActive = 0)
    BEGIN
        RAISERROR('Record is not active', 16, 1);
        RETURN 1;
    END

    UPDATE Categories
    SET Name = @Name,
        Description = @Description,
        UpdateDate = GETDATE()
    WHERE 
        CategoryID = @CategoryID
        AND (
            Name != @Name
            OR COALESCE(Description, '') != COALESCE(@Description, '')
        );

    RETURN 0;
END