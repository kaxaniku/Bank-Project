CREATE PROCEDURE sp_GetTRansaction
    @TransactionID  INT
AS
BEGIN
    SELECT * 
    FROM Transactions 
    WHERE TransactionID = @TransactionID;
END