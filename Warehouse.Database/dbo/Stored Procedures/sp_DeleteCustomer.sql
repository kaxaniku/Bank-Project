CREATE PROCEDURE sp_DeleteCustomer
    @CustomerID INT
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS(SELECT 1 FROM Customers WHERE CustomerID = @CustomerID AND IsActive = 1)
    BEGIN
		RAISERROR('Record was not found', 16, 1);

        RETURN 1;
    END

    UPDATE Customers
    SET IsActive = 0
    WHERE CustomerID = @CustomerID;

    RETURN 0;
END