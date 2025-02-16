CREATE PROCEDURE sp_InsertTags
    @Name NVARCHAR(50),
    @Description NVARCHAR(1000),
    @TagID INT OUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Tags (Name, Description)
    VALUES (@Name, @Description);

    SET @TagID = SCOPE_IDENTITY();

    RETURN 0;
END