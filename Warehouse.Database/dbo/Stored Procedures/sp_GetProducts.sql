CREATE PROCEDURE sp_GetProducts
    @ProductsID  INT
AS
BEGIN
    SELECT * 
    FROM Products 
    WHERE ProductsID = @ProductsID AND IsActive = 1;
END