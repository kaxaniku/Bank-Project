CREATE PROCEDURE sp_DeleteEmployee
    @EmployeeID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS(SELECT 1 FROM Employees WHERE EmployeeID = @EmployeeID AND IsActive = 1)
    BEGIN
		RAISERROR('Record was not found', 16, 1);
        RETURN 1;
    END

    UPDATE Employees
    SET IsActive = 0
    WHERE EmployeeID = @EmployeeID;

    RETURN 0;
END
