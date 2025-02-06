CREATE OR ALTER PROCEDURE sp_InsertSlot
    @StorageID INT,
    @SlotCode VARCHAR(50),
    @SlotID INT OUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Slots(StorageID, SlotCode)
    VALUES (@StorageID, @SlotCode);

    SET @SlotID = SCOPE_IDENTITY();

    RETURN 0;
END;