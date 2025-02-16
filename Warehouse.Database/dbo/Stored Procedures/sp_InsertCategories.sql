CREATE PROCEDURE sp_InsertCategories
    @Name NVARCHAR(50),
    @Description NVARCHAR(1000),
    @CategoryID INT OUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Categories (Name, Description)
    VALUES (@Name, @Description);

    SET @CategoryID = SCOPE_IDENTITY();

    RETURN 0;
END