CREATE PROCEDURE sp_GetSlots
    @SlotsID  INT
AS
BEGIN
    SELECT * 
    FROM Slots 
    WHERE SlotsID = @SlotsID AND IsActive = 1;
END