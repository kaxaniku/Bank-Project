CREATE PROCEDURE sp_SeedDatabase
AS
BEGIN
    SET NOCOUNT ON;
    
    EXEC sp_ClearDatabase;

    EXEC sp_InsertCategory 'Electronics'; -- ID = 1
    EXEC sp_InsertCategory 'Clothing'; -- ID = 2
    EXEC sp_InsertCategory 'Books'; -- ID = 3
    EXEC sp_InsertCategory 'Toys'; -- ID = 4
    EXEC sp_InsertCategory 'Furniture'; -- ID = 5

    EXEC sp_InsertProduct 'Laptop', 1, 1000; -- ID = 1
    EXEC sp_InsertProduct 'Smartphone', 1, 500; -- ID = 2
    EXEC sp_InsertProduct 'Headphones', 1, 100; -- ID = 3
    EXEC sp_InsertProduct 'T-shirt', 2, 20; -- ID = 4
    EXEC sp_InsertProduct 'Jeans', 2, 50; -- ID = 5
    EXEC sp_InsertProduct 'Sweater', 2, 30; -- ID = 6
    EXEC sp_InsertProduct 'Novel', 3, 10; -- ID = 7
    EXEC sp_InsertProduct 'Textbook', 3, 50; -- ID = 8
    EXEC sp_InsertProduct 'Coloring book', 3, 5; -- ID = 9
    EXEC sp_InsertProduct 'Action figure', 4, 15; -- ID = 10
    EXEC sp_InsertProduct 'Board game', 4, 30; -- ID = 11
    EXEC sp_InsertProduct 'Puzzle', 4, 20; -- ID = 12
    EXEC sp_InsertProduct 'Table', 5, 200; -- ID = 13
    EXEC sp_InsertProduct 'Chair', 5, 50; -- ID = 14
    EXEC sp_InsertProduct 'Sofa', 5, 300; -- ID = 15

    -- Add more seed data here...

    RETURN 0;
END