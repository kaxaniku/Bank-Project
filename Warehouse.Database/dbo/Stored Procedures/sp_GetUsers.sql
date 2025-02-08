CREATE PROCEDURE sp_GetUsers
    @UsersID  int
AS
BEGIN
    SELECT * 
    FROM Users 
    WHERE UsersID = @UsersID AND IsActive = 1;
END