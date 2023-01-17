--Initial price is 100

--Transaction #1 (starts first)
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
BEGIN TRANSACTION;
SELECT Price FROM dbo.Orders WHERE Id = 1 --The price is 100
WAITFOR DELAY '00:00:05'
SELECT Price FROM dbo.Orders WHERE Id = 1 --The price is 100
COMMIT;

--Transaction #2 (starts immediatelly after the start of transaction #1)
BEGIN TRANSACTION;
UPDATE dbo.Orders SET Price = 200 WHERE Id = 1 --The price becomes 200
COMMIT;