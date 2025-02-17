CREATE PROCEDURE sp_UpdateUser
    @UserID INT,
	@Username VARCHAR(30),
	@Password VARBINARY(64),
	@UserRole TINYINT
AS
BEGIN
    SET NOCOUNT ON;

	IF EXISTS(SELECT 1 FROM Users WHERE UserID = @UserID AND IsActive = 0)
	BEGIN 
	     RAISERROR('Record is not active', 16, 1)
		 RETURN 1;
	END

	UPDATE Users
	SET Username = @Username,
	    Password = @Password,
		UserRole = @UserRole,
		UpdateDate = GETDATE()
	WHERE 
		UserID = @UserID 
		AND (
			Username != @Username 
			OR Password != @Password 
			OR UserRole != @UserRole
		);

	RETURN 0;
END