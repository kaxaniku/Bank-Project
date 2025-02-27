CREATE PROCEDURE sp_InsertCategory
    @Name NVARCHAR(100),
    @Description NVARCHAR(1000) = NULL,
    @CategoryID INT OUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Categories (Name, Description)
    VALUES (@Name, @Description);

    SET @CategoryID = SCOPE_IDENTITY();

    RETURN 0;
END