using System.Diagnostics;

internal class StopwatchJitting
{
    public static void Start()
    {
        Console.WriteLine();
        Console.WriteLine("--> StopwatchJitting");

        var test2 = new Test2();
        var sw = Stopwatch.StartNew();
        sw.Start();

        // need to jit method
        test2.Method(sw);
        Console.WriteLine(sw.ElapsedTicks);

        // already jitted
        // much faster!
        sw = Stopwatch.StartNew();
        sw.Start();
        test2.Method(sw);

        Console.WriteLine(sw.ElapsedTicks);

    }
}

class Test2
{
    public void Method(Stopwatch sw)
    {
        sw.Stop();

        Console.WriteLine("Method");
    }
}