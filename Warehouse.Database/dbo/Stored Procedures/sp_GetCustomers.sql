CREATE PROCEDURE sp_GetCustomers
    @CustomerID INT
AS
BEGIN
    SELECT * 
    FROM Customer 
    WHERE CustomerID = @CustomerID AND IsActive = 1;
END