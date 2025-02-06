CREATE OR ALTER PROCEDURE sp_GetStorages
    @StoragesID  int
AS
BEGIN
    SELECT * 
    FROM Storages 
    WHERE StoragesID = @StoragesID AND IsActive = 1;
END;