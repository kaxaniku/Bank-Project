CREATE PROCEDURE sp_GetTRansactions
    @TransactionsID  int
AS
BEGIN
    SELECT * 
    FROM TransactionsID 
    WHERE TransactionsID = @TransactionsID AND IsActive = 1;
END