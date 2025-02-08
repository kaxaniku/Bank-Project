CREATE PROCEDURE sp_GetTRansactions
    @TransactionsID  int
AS
BEGIN
    SELECT * 
    FROM Transactions
    WHERE TransactionID = @TransactionsID;
END