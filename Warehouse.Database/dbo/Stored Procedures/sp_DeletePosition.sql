CREATE OR ALTER PROCEDURE sp_DeletePosition
    @PositionID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Positions WHERE PositionID = @PositionID AND IsActive = 0)
    BEGIN
        RAISERROR('Record is already not active', 16, 1);
        RETURN 1;
    END

    UPDATE Positions
    SET IsActive = 0
    WHERE PositionID = @PositionID;

    RETURN 0;
END;