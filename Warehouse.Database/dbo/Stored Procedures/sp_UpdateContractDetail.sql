CREATE PROCEDURE sp_UpdateContractDetail
    @ContractDetailID INT,
    @ContractID INT,
    @SlotID INT,
    @StartDate DATETIME,
    @EndDate DATETIME
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS(SELECT 1 FROM ContractDetails WHERE ContractDetailID = @ContractDetailID)
    BEGIN
        RAISERROR('Record is not active', 16, 1);
        RETURN 1;
    END

    UPDATE ContractDetails
    SET ContractID = @ContractID,
        SlotID = @SlotID,
        StartDate = @StartDate,
        EndDate = @EndDate,
        UpdateDate = GETDATE()
    WHERE ContractID = @ContractID
        AND(SlotID != @SlotID
        OR StartDate != @StartDate
        OR EndDate != @EndDate);

    RETURN 0;
END