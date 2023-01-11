using System.Linq.Expressions;

internal class Lambda
{
    // Closure works cause allocation sealed class for lambda,
    // x becomes public field of allocatoed object

    // If you don't use lambda outside method, it could be created with no allocation
    // Then struct would be used and lambda destroys after getting out of scope

    public static void Start()
    {
        bool x = true;

        // closure here...
        Expression<Func<bool>> boolExp = () => x;

        // ...here...
        Func<bool> boolLambda = () => x;

        boolLambda();
        BoolMethod();

        // ...and here
        bool BoolMethod() => x;
    }

    public static void Start2()
    {
        int x = 10, y = 20;

        int z = 30;

        Func<int> xx = () =>
        {
            // value z could not be visible here
            // cause it not added do struct

            // writing next statement adds z field
            int z1 = z;

            return x + y;
        };

        Console.WriteLine(xx());
    }
}