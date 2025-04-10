CREATE PROCEDURE sp_DeleteStorage
    @StorageID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS(SELECT 1 FROM Storages WHERE StorageID = @StorageID AND IsActive = 1)
    BEGIN
		RAISERROR('Record was not found', 16, 1);
        RETURN 1;
    END

    UPDATE Storages
    SET IsActive = 0
    WHERE StorageID = @StorageID;

    RETURN 0;
END