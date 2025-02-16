CREATE PROCEDURE sp_GetCategories
    @CategoryID INT
AS
BEGIN
    SELECT * 
    FROM Categories 
    WHERE CategoryID = @CategoryID AND IsActive = 1;
END