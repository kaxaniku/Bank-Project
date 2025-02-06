CREATE PROCEDURE sp_DeleteStorage
    @StorageID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM Storages WHERE StorageID = @StorageID AND IsActive = 0)
    BEGIN
        RAISERROR('Storage is already not active.', 16, 1);
        RETURN 1;
    END

    UPDATE Storages
    SET IsActive = 0
    WHERE StorageID = @StorageID;

    RETURN 0;
END