CREATE TABLE [dbo].[ContractDetails]
(
    ContractDetailID INT PRIMARY KEY IDENTITY(1,1),
    ContractID INT NOT NULL REFERENCES Contracts(ContractID),
    SlotID INT NOT NULL REFERENCES Slots(SlotID),
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
)
