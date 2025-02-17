CREATE PROCEDURE sp_GetContract
    @ContractID INT
AS
BEGIN
    SELECT * 
    FROM Contracts 
    WHERE ContractID = @ContractID AND IsActive = 1;
END