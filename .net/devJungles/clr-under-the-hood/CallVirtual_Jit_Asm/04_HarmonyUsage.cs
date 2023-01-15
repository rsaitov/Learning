using HarmonyLib;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;

// using Harmony library:
// 1. to override methods (demonstration in code below)
// 2. mock static for unit tests
internal class HarmonyUsage
{
    public static void HarmonyDetourMethod()
    {
        Console.WriteLine();
        Console.WriteLine("--> HarmonyUsage");

        var source = typeof(H1).GetMethod("Method1");
        var dest = typeof(H2).GetMethod("Method2");

        HarmonyLib.Memory.DetourMethod(source, dest);

        var h1 = new H1();
        h1.Method1();
    }

    // override (add prefix and postfix methods)
    // to stopwatch all JsonConvert serialization calls
    public static void SerilizationStopwatch()
    {
        Console.WriteLine();
        Console.WriteLine($"--> {nameof(SerilizationStopwatch)}");

        var harmony = new Harmony("stream.project");

        var original = typeof(JsonConvert)
            .GetMethod("SerializeObject", new[] { typeof(object) });

        var prefix = typeof(PerfCounter).GetMethod("Before");
        var postfix = typeof(PerfCounter).GetMethod("After");

        harmony.Patch(
            original,
            new HarmonyMethod(prefix),
            new HarmonyMethod(postfix)
        );

        JsonConvert.SerializeObject(Enumerable.Range(0, 10_000));
    }

    // override constructor
    public static void OverrideConstructor()
    {
        var source = typeof(ConstrToOverride).GetConstructor(Array.Empty<Type>());
        var dest = typeof(Collector<ConstrToOverride>).GetMethod("CtorOverride");

        // Use IllegalMethod to instatiate ConstrToOverride object
        Memory.DetourMethod(source,dest);

        var c = new ClassToCallConstrToOverride();
    }
}

class Collector<T> where T : new()
{
    private static readonly List<T> items = new List<T>();

    static T CtorOverride()
    {
        Console.WriteLine(Environment.StackTrace);
        var result = new T();
        items.Add(result);
        return result;
    }
}

class ConstrToOverride
{
    public ConstrToOverride()
    {
        Console.WriteLine("ConstrToOverride ctor");
    }
}

class ClassToCallConstrToOverride
{
    public ClassToCallConstrToOverride()
    {
        // just need to test ctor override
        var c = new ConstrToOverride();

    }
}

class PerfCounter
{
    public static ThreadLocal<Stopwatch> sw = new ThreadLocal<Stopwatch>();
    public static void Before()
    {
        sw.Value = Stopwatch.StartNew();
    }
    public static void After()
    {
        sw.Value.Stop();
        Console.WriteLine(sw.Value.ElapsedTicks);
    }
}

class H1
{
    public void Method1()
    {
        Console.WriteLine("Method1");
    }
}
class H2
{
    public void Method2()
    {
        Console.WriteLine("The system is hacked with Harmony lib");
    }
}