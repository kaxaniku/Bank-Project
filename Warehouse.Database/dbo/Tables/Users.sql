CREATE TABLE [dbo].[Users]
(
    UserID INT NOT NULL PRIMARY KEY REFERENCES Employees(EmployeeID),
    Username VARCHAR(30) NOT NULL UNIQUE,
    Password VARBINARY(64) NOT NULL,
    UserRole TINYINT NOT NULL CHECK(UserRole IN (1, 2, 3)), -- 1: Admin, 2: Operator, 3: Accountant
    IsActive BIT NOT NULL DEFAULT 1,
    CreateDate DATETIME NOT NULL DEFAULT(GETDATE()),
    UpdateDate DATETIME NULL,
)
