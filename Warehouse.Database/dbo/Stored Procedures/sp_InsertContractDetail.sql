CREATE PROCEDURE sp_InsertContractDetail
    @ContractID INT,
    @SlotID INT,
    @StartDate DATETIME,
    @EndDate DATETIME,
    @ContractDetailID INT OUT
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO ContractDetails (ContractID, SlotID, StartDate, EndDate)
    VALUES (@ContractID, @SlotID, @StartDate, @EndDate);

    SET @ContractDetailID = SCOPE_IDENTITY();

    RETURN 0;
END;