CREATE PROCEDURE sp_GetProductTags
    @ProductTagsID  int
AS
BEGIN
    SELECT * 
    FROM Products
    WHERE ProductID = @ProductTagsID;
END