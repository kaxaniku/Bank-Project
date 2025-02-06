CREATE PROCEDURE sp_GetEmployees
    @EmployesID  int
AS
BEGIN
    SELECT * 
    FROM  Employees
    WHERE CustomerID = @CustomerID AND IsActive = 1;
END