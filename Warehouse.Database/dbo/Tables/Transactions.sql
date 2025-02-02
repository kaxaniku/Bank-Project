CREATE TABLE Transactions(
    TransactionID INT PRIMARY KEY IDENTITY(1,1),
    ContractID INT NOT NULL REFERENCES Contracts(ContractID),
    EmployeeID INT NOT NULL REFERENCES Employees(EmployeeID),
    ProductID INT NOT NULL REFERENCES Products(ProductID),
    SlotID INT NOT NULL REFERENCES Slots(SlotID),
    Quantity INT NOT NULL CHECK (Quantity > 0),
    TransactionType NVARCHAR(50) NOT NULL CHECK (TransactionType IN ('Import', 'Export')),
    TransactionDate DATETIME NOT NULL DEFAULT GETDATE(),
	CustomerAgent NVARCHAR(100) NULL
);