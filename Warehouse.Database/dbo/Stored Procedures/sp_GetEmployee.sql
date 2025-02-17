CREATE PROCEDURE sp_GetEmployee
    @EmployeeID  INT
AS
BEGIN
    SELECT * 
    FROM  Employees
    WHERE EmployeeID = @EmployeeID AND IsActive = 1;
END