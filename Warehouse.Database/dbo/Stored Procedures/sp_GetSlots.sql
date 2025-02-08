CREATE PROCEDURE sp_GetSlots
    @SlotID  int
AS
BEGIN
    SELECT * 
    FROM Slots 
    WHERE SlotID = @SlotID AND IsActive = 1;
END