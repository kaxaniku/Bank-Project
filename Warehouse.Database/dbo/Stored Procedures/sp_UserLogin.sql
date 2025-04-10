CREATE PROCEDURE sp_UserLogin
    @Username VARCHAR(30),
    @Password VARCHAR(30)
AS
BEGIN
    DECLARE @UserID INT;
    
    SELECT @UserID = UserID
    FROM Users
    WHERE IsActive = 1 AND Username = @Username AND Password = HASHBYTES('SHA2_256', @Password);
    
    IF (@UserID IS NULL)
    BEGIN
        RETURN -1; 
    END
    RETURN @UserID; 
END