CREATE PROCEDURE sp_GetEmployees
    @EmployeeID  int
AS
BEGIN
    SELECT * 
    FROM Employees
    WHERE EmployeeID = @EmployeeID AND IsActive = 1;
END