CREATE PROCEDURE sp_GetContractDetails
    @ContractDetailID INT
AS
BEGIN
    SELECT * 
    FROM ContractDetails 
    WHERE ContractDetailID = @ContractDetailID;
END