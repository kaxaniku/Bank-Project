CREATE PROCEDURE sp_GetUsers
    @UserID  INT
AS
BEGIN
    SELECT * 
    FROM Users 
    WHERE UserID = @UserID AND IsActive = 1;
END