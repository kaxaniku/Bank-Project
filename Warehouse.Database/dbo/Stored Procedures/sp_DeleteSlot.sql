CREATE PROCEDURE sp_DeleteSlot
	@SlotID INT
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS(SELECT 1 FROM Slots WHERE SlotID = @SlotID AND IsActive = 1)
	BEGIN
		RAISERROR('Record was not found', 16, 1);
		RETURN 1;
	END

	UPDATE Slots
	Set IsActive = 0
	WHERE SlotID = @SlotID;

	Return 0;
END