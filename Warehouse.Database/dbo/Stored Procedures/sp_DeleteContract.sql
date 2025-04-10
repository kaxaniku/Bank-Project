CREATE PROCEDURE sp_DeleteContract
    @ContractID INT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS(SELECT 1 FROM Contracts WHERE ContractID = @ContractID AND IsActive = 1)
	BEGIN
		RAISERROR('Record was not found', 16, 1);
		RETURN 1;
	END

    UPDATE Contracts
    SET IsActive = 0
    WHERE ContractID = @ContractID;

	RETURN 0;
END