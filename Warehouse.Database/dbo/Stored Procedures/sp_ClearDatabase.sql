CREATE PROCEDURE sp_ClearDatabase
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM Transactions;
    DBCC CHECKIDENT ('Transactions', RESEED, 0);
    
    DELETE FROM ContractDetails;
    DBCC CHECKIDENT ('ContractDetails', RESEED, 0);
    
    DELETE FROM ProductTags;
    
    DELETE FROM Users;
    
    DELETE FROM Contracts;
    DBCC CHECKIDENT ('Contracts', RESEED, 0);
    
    DELETE FROM Customers;
    DBCC CHECKIDENT ('Customers', RESEED, 0);
    
    DELETE FROM Employees;
    DBCC CHECKIDENT ('Employees', RESEED, 0);
    
    DELETE FROM Positions;
    DBCC CHECKIDENT ('Positions', RESEED, 0);
    
    DELETE FROM Products;
    DBCC CHECKIDENT ('Products', RESEED, 0);
    
    DELETE FROM Categories;
    DBCC CHECKIDENT ('Categories', RESEED, 0);
    
    DELETE FROM Slots;
    DBCC CHECKIDENT ('Slots', RESEED, 0);
    
    DELETE FROM Storages;
    DBCC CHECKIDENT ('Storages', RESEED, 0);
    
    DELETE FROM Cities;
    DBCC CHECKIDENT ('Cities', RESEED, 0);
    
    DELETE FROM Countries;
    DBCC CHECKIDENT ('Countries', RESEED, 0);
    
    DELETE FROM Tags;
    DBCC CHECKIDENT ('Tags', RESEED, 0);
    
    RETURN 0;
END