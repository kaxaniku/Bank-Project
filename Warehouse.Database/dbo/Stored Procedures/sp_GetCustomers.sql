CREATE PROCEDURE sp_GetCustomers
    @CustomerID int
AS
BEGIN
    SELECT * 
    FROM Customers
    WHERE CustomerID = @CustomerID AND IsActive = 1;
END