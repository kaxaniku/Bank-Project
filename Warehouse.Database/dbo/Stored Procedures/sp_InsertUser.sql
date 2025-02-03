CREATE PROCEDURE sp_InsertUser
    @UserName VARCHAR(30),
	@Password VARBINARY(64),
	@UserRole TINYINT,
	@UserID INT OUT
AS
BEGIN
     SET NOCOUNT ON;

	 INSERT INTO Users(Username, Password, UserRole)
	 VALUES(@UserName, @Password, @UserRole)

	 SET @UserID = SCOPE_IDENTITY()
     RETURN 0;
END
