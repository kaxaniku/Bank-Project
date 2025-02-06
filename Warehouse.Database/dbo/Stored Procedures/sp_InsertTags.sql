CREATE PROCEDURE sp_InsertTags
    @Name NVARCHAR(50),
    @Description NVARCHAR(MAX),
    @TagID INT OUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Tags (Name, Description, IsActive, CreateDate)
    VALUES (@Name, @Description, 1, GETDATE());

    SET @TagID = SCOPE_IDENTITY();

    RETURN 0;
END