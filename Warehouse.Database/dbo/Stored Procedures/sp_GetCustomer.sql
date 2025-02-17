CREATE PROCEDURE sp_GetCustomer
    @CustomerID INT
AS
BEGIN
    SELECT * 
    FROM Customers
    WHERE CustomerID = @CustomerID AND IsActive = 1;
END