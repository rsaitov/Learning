public class Events
{
    public static void Start()
    {
        var evt = new ClassWithEvent();
        evt.OnEvent += () =>
        {
            Console.WriteLine("BlaBla");
        };
    }
}

class ClassWithEvent
{
    // generates add and delete methods
    // Interlocked statements on add/delete delegates
    public event Action OnEvent;
}