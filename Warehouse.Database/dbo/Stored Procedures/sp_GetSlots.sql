CREATE OR ALTER PROCEDURE sp_GetSlots
    @SlotsID  int
AS
BEGIN
    SELECT * 
    FROM Slots 
    WHERE SlotsID = @SlotsID AND IsActive = 1;
END;