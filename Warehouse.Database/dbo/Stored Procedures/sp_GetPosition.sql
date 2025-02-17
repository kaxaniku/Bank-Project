CREATE PROCEDURE sp_GetPosition
    @PositionID  INT
AS
BEGIN
    SELECT * 
    FROM Positions 
    WHERE PositionID = @PositionID AND IsActive = 1;
END