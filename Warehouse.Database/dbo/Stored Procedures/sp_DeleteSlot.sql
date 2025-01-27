CREATE PROCEDURE sp_DeleteSlot
	@SlotID INT
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS(SELECT 1 FROM Slots WHERE SlotID = @SlotID AND IsActive = 0)
	BEGIN
		RAISERROR('Record is already not active', 16, 1);
		RETURN 1;
	END

	UPDATE Slots
	Set IsActive = 0
	WHERE SlotID = @SlotID;

	Return 0;
END
