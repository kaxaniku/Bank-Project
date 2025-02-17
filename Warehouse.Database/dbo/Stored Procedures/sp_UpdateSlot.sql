CREATE PROCEDURE sp_UpdateSlot
    @SlotID INT,
	@StorageID INT,
    @SlotCode VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS(SELECT 1 FROM Slots WHERE SlotID = @SlotID AND IsActive = 0)
	BEGIN		
		RAISERROR('Record is not active', 16, 1);
		RETURN 1;
	END

	UPDATE Slots
	SET SlotCode = @SlotCode,
		StorageID = @StorageID,
		UpdateDate = GETDATE()
	WHERE SlotID = @SlotID AND (SlotCode != @SlotCode OR StorageID != @StorageID);

	RETURN 0;
END