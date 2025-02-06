CREATE PROCEDURE sp_UpdateStorage
    @StorageID INT,
    @Name NVARCHAR(100),
    @AddressLine1 NVARCHAR(100),
    @AddressLine2 NVARCHAR(100),
    @CityID INT,
    @Description NVARCHAR(1000)
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS(SELECT 1 FROM Storages WHERE StorageID = @StorageID AND IsActive = 0)
    BEGIN
        RAISERROR('Cannot update an inactive storage.', 16, 1);
        RETURN 1;
    END

    UPDATE Storages
    SET Name = @Name,
        AddressLine1 = @AddressLine1,
        AddressLine2 = @AddressLine2,
        CityID = @CityID,
        Description = @Description,
        UpdateDate = GETDATE()
    WHERE StorageID = @StorageID 
      AND (Name != @Name OR AddressLine1 != @AddressLine1 OR AddressLine2 != @AddressLine2 OR CityID != @CityID OR Description != @Description);

    RETURN 0;
END