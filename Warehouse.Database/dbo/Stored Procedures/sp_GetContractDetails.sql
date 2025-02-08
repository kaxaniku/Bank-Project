CREATE PROCEDURE sp_GetContractDetails
    @ContractDetailID int
AS
BEGIN
    SELECT * 
    FROM ContractDetails
    WHERE ContractDetailID = @ContractDetailID;
END