CREATE PROCEDURE sp_InsertTransaction
    @ContractID INT,
    @EmployeeID INT,
    @ProductID INT,
    @SlotID INT,
    @Quantity INT,
    @TransactionType NVARCHAR(50),
    @CustomerAgent NVARCHAR(100) NULL,
    @TransactionID INT OUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Transactions(ContractID, EmployeeID, ProductID, SlotID, Quantity, TransactionType, CustomerAgent)
    VALUES (@ContractID, @EmployeeID, @ProductID, @SlotID, @Quantity, @TransactionType, @CustomerAgent);

    SET @TransactionID = SCOPE_IDENTITY();

    RETURN 0;
END