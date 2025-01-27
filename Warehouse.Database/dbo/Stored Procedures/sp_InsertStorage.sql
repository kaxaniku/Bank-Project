CREATE PROCEDURE sp_InsertStorage
    @Name NVARCHAR(100),
    @AddressLine1 NVARCHAR(100),
    @AddressLine2 NVARCHAR(100),
    @CityID INT,
    @Description NVARCHAR(1000),
    @StorageID INT OUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Storages(Name, AddressLine1, AddressLine2, CityID, Description)
    VALUES (@Name, @AddressLine1, @AddressLine2, @CityID, @Description);

    SET @StorageID = SCOPE_IDENTITY();

    RETURN 0;
END
GO