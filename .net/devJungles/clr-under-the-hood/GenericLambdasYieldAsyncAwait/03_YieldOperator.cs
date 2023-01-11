internal class YieldOperator
{
    public static void Start()
    {
        // 3. yield
        // State machine
        for (var enumerator = DoEnumeration(); enumerator.MoveNext();)
        {
            Console.WriteLine(enumerator.Current);
        }

        IEnumerator<int> DoEnumeration()
        {
            yield return 1;
            Console.WriteLine("After 1");
            yield return 2;
            Console.WriteLine("After 2");
            yield return 3;
            Console.WriteLine("After 3");
        }

        // Save thread id
        // On calling enumerator from other thread - state = -1
        var enumerable = DoEnumerable();
        foreach (var obj in enumerable)
        {
            Console.WriteLine(obj);
        }
        foreach (var obj in enumerable)
        {
            Console.WriteLine(obj);
        }

        IEnumerable<int> DoEnumerable()
        {
            yield return 1;
            Console.WriteLine("IEnumerable After 1");
            yield return 2;
            Console.WriteLine("IEnumerable After 2");
            yield return 3;
            Console.WriteLine("IEnumerable After 3");
        }

        var enumerableWithTryFinally = DoEnumerableWithTryFinally();
        foreach (var obj in enumerableWithTryFinally)
        {
            Console.WriteLine(obj);
        }
        IEnumerable<int> DoEnumerableWithTryFinally()
        {
            try
            {
                yield return 1;
                Console.WriteLine("IEnumerable After 1");
            }
            //catch
            //{
            //    yield return 2;
            //    Console.WriteLine("IEnumerable After 2");
            //}
            finally
            {
                //yield return 3;
                Console.WriteLine("IEnumerable After 3");
            }
        }
    }
}