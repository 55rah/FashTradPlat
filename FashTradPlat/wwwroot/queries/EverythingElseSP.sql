CREATE PROCEDURE EverythingElse
As
BEGIN
Select 
*
FROM
Products
WHERE
CategoryID = '3'
END
/* This query creates a stored procedure for all products within the "Everything Else" category */