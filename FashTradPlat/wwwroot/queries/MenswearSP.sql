CREATE PROCEDURE Menswear
As
BEGIN
Select 
*
FROM
Products
WHERE
CategoryID = '1'
END
/* This query creates a stored procedure for all products within the "Menswear" category */