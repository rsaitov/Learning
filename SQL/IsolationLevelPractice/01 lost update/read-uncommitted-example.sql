--Initial price is 100

--Transaction #1 (starts first)
BEGIN TRANSACTION;
UPDATE dbo.Orders SET Price = 50 WHERE Id = 7 --The value 50 is set
WAITFOR DELAY '00:00:05'
ROLLBACK; --The value 50 is rolled back

--Transaction #2 (starts immediately after the start of transaction #1)
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
BEGIN TRANSACTION;
SELECT Price FROM dbo.Orders WHERE Id = 7 --The value 50 is read
COMMIT;
