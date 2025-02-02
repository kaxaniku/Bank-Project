CREATE OR ALTER PROCEDURE sp_UpdateEmployee
    @EmployeeID INT,
    @FirstName NVARCHAR(50),
    @LastName NVARCHAR(50),
    @PositionID INT,
    @PhoneNumber NVARCHAR(24),
    @Email NVARCHAR(50),
    @AddressLine1 NVARCHAR(100),
    @CityID INT,
    @HireDate DATE,
    @BirthDate DATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (SELECT 1 FROM Employees WHERE EmployeeID = @EmployeeID AND IsActive = 0)
    BEGIN
        RAISERROR('Record is not active', 16, 1);
        RETURN 1;
    END

    UPDATE Employees
    SET 
        FirstName = @FirstName,
        LastName = @LastName,
        PositionID = @PositionID,
        PhoneNumber = @PhoneNumber,
        Email = @Email,
        AddressLine1 = @AddressLine1,
        CityID = @CityID,
        HireDate = @HireDate,
        BirthDate = @BirthDate,
        UpdateDate = GETDATE()
    WHERE 
        EmployeeID = @EmployeeID 
        AND (FirstName != @FirstName OR LastName != @LastName OR PositionID != @PositionID OR 
             PhoneNumber != @PhoneNumber OR Email != @Email OR AddressLine1 != @AddressLine1 OR 
             CityID != @CityID OR HireDate != @HireDate OR BirthDate != @BirthDate);

    RETURN 0;
END;
