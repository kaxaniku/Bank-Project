CREATE PROCEDURE sp_InsertUser
    @EmployeeID INT,
    @Username VARCHAR(30),
    @Password VARCHAR(30),
    @UserRole TINYINT,
    @UserID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Users(UserID, Username, Password, UserRole)
    VALUES(@EmployeeID, @Username, HASHBYTES('SHA2_256',@Password), @UserRole);

    SET @UserID = @EmployeeID;

    RETURN 0;
END
