CREATE OR ALTER PROCEDURE sp_GetTransactions
    @TransactionsID  int
AS
BEGIN
    SELECT * 
    FROM Transactions 
    WHERE TransactionsID = @TransactionsID AND IsActive = 1;
END;