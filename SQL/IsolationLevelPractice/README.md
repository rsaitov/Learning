https://habr.com/ru/company/infopulse/blog/261097/

# Transaction isolation level

___

**Q: What is transaction isolation level in SQL?**

Transaction isolation level defines what data can be observed by read statements inside a running SQL transaction.

___

**Q: What isolation levels are available?**

`READ UNCOMMITTED` - a transaction running at this isolation level can read uncommitted data made by other transactions.
Using a read uncommitted isolation level for a transaction is a good choice for querying data that is unlikely to change, or static data that never actually changes after it is created.

**Shared locks are not acquired at all at the read uncommitted isolation level.**

Reading an uncommitted isolation level or NOLOCK hint do not require shared locks, which improves performance when querying data.

`READ COMMITTED` - a transaction can only read committed data made by other transactions. This is the default option.

`REPEATABLE READ` - a transaction can only read committed data made by other transactions and also places locks on them so that other transactions can't modify that data until current transaction completes.

**The non-repeatable read problem cannot occur at the REPEATABLE READ isolation level because the transaction holds a shared lock until the end of the transaction.**

`SNAPSHOT` - a transaction can only read data that was available at the start of the current transaction along with any uncommitted changes made in the current transaction.

`SERIALIZABLE` - transactions that interact with the same data are run sequentially rather than concurrently.

**Transaction starts and acquires a shared range lock (RangeS-S). Shared range lock locks not only existing records, but also records that could potentially fall into the predicate range specified in WHERE clause (all records with Id ≥2).**

<p>
<img src="https://sqlperformance.com/wp-content/uploads/2015/04/image.png" />
</p>
