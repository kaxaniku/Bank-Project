CREATE PROCEDURE sp_InsertCity
    @Name NVARCHAR(100),
    @PostCode NVARCHAR(20),
    @CountryID INT,
    @CityID INT OUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Cities(Name, PostCode, CountryID)
    VALUES (@Name, @PostCode, @CountryID);

    SET @CityID = SCOPE_IDENTITY();

    RETURN 0;
END