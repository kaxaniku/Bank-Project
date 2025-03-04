CREATE PROCEDURE sp_SeedDatabase
AS
BEGIN
    SET NOCOUNT ON;
    
    -- First, clear the database
    EXEC sp_ClearDatabase;

    -- Insert Countries
    DECLARE @CountryID1 INT, @CountryID2 INT;
    EXEC sp_InsertCountry 'USA', 'US', @CountryID1 OUTPUT;    -- CountryID = 1
    EXEC sp_InsertCountry 'Canada', 'CA', @CountryID2 OUTPUT; -- CountryID = 2
    
    -- Insert Cities
    DECLARE @CityID1 INT, @CityID2 INT, @CityID3 INT, @CityID4 INT;
    EXEC sp_InsertCity 'New York', '10001', @CountryID1, @CityID1 OUTPUT;      -- CityID = 1
    EXEC sp_InsertCity 'Los Angeles', '90001', @CountryID1, @CityID2 OUTPUT;   -- CityID = 2
    EXEC sp_InsertCity 'Toronto', 'M5A 1A1', @CountryID2, @CityID3 OUTPUT;     -- CityID = 3
    EXEC sp_InsertCity 'Vancouver', 'V5K 0A1', @CountryID2, @CityID4 OUTPUT;   -- CityID = 4
    
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
    EXEC sp_InsertProduct @CategoryID2, @ProductID3 OUTPUT, 'TSHIRT20', 'T-shirt', 'Comfortable cotton t-shirt', 'Medium size', 0.3;     -- ProductID = 3

    -- Insert Positions
    DECLARE @PositionID1 INT, @PositionID2 INT;
    EXEC sp_InsertPosition 'Manager', 'Manages operations', 50000, @PositionID1 OUTPUT; -- PositionID = 1
    EXEC sp_InsertPosition 'Clerk', 'Handles sales', 30000, @PositionID2 OUTPUT;        -- PositionID = 2
    
    -- Insert Employees
    DECLARE @EmployeeID1 INT, @EmployeeID2 INT;
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
    
    -- Insert Customers
    DECLARE @CustomerID1 INT, @CustomerID2 INT;
    EXEC sp_InsertCustomer 'Acme Corp', '789 Road', NULL, @CityID1, '555-9999', 'acme@example.com', @CustomerID1 OUTPUT;         -- CustomerID = 1
    EXEC sp_InsertCustomer 'Global Inc', '321 Boulevard', NULL, @CityID2, '555-8888', 'global@example.com', @CustomerID2 OUTPUT; -- CustomerID = 2
    
    -- Insert Contracts
    DECLARE @ContractID1 INT, @ContractID2 INT;
    EXEC sp_InsertContract 'Contract A', 'Details of contract A', @CustomerID1, @EmployeeID1, 1000, @ContractID1 OUTPUT;   -- ContractID = 1
    EXEC sp_InsertContract 'Contract B', 'Details of contract B', @CustomerID2, @EmployeeID2, 2000, @ContractID2 OUTPUT;   -- ContractID = 2
    
    -- Insert Storages
    DECLARE @StorageID1 INT;
    EXEC sp_InsertStorage 'Main Warehouse', '100 Warehouse St', NULL, @CityID1, 'Primary storage facility', @StorageID1 OUTPUT;  -- StorageID = 1
    
    -- Insert Slots
    DECLARE @SlotID1 INT, @SlotID2 INT;
    EXEC sp_InsertSlot @StorageID1, 'A1', @SlotID1 OUTPUT;   -- SlotID = 1
    EXEC sp_InsertSlot @StorageID1, 'B2', @SlotID2 OUTPUT;   -- SlotID = 2
    
    -- Insert ContractDetails
    DECLARE @ContractDetailID1 INT, @ContractDetailID2 INT;
    EXEC sp_InsertContractDetail @ContractID1, @SlotID1, '2024-01-01', '2024-12-31', @ContractDetailID1 OUTPUT;  -- ContractDetailID = 1
    EXEC sp_InsertContractDetail @ContractID2, @SlotID2, '2025-01-01', '2025-12-31', @ContractDetailID2 OUTPUT;  -- ContractDetailID = 2
    
    -- Insert Transactions
    DECLARE @TransactionID1 INT, @TransactionID2 INT;
    EXEC sp_InsertTransaction @ContractID1, @EmployeeID1, @ProductID1, @SlotID1, 10, 'Import', NULL, @TransactionID1 OUTPUT;  -- TransactionID = 1
    EXEC sp_InsertTransaction @ContractID2, @EmployeeID2, @ProductID2, @SlotID2, 5, 'Export', NULL, @TransactionID2 OUTPUT;   -- TransactionID = 2
    
    -- Insert Users
    DECLARE @UserID1 INT, @UserID2 INT;
    EXEC sp_InsertUser @EmployeeID1, 'admin', 0x123456789, 1, @UserID1 OUTPUT;    -- Creates a user for EmployeeID1
    EXEC sp_InsertUser @EmployeeID2, 'operator', 0x987654321, 2, @UserID2 OUTPUT; -- Creates a user for EmployeeID2

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
