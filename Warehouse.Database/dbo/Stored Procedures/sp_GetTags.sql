CREATE PROCEDURE sp_GetTags
    @TagID  int
AS
BEGIN
    SELECT * 
    FROM Tags
    WHERE TagID = @TagID AND IsActive = 1;
END