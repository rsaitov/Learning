// Analyze with dotPeek

// 1. Anonymous objects

var obj1 = new { Hello = "Hello", World = "World" };        // anonymous type 0 (generic)
var obj2 = new { Hello = 111, World = new List<int>() };    // anonymous type 0 (generic)
var obj3 = new { Hello1 = "Hello", World1 = "World" };      // anonymous type 1
Console.WriteLine(obj1);    // pretty string due to overriden ToString()
Console.WriteLine(obj2);    // pretty string due to overriden ToString()
Console.WriteLine(obj3);    // pretty string due to overriden ToString()

// 2. Events 

var evt = new ClassWithEvent();
evt.OnEvent += () =>
{
    Console.WriteLine("BlaBla");
};

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

// 4 async/await
// State Machine

await M1();

async Task M1()
{
    try
    {
        await Task.Delay(1000);
        Console.WriteLine("Hello 1");

    }
    catch(IOException)
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

class ClassWithEvent
{
    // generates add and delete methods
    // Interlocked statements on add/delete delegates
    public event Action OnEvent;
}