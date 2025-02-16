CREATE PROCEDURE sp_GetPositions
    @PositionsID  INT
AS
BEGIN
    SELECT * 
    FROM Positions 
    WHERE PositionsID = @PositionsID AND IsActive = 1;
END