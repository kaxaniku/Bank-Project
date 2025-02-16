CREATE PROCEDURE sp_GetEmployees
    @EmployeeID  INT
AS
BEGIN
    SELECT * 
    FROM  Employees
    WHERE EmployeeID = @EmployeeID AND IsActive = 1;
END