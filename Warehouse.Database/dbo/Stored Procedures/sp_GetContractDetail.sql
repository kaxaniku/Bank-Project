CREATE PROCEDURE sp_GetContractDetail
    @ContractDetailID INT
AS
BEGIN
    SELECT * 
    FROM ContractDetails 
    WHERE ContractDetailID = @ContractDetailID;
END