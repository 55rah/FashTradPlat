INSERT INTO dbo.Transactions(
Checkout_ID,
Payment_ID,
Product_ID,
Status,
TransactionDate,
SendAddress,
EstCompletion
)
VALUES
(
'1',
'1',
'1',
'0',
'7/08/2024',
'14 Hollywood Blvd, 2000 Auckland',
'7/22/2024'
),
(
'2',
'2',
'2',
'0',
'08/08/2024',
'20 Terry St, 4200 Auckland',
'08/22/2024'
),
(
'3',
'3',
'3',
'1',
'10/09/2024',
'2 Sunset Blvd, 4200 Auckland',
'10/23/2024'
);
/* This sql statement inserts 3 rows of data into the Transactions table */