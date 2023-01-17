--Initial data in the table:
--1	"Order 1"	100
--2	"Order 2"	200
--3	"Order 3"	300

--Transaction #1 (starts first)
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE
BEGIN TRANSACTION;
SELECT Price FROM dbo.Orders WHERE Id >= 2 --2 rows is retrieved from Orders table
WAITFOR DELAY '00:00:05'
SELECT Price FROM dbo.Orders WHERE Id >= 2 --2 rows is retrieved from Orders table
COMMIT;

--Transaction #2 (starts immediatelly after the start of transaction #1)
BEGIN TRANSACTION;
INSERT INTO dbo.Orders VALUES ('Order 4', 400)
COMMIT;