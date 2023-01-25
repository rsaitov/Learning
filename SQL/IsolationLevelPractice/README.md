https://habr.com/ru/company/infopulse/blog/261097/

https://levelup.gitconnected.com/transaction-isolation-levels-in-ms-sql-guide-for-backend-developers-6a5998e34f6c#4808

# Transaction isolation level

___

**Q: What is transaction isolation level in SQL?**

Transaction isolation level defines what data can be observed by read statements inside a running SQL transaction.

___

**Q: Shared (S) vs Exclusive (X) Locks**

Understanding the key differences, at least between shared (S) and exclusive (X) locks, is important to understanding transaction isolation levels.

- Read operations (SELECT) acquire shared locks on a resource (row, key, table, page etc.) before reading the data.
- Data modification operations (INSERT, UPDATE, DELETE) acquire exclusive locks before modifying the data.
- A shared lock can be acquired for a resource even if it already has another shared lock. A resource can have as many shared locks as needed because parallel reading is a safe operation.
- An exclusive lock can be acquired for a resource only if it does not already have shared or exclusive locks to ensure that only one transaction can modify the data at one time.
- If shared or exclusive locks cannot be acquired, the transaction will wait until the locks are released (not applicable to shapshot isolation level).
___

**Q: What isolation levels are available?**

`READ UNCOMMITTED` - a transaction running at this isolation level can read uncommitted data made by other transactions.
Using a read uncommitted isolation level for a transaction is a good choice for querying data that is unlikely to change, or static data that never actually changes after it is created.

**Shared locks (S) are not acquired at all at the read uncommitted isolation level.**

Reading an uncommitted isolation level or NOLOCK hint do not require shared locks, which improves performance when querying data.

`READ COMMITTED` - a transaction can only read committed data made by other transactions. This is the default option.

**Exclusive lock (X) before running the UPDATE. Shared locks (S) releases on SELECT-statement end.**

`REPEATABLE READ` - a transaction can only read committed data made by other transactions and also places locks on them so that other transactions can't modify that data until current transaction completes.

**The non-repeatable read problem cannot occur at the REPEATABLE READ isolation level because the transaction holds a shared lock (S) until the end of the transaction.**

`SNAPSHOT` - a transaction can only read data that was available at the start of the current transaction along with any uncommitted changes made in the current transaction.

`SERIALIZABLE` - transactions that interact with the same data are run sequentially rather than concurrently.

**Transaction starts and acquires a shared range lock (RangeS-S). Shared range lock locks not only existing records, but also records that could potentially fall into the predicate range specified in WHERE clause (all records with Id ≥2).**

<p>
<img src="https://retool.com/blog/content/images/2020/03/Image-2020-01-21-at-5.48.02-PM.png" />
</p>

## Summary
- The higher the isolation level of the transaction, the better the protection against concurrency problems, but the lower the performance.
- Serializable is the highest isolation level that protects transactions from all types of concurrency phenomena.
- Repeatable read isolation level does not protect transactions from a phantom reads problem.
- Read committed transaction isolation level does not protect transactions from phantom reads, non-repeatable reads and lost updates problems.
- Read uncommitted does not protect transactions from phantom reads, non-repeatable read, lost updates and dirty reads problems.
