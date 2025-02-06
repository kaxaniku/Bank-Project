CREATE OR ALTER PROCEDURE sp_DeleteUser
    @UserID INT
AS
BEGIN
     SET NOCOUNT ON;

	 IF EXISTS(SELECT 1 FROM Users WHERE UserID = @UserID AND IsActive = 0)
	 BEGIN
	      RAISERROR('Record is already not active', 16, 1)
		  RETURN 1;
	 END

	 UPDATE Users
	 SET IsActive = 0
	 WHERE UserID = @UserID

	 RETURN 0;
END;