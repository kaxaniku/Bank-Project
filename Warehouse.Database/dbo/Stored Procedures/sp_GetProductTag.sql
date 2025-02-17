CREATE PROCEDURE sp_GetProductTag
    @TagID  INT
AS
BEGIN
    SELECT * 
    FROM ProductTags
    WHERE TagID = @TagID;
END