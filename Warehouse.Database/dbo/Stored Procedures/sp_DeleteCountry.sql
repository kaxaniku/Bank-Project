CREATE PROCEDURE sp_DeleteCountry
	@CountryID INT
AS
BEGIN
	SET NOCOUNT ON;

	IF NOT EXISTS(SELECT 1 FROM Countries WHERE CountryID = @CountryID AND IsActive = 1)
	BEGIN
		RAISERROR('Record was not found', 16, 1);
		RETURN 1;
	END

	UPDATE Countries
	SET IsActive = 0
	WHERE CountryID = @CountryID;

	RETURN 0;
END