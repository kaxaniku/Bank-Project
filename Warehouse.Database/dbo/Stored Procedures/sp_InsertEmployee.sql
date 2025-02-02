	CREATE OR ALTER PROCEDURE sp_InsertEmployee
	    @FirstName NVARCHAR(50),
	    @LastName NVARCHAR(50),
		@Email NVARCHAR(50),
		@AddressLine1 NVARCHAR(100),
		@CityID INT,
		@PhoneNumber NVARCHAR(24),
		@BirthDate DATE,
		@HireDate DATE,
	    @PositionID INT,
		@ReportsTo INT,
	    @EmployeeID INT OUT
	AS
	BEGIN
	    SET NOCOUNT ON;
	
	    INSERT INTO Employees (FirstName, LastName, Email, AddressLine1, CityID, PhoneNumber, BirthDate, HireDate, PositionID, ReportsTo)
	    VALUES (@FirstName, @LastName, @Email, @AddressLine1, @CityID, @PhoneNumber, @BirthDate, @HireDate, @PositionID, @ReportsTo);
	
	    SET @EmployeeID = SCOPE_IDENTITY();
	
	    RETURN 0;
	END;