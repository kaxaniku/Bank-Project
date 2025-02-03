CREATE PROCEDURE sp_GetCity
    @CityID int
AS
BEGIN
    SELECT * 
    FROM Cities 
    WHERE CityID = @CityID AND IsActive = 1;
END