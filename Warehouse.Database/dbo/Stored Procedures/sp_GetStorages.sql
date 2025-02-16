CREATE PROCEDURE sp_GetStorages
    @StorageID  INT
AS
BEGIN
    SELECT * 
    FROM Storages 
    WHERE StorageID = @StorageID AND IsActive = 1;
END