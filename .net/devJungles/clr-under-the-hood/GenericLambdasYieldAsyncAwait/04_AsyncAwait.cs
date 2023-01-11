internal class AsyncAwait
{
    // State machine
    public async static void Start()
    {
        await M1();

        async Task M1()
        {
            try
            {
                await Task.Delay(1000);
                Console.WriteLine("Hello 1");

            }
            catch (IOException)
            {
                // Low-level C# code handles all exceptions to guarantee
                // finally call
                await Task.Delay(500);
                Console.WriteLine("Hello 2");
            }
            finally
            {
                await Task.Delay(150);
                Console.WriteLine("Hello 3");
            }
        }
    }
}