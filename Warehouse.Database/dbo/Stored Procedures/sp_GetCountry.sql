CREATE PROCEDURE sp_GetCountry
    @CountryID INT
AS
BEGIN
    SELECT * 
    FROM Countries 
    WHERE CountryID = @CountryID AND IsActive = 1;
END