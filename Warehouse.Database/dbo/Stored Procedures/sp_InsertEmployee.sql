CREATE PROCEDURE sp_InsertEmployee
    @FirstName NVARCHAR(15),
    @LastName NVARCHAR(20),
	@Email VARCHAR(50),
	@AddressLine1 NVARCHAR(100),
	@AddressLine2 NVARCHAR(100) = NULL,
	@CityID INT,
	@PhoneNumber VARCHAR(24),
	@BirthDate DATE,
	@HireDate DATE,
    @PositionID INT,
	@ReportsTo INT = NULL,
    @EmployeeID INT OUT
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Employees (FirstName, LastName, Email, AddressLine1, AddressLine2, CityID, PhoneNumber, BirthDate, HireDate, PositionID, ReportsTo)
    VALUES (@FirstName, @LastName, @Email, @AddressLine1, @AddressLine2, @CityID, @PhoneNumber, @BirthDate, @HireDate, @PositionID, @ReportsTo);

    SET @EmployeeID = SCOPE_IDENTITY();

    RETURN 0;
END