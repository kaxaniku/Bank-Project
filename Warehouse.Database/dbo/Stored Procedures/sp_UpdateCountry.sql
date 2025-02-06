CREATE PROCEDURE sp_UpdateCountry
	@CountryID INT,
    @Name NVARCHAR(100),
    @ISOCode NVARCHAR(3)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS(SELECT 1 FROM Countries WHERE CountryID = @CountryID AND IsActive = 0)
	BEGIN
		RAISERROR('Record is not active', 16, 1);
		RETURN 1;
	END

	UPDATE Countries
	SET Name = @Name,
	    ISOCode = @ISOCode,
		UpdateDate = GETDATE()
	WHERE CountryID = @CountryID AND (Name != @Name OR ISOCode != @ISOCode);

	RETURN 0;
END;