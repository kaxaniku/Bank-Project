CREATE PROCEDURE sp_UpdatePosition
    @PositionID INT,
    @Name NVARCHAR(50),
    @Description NVARCHAR(1000),
    @Salary MONEY
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Positions WHERE PositionID = @PositionID AND IsActive = 0)
    BEGIN
        RAISERROR('Record is not active', 16, 1);
        RETURN 1;
    END

    UPDATE Positions
    SET 
        Name = @Name,
        Description = @Description,
        Salary = @Salary,
        UpdateDate = GETDATE()
    WHERE 
        PositionID = @PositionID 
        AND (Name != @Name  OR Description != @Description OR Salary != @Salary);

    SET @PositionID = @PositionID;

    RETURN 0;
END