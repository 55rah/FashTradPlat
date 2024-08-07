CREATE PROCEDURE Womenswear
As
BEGIN
Select 
*
FROM
Products
WHERE
CategoryID = '2'
END
/* This query creates a stored procedure for all products within the "Womenswear" category */