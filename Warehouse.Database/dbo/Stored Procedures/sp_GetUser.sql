CREATE PROCEDURE sp_GetUser
    @UserID  INT
AS
BEGIN
    SELECT * 
    FROM Users 
    WHERE UserID = @UserID AND IsActive = 1;
END