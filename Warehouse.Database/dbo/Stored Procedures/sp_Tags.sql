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
GO

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
END;
GO

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
GO
