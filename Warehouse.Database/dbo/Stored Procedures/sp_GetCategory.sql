CREATE PROCEDURE sp_GetCategory
    @CategoryID INT
AS
BEGIN
    SELECT * 
    FROM Categories 
    WHERE CategoryID = @CategoryID AND IsActive = 1;
END