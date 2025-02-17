CREATE PROCEDURE sp_GetStorage
    @StorageID  INT
AS
BEGIN
    SELECT * 
    FROM Storages 
    WHERE StorageID = @StorageID AND IsActive = 1;
END