CREATE OR ALTER PROCEDURE sp_DeleteCountry
	@CountryID INT
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS(SELECT 1 FROM Countries WHERE CountryID = @CountryID AND IsActive = 0)
	BEGIN
		RAISERROR('Record is already not active', 16, 1);
		RETURN 1;
	END

	UPDATE Countries
	SET IsActive = 0
	WHERE CountryID = @CountryID;

	RETURN 0;
END;