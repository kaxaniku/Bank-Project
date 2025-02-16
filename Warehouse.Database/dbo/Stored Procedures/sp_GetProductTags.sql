CREATE PROCEDURE sp_GetProductTags
    @ProductTagsID  INT
AS
BEGIN
    SELECT * 
    FROM ProductTags
    WHERE ProductTagsID = @ProductTagsID AND IsActive = 1;
END