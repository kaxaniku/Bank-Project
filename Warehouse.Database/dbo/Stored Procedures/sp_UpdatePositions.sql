CREATE OR ALTER PROCEDURE sp_UpdatePosition
    @PositionID INT,
    @PositionName NVARCHAR(50),
    @DepartmentID INT,
    @Name NVARCHAR(50),
    @Description NVARCHAR(1000),
    @Salary MONEY,
    @PositionID OUT INT
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
        PositionName = @PositionName,
        DepartmentID = @DepartmentID,
        Name = @Name,
        Description = @Description,
        Salary = @Salary,
        UpdateDate = GETDATE()
    WHERE 
        PositionID = @PositionID 
        AND (PositionName != @PositionName OR DepartmentID != @DepartmentID OR 
             Name != @Name OR Description != @Description OR Salary != @Salary);

    SET @PositionID = @PositionID;

    RETURN 0;
END;
