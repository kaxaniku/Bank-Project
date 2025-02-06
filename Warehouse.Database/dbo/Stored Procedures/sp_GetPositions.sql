CREATE OR ALTER PROCEDURE sp_GetPositions
    @PositionsID  int
AS
BEGIN
    SELECT * 
    FROM Positions 
    WHERE PositionsID = @PositionsID AND IsActive = 1;
END;