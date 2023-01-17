--SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

--Transaction #1 (starts first)
BEGIN TRANSACTION;
UPDATE dbo.Orders SET Price = 50 WHERE Id = 7 --The value 50 is set
WAITFOR DELAY '00:00:05'
ROLLBACK; --The value 50 is rolled back

--Transaction #2 (starts immediately after the start of transaction #1)
BEGIN TRANSACTION;
SELECT Price FROM dbo.Orders WHERE Id = 7 --The transaction is waiting here
COMMIT;


/*
The first transaction acquires an exclusive lock (X) before running the UPDATE statement and holds it until the end of the transaction.

The second transaction tries to acquire a shared lock (S) for the same record that the first transaction has already acquired an exclusive lock (X).

The second transaction must wait for the first transaction to finish execution and release the exclusive lock because a shared lock cannot be acquired on a record that already has an exclusive lock.

When the first transaction completes, the second continues executing and reads a non-dirty value from the table.
*/