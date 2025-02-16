CREATE PROCEDURE sp_GetTags
    @TagsID  INT
AS
BEGIN
    SELECT * 
    FROM Tags 
    WHERE TagsID = @TagsID AND IsActive = 1;
END