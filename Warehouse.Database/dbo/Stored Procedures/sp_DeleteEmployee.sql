CREATE PROCEDURE sp_DeleteEmployee
    @EmployeeID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Employees WHERE EmployeeID = @EmployeeID AND IsActive = 0)
    BEGIN
        RAISERROR('Record is already not active', 16, 1);
        RETURN 1;
    END

    UPDATE Employees
    SET IsActive = 0
    WHERE EmployeeID = @EmployeeID;

    RETURN 0;
END
