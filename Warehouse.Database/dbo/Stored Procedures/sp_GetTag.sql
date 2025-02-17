CREATE PROCEDURE sp_GetTag
    @TagID  INT
AS
BEGIN
    SELECT * 
    FROM Tags 
    WHERE TagID = @TagID AND IsActive = 1;
END