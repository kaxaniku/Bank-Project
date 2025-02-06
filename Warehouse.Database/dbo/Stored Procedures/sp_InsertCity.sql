CREATE PROCEDURE sp_InsertCity
    @Name NVARCHAR(100),
    @CountryID INT,
    @CityID INT OUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Cities(Name, CountryID)
    VALUES (@Name, @CountryID);

    SET @CityID = SCOPE_IDENTITY();

    RETURN 0;
END