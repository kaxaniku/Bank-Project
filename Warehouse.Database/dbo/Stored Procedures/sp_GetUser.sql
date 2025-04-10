CREATE PROCEDURE sp_GetUser
    @UserID  INT
AS
BEGIN
    SELECT UserID,
           Username,
           null as Password,
           UserRole,
           IsActive
    FROM Users 
    WHERE UserID = @UserID AND IsActive = 1;
END