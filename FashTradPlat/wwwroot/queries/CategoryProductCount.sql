SELECT 
    Category_Name, 
    COUNT(ProductID) product_count
FROM 
    Products
    INNER JOIN Categories  
        ON Category_ID = CategoryID
GROUP BY 
    Category_Name;
/* This sql statement counts and returns a result that shows the number of products in a category  */