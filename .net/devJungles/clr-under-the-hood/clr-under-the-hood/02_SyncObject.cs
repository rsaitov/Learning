namespace clr_under_the_hood
{
    internal class SyncObject
    {
        ///             00 00 00 00     // Sync block
        /// locked or not by recording thread id
        /// Also may contain recursive lock info
        internal static void Execute()
        {
            var syncObject = new object();
            var hashCode = syncObject.GetHashCode();
            var ptrSyncObject = AddressViewer.AddressOf(syncObject);

            Thread t = null;
            t = new Thread(o =>
            {
                lock (syncObject)
                {
                    lock (syncObject)
                    {
                        // Not recomended to use sync object both to
                        // synch and hasCode calculations
                        // Uses the same memory blocks, may gain lots of time
                        // cause of memory jumps.
                        syncObject.GetHashCode();
                        Console.WriteLine(t.ManagedThreadId);
                        Console.WriteLine($"ptrSyncObject address: {ptrSyncObject}");
                    }
                }
            });
            t.Start();
            t.Join();

            lock (syncObject)
            {
                lock (syncObject)
                {
                    Console.WriteLine($"ptrSyncObject address: {ptrSyncObject}");
                }
            }

            lock (syncObject)
            {
            }
        }
    }
}
