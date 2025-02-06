CREATE PROCEDURE sp_DeleteCustomer
    @CustomerID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Customers WHERE CustomerID = @CustomerID AND IsActive = 0)
    BEGIN
        RAISERROR('Record is already not active', 16, 1);
        RETURN 1;
    END

    UPDATE Customers
    SET IsActive = 0
    WHERE CustomerID = @CustomerID;

    RETURN 0;
END