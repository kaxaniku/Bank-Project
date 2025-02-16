CREATE PROCEDURE sp_GetSlots
    @SlotID  INT
AS
BEGIN
    SELECT * 
    FROM Slots 
    WHERE SlotID = @SlotID AND IsActive = 1;
END