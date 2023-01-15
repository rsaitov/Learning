using System.Reflection;
using System.Runtime.CompilerServices;

// how to override method of sealed class

// PS: no need to allow unsafe to use objects, that use unsafe
// that's why overriding mock objects not needed unsafe keyword
internal class OverrideSealedClass
{
    public void Start()
    {
        Console.WriteLine();
        Console.WriteLine("--> OverrideSealedClass");

        var source = typeof(C1).GetMethod("Method1");
        var dest = typeof(C2).GetMethod("Method2");

        var change = IllegalOverride(source, dest);
        C2.Change = change;

        var c1 = new C1();
        c1.Method1();
    }

    // "allow unsafe" checkbox in solution settings
    unsafe UnsafeMemoryChange IllegalOverride(MethodBase source, MethodBase dest)
    {
        // forcing jitting
        RuntimeHelpers.PrepareMethod(source.MethodHandle);
        RuntimeHelpers.PrepareMethod(dest.MethodHandle);

        // guaranteed jitted methods
        // pointers to methods
        var fp1 = source.MethodHandle.GetFunctionPointer();
        var fp2 = dest.MethodHandle.GetFunctionPointer();

        var f1Ptr = (byte*)fp1.ToPointer();
        var f2Ptr = (byte*)fp2.ToPointer();

        // 1 byte - assembler instruction
        // 4 bytes - method address

        // jmp handles relative address
        var sJump = (uint)f1Ptr + 1 + 4;

        return new UnsafeMemoryChange((uint*)(f1Ptr + 1), (uint)(f2Ptr - sJump));

    }
}

unsafe class UnsafeMemoryChange
{
    private readonly uint* ptr;
    private readonly uint data;
    private readonly uint originalValue;

    public UnsafeMemoryChange(uint* ptr, uint data)
    {
        this.ptr = ptr;
        this.data = data;
        originalValue = *ptr;

        Apply();
    }

    public void Undo()
    {
        *ptr = originalValue;
    }

    // changing method address value
    public void Apply()
    {
        *ptr = data;
    }
}

sealed class C1
{
    public void Method1()
    {
        Console.WriteLine("Method1");
    }
}

sealed class C2
{
    public static UnsafeMemoryChange Change;

    public void Method2(C1 that)
    {
        Change.Undo();

        try
        {
            // on override method "this" will direct to C1 object!
            that.Method1();
        }
        finally
        {
            Change.Apply();
        }

        Console.WriteLine("The system was hacked!");
    }
}