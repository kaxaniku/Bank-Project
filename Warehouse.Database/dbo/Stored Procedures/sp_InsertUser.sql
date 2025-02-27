CREATE PROCEDURE sp_InsertUser
    @EmployeeID INT,
    @UserName VARCHAR(30),
    @Password VARBINARY(64),
    @UserRole TINYINT,
    @UserID INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Users(UserID, Username, Password, UserRole)
    VALUES(@EmployeeID, @UserName, @Password, @UserRole);

    SET @UserID = @EmployeeID;

    RETURN 0;
END
