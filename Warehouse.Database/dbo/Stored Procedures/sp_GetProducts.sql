CREATE PROCEDURE sp_GetProducts
    @ProductsID  int
AS
BEGIN
    SELECT * 
    FROM Products 
    WHERE ProductID = @ProductsID AND IsActive = 1;
END