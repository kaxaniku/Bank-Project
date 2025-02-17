CREATE PROCEDURE sp_DeleteContract
    @ContractID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS(SELECT 1 FROM Contracts WHERE ContractID = @ContractID AND IsActive = 0)
	BEGIN
		RAISERROR('Record is already not active', 16, 1);
		RETURN 1;
	END

    UPDATE Contracts
    SET IsActive = 0
    WHERE ContractID = @ContractID;

	RETURN 0;
END