CREATE PROCEDURE sp_GetPositions
    @PositionsID  int
AS
BEGIN
    SELECT * 
    FROM Positions 
    WHERE PositionID = @PositionsID;
END