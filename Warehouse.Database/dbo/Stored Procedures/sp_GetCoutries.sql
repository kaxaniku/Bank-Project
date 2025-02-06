CREATE OR ALTER PROCEDURE sp_GetCountries
    @CountryID int
AS
BEGIN
    SELECT * 
    FROM Countries 
    WHERE CountryID = @CountryID AND IsActive = 1;
END;