CREATE PROCEDURE sp_InsertPosition
    @Name NVARCHAR(50),
    @Description NVARCHAR(1000),
    @Salary money,
    @PositionID INT OUT
AS
BEGIN
    SET NOCOUNT ON;
	
    INSERT INTO Positions (Name, Description, Salary)
    VALUES (@Name, @Description, @Salary);

    SET @PositionID = SCOPE_IDENTITY();

    RETURN 0;
END