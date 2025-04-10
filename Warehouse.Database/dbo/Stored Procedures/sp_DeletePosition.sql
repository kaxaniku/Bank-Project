CREATE PROCEDURE sp_DeletePosition
    @PositionID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS(SELECT 1 FROM Positions WHERE PositionID = @PositionID AND IsActive = 1)
    BEGIN
		RAISERROR('Record was not found', 16, 1);
        RETURN 1;
    END

    UPDATE Positions
    SET IsActive = 0
    WHERE PositionID = @PositionID;

    RETURN 0;
END