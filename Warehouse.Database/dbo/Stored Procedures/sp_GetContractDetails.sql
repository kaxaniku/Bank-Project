CREATE PROCEDURE sp_GetContractDetails
    @ContractDetailsID INT
AS
BEGIN
    SELECT * 
    FROM ContractDetails 
    WHERE ContractDetailsID = @ContractDetailsID AND IsActive = 1;
END