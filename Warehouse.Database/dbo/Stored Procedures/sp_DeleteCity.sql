CREATE PROCEDURE sp_DeleteCity
	@CityID INT
AS
BEGIN
	SET NOCOUNT ON;

	IF EXISTS(SELECT 1 FROM Cities WHERE CityID = @CityID AND IsActive = 0)
	BEGIN
		RAISERROR('Record is already not active', 16, 1);
		RETURN 1;
	END

	UPDATE Cities
	SET IsActive = 0
	WHERE CityID = @CityID;

	RETURN 0;
END