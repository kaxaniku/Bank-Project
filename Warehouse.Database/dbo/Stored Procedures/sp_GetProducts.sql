CREATE PROCEDURE sp_GetProducts
    @ProductsID  int
AS
BEGIN
    SELECT * 
    FROM Products 
    WHERE ProductsID = @ProductsID AND IsActive = 1;
END