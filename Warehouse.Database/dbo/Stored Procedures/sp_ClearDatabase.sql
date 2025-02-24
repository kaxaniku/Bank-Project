CREATE PROCEDURE sp_ClearDatabase
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Categories;
    DBCC CHECKIDENT('Categories', RESEED, 0);
    
    DELETE FROM Products;
    DBCC CHECKIDENT('Products', RESEED, 0);

    -- Add more tables here...

    RETURN 0;
END