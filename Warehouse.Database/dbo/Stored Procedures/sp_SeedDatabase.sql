CREATE PROCEDURE sp_SeedDatabase
AS
BEGIN
    SET NOCOUNT ON;
    
    -- First, clear the database
    EXEC sp_ClearDatabase;

    -- Insert Countries
    DECLARE @CountryID1 INT, @CountryID2 INT, @CountryID3 INT;
    EXEC sp_InsertCountry 'USA', 'US', @CountryID1 OUTPUT;    -- CountryID = 1
    EXEC sp_InsertCountry 'Canada', 'CA', @CountryID2 OUTPUT; -- CountryID = 2
    EXEC sp_InsertCountry 'Germany', 'GER', @CountryID3 OUTPUT; -- CountryID = 3
    
    -- Insert Cities
    DECLARE @CityID1 INT, @CityID2 INT, @CityID3 INT, @CityID4 INT, @CityID5 INT;
    EXEC sp_InsertCity 'New York', '10001', @CountryID1, @CityID1 OUTPUT;      -- CityID = 1
    EXEC sp_InsertCity 'Los Angeles', '90001', @CountryID1, @CityID2 OUTPUT;   -- CityID = 2
    EXEC sp_InsertCity 'Toronto', 'M5A 1A1', @CountryID2, @CityID3 OUTPUT;     -- CityID = 3
    EXEC sp_InsertCity 'Vancouver', 'V5K 0A1', @CountryID2, @CityID4 OUTPUT;   -- CityID = 4
    EXEC sp_InsertCity 'Metzingen', '000001', @CountryID3, @CityID5 OUTPUT;   -- CityID = 5
    
    -- Insert Categories
    DECLARE @CategoryID1 INT, @CategoryID2 INT, @CategoryID3 INT, @CategoryID4 INT, @CategoryID5 INT;
    EXEC sp_InsertCategory 'Electronics', NULL, @CategoryID1 OUTPUT;  -- CategoryID = 1
    EXEC sp_InsertCategory 'Clothing', NULL, @CategoryID2 OUTPUT;     -- CategoryID = 2
    EXEC sp_InsertCategory 'Books', NULL, @CategoryID3 OUTPUT;        -- CategoryID = 3
    EXEC sp_InsertCategory 'Toys', NULL, @CategoryID4 OUTPUT;         -- CategoryID = 4
    EXEC sp_InsertCategory 'Furniture', NULL, @CategoryID5 OUTPUT;    -- CategoryID = 5
    
    -- Insert Products
    DECLARE @ProductID1 INT, @ProductID2 INT, @ProductID3 INT;
    EXEC sp_InsertProduct @CategoryID1, @ProductID1 OUTPUT, 'LP1000', 'Laptop', 'High performance laptop', '15-inch screen, 1.8kg', 1.8; -- ProductID = 1
    EXEC sp_InsertProduct @CategoryID1, @ProductID2 OUTPUT, 'SP500', 'Smartphone', 'Latest smartphone', '6-inch screen, 0.2kg', 0.2;     -- ProductID = 2
    EXEC sp_InsertProduct @CategoryID5, @ProductID3 OUTPUT, 'TSHIRT20', 'T-shirt', 'Comfortable cotton t-shirt', 'Medium size', 0.3;     -- ProductID = 3

    -- Insert Positions
    DECLARE @PositionID1 INT, @PositionID2 INT, @PositionID3 INT, @PositionID4 INT, @PositionID5 INT;
    
    EXEC sp_InsertPosition 'Manager', 'Manages operations', 50000, @PositionID1 OUTPUT;      -- PositionID = 1
    EXEC sp_InsertPosition 'Clerk', 'Handles sales', 30000, @PositionID2 OUTPUT;            -- PositionID = 2
    EXEC sp_InsertPosition 'Security', 'Responsible for security', 40000, @PositionID3 OUTPUT; -- PositionID = 3
    EXEC sp_InsertPosition 'Technician', 'Maintains equipment and IT infrastructure', 45000, @PositionID4 OUTPUT; -- PositionID = 4
    EXEC sp_InsertPosition 'HR Specialist', 'Handles employee relations and recruitment', 47000, @PositionID5 OUTPUT; -- PositionID = 5

    
    -- Insert Employees
    DECLARE @EmployeeID1 INT, @EmployeeID2 INT, @EmployeeID3 INT, @EmployeeID4 INT, @EmployeeID5 INT;
    
    EXEC sp_InsertEmployee 
         'John', 'Doe', 'john@example.com', 
         '123 Street', NULL, 
         @CityID1, '555-1234', 
         '1985-05-10', '2010-06-15', 
         @PositionID1, NULL, 
         @EmployeeID1 OUTPUT;   -- EmployeeID = 1
    
    EXEC sp_InsertEmployee 
         'Jane', 'Smith', 'jane@example.com', 
         '456 Avenue', NULL, 
         @CityID2, '555-5678', 
         '1990-07-20', '2015-09-30', 
         @PositionID2, @EmployeeID1, 
         @EmployeeID2 OUTPUT;   -- EmployeeID = 2
    
    EXEC sp_InsertEmployee 
         'Reinhard', 'Heydrich', 'Reinhard@example.com', 
         'Niederkirchnerstraße 8', NULL, 
         @CityID3, '555-1357', 
         '1904-03-07', '2013-03-20', 
         @PositionID3, @EmployeeID1, 
         @EmployeeID3 OUTPUT;   -- EmployeeID = 3
    
    EXEC sp_InsertEmployee 
         'Alice', 'Johnson', 'alice@example.com', 
         '789 Boulevard', NULL, 
         @CityID4, '555-2468', 
         '1995-12-05', '2020-01-15', 
         @PositionID4, @EmployeeID2, 
         @EmployeeID4 OUTPUT;   -- EmployeeID = 4
    
    EXEC sp_InsertEmployee 
         'Michael', 'Brown', 'michael@example.com', 
         '159 Park Lane', NULL, 
         @CityID5, '555-9876', 
         '1988-11-22', '2012-07-08', 
         @PositionID5, @EmployeeID3, 
         @EmployeeID5 OUTPUT;   -- EmployeeID = 5

    
    -- Insert Customers
    DECLARE @CustomerID1 INT, @CustomerID2 INT, @CustomerID3 INT;
    EXEC sp_InsertCustomer 'Acme Corp', '789 Road', NULL, @CityID1, '555-9999', 'acme@example.com', @CustomerID1 OUTPUT;         -- CustomerID = 1
    EXEC sp_InsertCustomer 'Global Inc', '321 Boulevard', NULL, @CityID2, '555-8888', 'global@example.com', @CustomerID2 OUTPUT; -- CustomerID = 2
    EXEC sp_InsertCustomer 'Hugo Boss', 'Holy-Allee 3', NULL, @CityID3, '49 7123 94-0', 'hugo@example.com', @CustomerID3 OUTPUT; -- CustomerID = 3
    
    -- Insert Contracts
    DECLARE @ContractID1 INT, @ContractID2 INT, @ContractID3 INT;
    EXEC sp_InsertContract 'Contract A', 'Details of contract A', @CustomerID1, @EmployeeID1, 1000, @ContractID1 OUTPUT;   -- ContractID = 1
    EXEC sp_InsertContract 'Contract B', 'Details of contract B', @CustomerID2, @EmployeeID2, 2000, @ContractID2 OUTPUT;   -- ContractID = 2
    EXEC sp_InsertContract 'Contract S', 'Details of contract S', @CustomerID3, @EmployeeID3, 3000, @ContractID3 OUTPUT;   -- ContractID = 3
    
    -- Insert Storages
    DECLARE @StorageID1 INT, @StorageID2 INT, @StorageID3 INT;
    EXEC sp_InsertStorage 'Main Warehouse', '100 Warehouse St', NULL, @CityID1, 'Primary storage facility', @StorageID1 OUTPUT;  -- StorageID = 1
    EXEC sp_InsertStorage 'Secondary Warehouse', '200 Warehouse Ave', NULL, @CityID2, 'Secondary storage facility', @StorageID2 OUTPUT;  -- StorageID = 2
    EXEC sp_InsertStorage 'Special Warehouse', 'Więźniów Oświęcimia 55 Street, Oświęcim', NULL, @CityID3, 'Special storage facility', @StorageID3 OUTPUT;  -- StorageID = 3
    
    -- Insert Slots
    DECLARE @SlotID1 INT, @SlotID2 INT, @SlotID3 INT;
    EXEC sp_InsertSlot @StorageID1, 'A1', @SlotID1 OUTPUT;   -- SlotID = 1
    EXEC sp_InsertSlot @StorageID1, 'B2', @SlotID2 OUTPUT;   -- SlotID = 2
    EXEC sp_InsertSlot @StorageID3, 'C3', @SlotID3 OUTPUT;   -- SlotID = 3
    
    -- Insert ContractDetails
    DECLARE @ContractDetailID1 INT, @ContractDetailID2 INT;
    EXEC sp_InsertContractDetail @ContractID1, @SlotID1, '2024-01-01', '2024-12-31', @ContractDetailID1 OUTPUT;  -- ContractDetailID = 1
    EXEC sp_InsertContractDetail @ContractID2, @SlotID2, '2025-01-01', '2025-12-31', @ContractDetailID2 OUTPUT;  -- ContractDetailID = 2
    
    -- Insert Transactions
    DECLARE @TransactionID1 INT, @TransactionID2 INT;
    EXEC sp_InsertTransaction @ContractID1, @EmployeeID1, @ProductID1, @SlotID1, 10, 'Import', NULL, @TransactionID1 OUTPUT;  -- TransactionID = 1
    EXEC sp_InsertTransaction @ContractID2, @EmployeeID2, @ProductID2, @SlotID2, 5, 'Export', NULL, @TransactionID2 OUTPUT;   -- TransactionID = 2
    
    -- Insert Users
    DECLARE @UserID1 INT, @UserID2 INT, @UserID3 INT;
    EXEC sp_InsertUser @EmployeeID1, 'admin', 'admin123', 1, @UserID1 OUTPUT;    -- Creates a user for EmployeeID1
    EXEC sp_InsertUser @EmployeeID2, 'operator', 'operator123', 2, @UserID2 OUTPUT; -- Creates a user for EmployeeID2
    EXEC sp_InsertUser @EmployeeID3, 'moderator', 'moderator123', 3, @UserID3 OUTPUT; -- Creates a user for EmployeeID3

    -- Insert Tags
    DECLARE @TagID1 INT, @TagID2 INT, @TagID3 INT, @TagID4 INT, @TagID5 INT;
    EXEC sp_InsertTag 'Portable', 'Portable devices', @TagID1 OUTPUT;       -- TagID = 1
    EXEC sp_InsertTag 'Fashion', 'Fashionable items', @TagID2 OUTPUT;       -- TagID = 2
    EXEC sp_InsertTag 'Education', 'Educational materials', @TagID3 OUTPUT; -- TagID = 3
    EXEC sp_InsertTag 'Entertainment', 'Fun toys and games', @TagID4 OUTPUT;-- TagID = 4
    EXEC sp_InsertTag 'Home', 'Home and furniture', @TagID5 OUTPUT;         -- TagID = 5

    -- Insert ProductTags
    EXEC sp_InsertProductTag @TagID1, @ProductID1;  -- Assign TagID 1 (Portable) to ProductID 1 (Laptop)
    EXEC sp_InsertProductTag @TagID1, @ProductID2;  -- Assign TagID 1 (Portable) to ProductID 2 (Smartphone)
    EXEC sp_InsertProductTag @TagID2, @ProductID3;  -- Assign TagID 2 (Fashion) to ProductID 3 (T-shirt)
    
    RETURN 0;
END
