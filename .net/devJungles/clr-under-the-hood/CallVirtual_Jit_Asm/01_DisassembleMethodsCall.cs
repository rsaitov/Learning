internal class DisassembleMethodsCall
{
    public static void Start()
    {
        Console.WriteLine();
        Console.WriteLine("--> DisassembleMethodsCall");

        var test = new Test();

        // GO TO Disassembly
        // before calling static method instruction contains call <method address>
        // after calling static method instruction contains jmp statement
        Test.Static();
        test.Instance();
        test.Virtual();
    }
}

class Test
{
    public void Instance()
    {
        Console.WriteLine("Instance");
    }

    public virtual void Virtual()
    {
        Console.WriteLine("Virtual");
    }

    public static void Static()
    {
        Console.WriteLine("Static");
    }
}