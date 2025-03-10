CREATE PROCEDURE sp_GetProductTag
    @TagID  INT,
    @ProductID INT
AS
BEGIN
    SELECT * 
    FROM ProductTags
    WHERE TagID = @TagID AND ProductID = @ProductID;
END