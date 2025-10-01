CREATE PROCEDURE sp_GetProduct
    @ProductID INT
AS
BEGIN
    SELECT * 
    FROM Products 
    WHERE ProductID = @ProductID;
END