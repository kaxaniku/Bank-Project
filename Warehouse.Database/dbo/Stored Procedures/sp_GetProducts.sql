CREATE PROCEDURE sp_GetProducts
    @ProductID  INT
AS
BEGIN
    SELECT * 
    FROM Products 
    WHERE ProductID = @ProductID AND IsActive = 1;
END