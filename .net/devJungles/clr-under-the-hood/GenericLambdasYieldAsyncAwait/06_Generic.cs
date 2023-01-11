internal class Generic
{
    // Use of "WinDbg Preview"
    // https://learn.microsoft.com/en-us/windows-hardware/drivers/debugger/debugger-download-tools
    public static void Start()
    {
        Console.WriteLine("--> Generic");

        // how many times would be called static ctor?
        // answer: 3 times
        // cause StaticClass<int> and StaticClass<double> are different types
        var a = new StaticClass<int>();
        var b = new StaticClass<double>();
        var c = new StaticClass<object>();

        // false
        Console.WriteLine(typeof(StaticClass<int>) == typeof(StaticClass<double>));

        // false, BUT...
        // ...Methods table wiil be the same for both 2 types
        Console.WriteLine(typeof(StaticClass<object>) == typeof(StaticClass<string>));

        // how to check whether two types have the same generic class?
        var genericType = typeof(StaticClass<int>);

        // true
        Console.WriteLine(genericType.IsGenericType);

        // true
        Console.WriteLine(genericType.GetGenericTypeDefinition() == typeof(StaticClass<>));


        // Generic methods
        // Go to Disassembly

        // different methods for vale types
        M<int>();
        M<double>();

        // the same methods for reference types
        M<string>();
        M<object>();
    }

    static void M<T>()
    {

    }
}

class StaticClass<T>
{
    // how many times would be called static ctor?
    static StaticClass()
    {
        Console.WriteLine("Called");
    }
}