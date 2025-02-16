CREATE PROCEDURE sp_GetTRansactions
    @TransactionsID  INT
AS
BEGIN
    SELECT * 
    FROM TransactionsID 
    WHERE TransactionsID = @TransactionsID AND IsActive = 1;
END