CREATE PROCEDURE sp_DeleteUser
    @UserID INT
AS
BEGIN
     SET NOCOUNT ON;

	 IF NOT EXISTS(SELECT 1 FROM Users WHERE UserID = @UserID AND IsActive = 1)
	 BEGIN
		RAISERROR('Record was not found', 16, 1);
		  RETURN 1;
	 END

	 UPDATE Users
	 SET IsActive = 0
	 WHERE UserID = @UserID

	 RETURN 0;
END