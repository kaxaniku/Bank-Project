CREATE PROCEDURE sp_GetTags
    @TagID  INT
AS
BEGIN
    SELECT * 
    FROM Tags 
    WHERE TagID = @TagID AND IsActive = 1;
END