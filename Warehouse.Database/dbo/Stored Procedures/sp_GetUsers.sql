CREATE PROCEDURE sp_GetUsers
    @UserID  int
AS
BEGIN
    SELECT * 
    FROM Users 
    WHERE UserID = @UserID AND IsActive = 1;
END