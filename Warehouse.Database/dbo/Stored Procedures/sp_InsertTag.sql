CREATE PROCEDURE sp_InsertTag
    @Name NVARCHAR(50),
    @Description NVARCHAR(1000) = NULL,
    @TagID INT OUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Tags (Name, Description)
    VALUES (@Name, @Description);

    SET @TagID = SCOPE_IDENTITY();

    RETURN 0;
END