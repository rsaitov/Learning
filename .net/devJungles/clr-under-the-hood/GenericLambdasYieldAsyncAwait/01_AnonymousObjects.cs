internal class AnonymousObjects
{
    public static void Start()
    {
        // 1. Anonymous objects

        var obj1 = new { Hello = "Hello", World = "World" };        // anonymous type 0 (generic)
        var obj2 = new { Hello = 111, World = new List<int>() };    // anonymous type 0 (generic)
        var obj3 = new { Hello1 = "Hello", World1 = "World" };      // anonymous type 1
        Console.WriteLine(obj1);    // pretty string due to overriden ToString()
        Console.WriteLine(obj2);    // pretty string due to overriden ToString()
        Console.WriteLine(obj3);    // pretty string due to overriden ToString()
    }
}