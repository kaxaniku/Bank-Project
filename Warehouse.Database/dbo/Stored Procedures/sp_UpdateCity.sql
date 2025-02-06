CREATE PROCEDURE sp_UpdateCity
	@CityID INT,
	@CountryID INT,
    @Name NVARCHAR(100),
    @PostCode NVARCHAR(20)
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS(SELECT 1 FROM Cities WHERE CityID = @CityID AND IsActive = 0)
	BEGIN
		RAISERROR('Record is not active', 16, 1);
		RETURN 1;
	END

	UPDATE Cities
	SET CountryID = @CountryID,
		Name = @Name,
	    PostCode = @PostCode,
		UpdateDate = GETDATE()
	WHERE CityID = @CityID AND (Name != @Name OR PostCode != @PostCode OR CityID != @CityID);

	RETURN 0;
END