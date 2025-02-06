CREATE OR ALTER PROCEDURE sp_GetTags
    @TagsID  int
AS
BEGIN
    SELECT * 
    FROM Tags 
    WHERE TagsID = @TagsID AND IsActive = 1;
END;