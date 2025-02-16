CREATE PROCEDURE sp_GetTRansactions
    @TransactionID  INT
AS
BEGIN
    SELECT * 
    FROM Transactions 
    WHERE TransactionID = @TransactionID;
END