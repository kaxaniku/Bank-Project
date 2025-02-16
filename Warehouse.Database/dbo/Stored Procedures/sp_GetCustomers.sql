CREATE PROCEDURE sp_GetCustomers
    @CustomerID INT
AS
BEGIN
    SELECT * 
    FROM Customers
    WHERE CustomerID = @CustomerID AND IsActive = 1;
END