using BenchmarkDotNet.Attributes;
using System.Linq.Expressions;
using System.Reflection;

public class CreateInstanceBenchmark
{
    private Lazy<Func<Person>> _lazyLambda = new (BuildLambda);

    private static Type _type = typeof(Person);
    private static PropertyInfo _nameProp = _type.GetProperty("Name");
    private static PropertyInfo _surnameProp = _type.GetProperty("Surname");

    /// <summary>
    /// Create Instance With ctor
    /// </summary>
    [Benchmark(Baseline = true)]    
    public Person CompileTime()
    {
        return new Person()
        {
            Name = "Rinat",
            Surname = "Saitov"
        };
    }

    /// <summary>
    /// Create Instance With Reflection No Cache
    /// </summary>
    [Benchmark]
    public object WithReflectionNoCache()
    {
        var type = typeof(Person);
        var obj = Activator.CreateInstance(type);

        type.GetProperty("Name").SetValue(obj, "Rinat");
        type.GetProperty("Surname").SetValue(obj, "Saitov");

        return obj;
    }

    /// <summary>
    /// Create Instance With Reflection Cached
    /// </summary>
    [Benchmark]
    public object WithReflectionCached()
    {
        var obj = Activator.CreateInstance(_type);

        _nameProp.SetValue(obj, "Rinat");
        _surnameProp.SetValue(obj, "Saitov");

        return obj;
    }

    /// <summary>
    /// Create Instance With Expression
    /// </summary>
    [Benchmark]
    public object WithExpression()
    {
        return _lazyLambda.Value();
    }

    private static Func<Person> BuildLambda()
    {
        var type = typeof(Person);
        var creation = Expression.New(type);
        var variable = Expression.Variable(type, "x");

        var block = Expression.Block(variables: new[] { variable }, 
            Expression.Assign(variable, creation),
            Expression.Assign(Expression.PropertyOrField(variable, "Name"), Expression.Constant("Rinat")),
            Expression.Assign(Expression.PropertyOrField(variable, "Surname"), Expression.Constant("Saitov")),
            variable
        );

        var lambda = Expression.Lambda<Func<Person>>(block);
        return lambda.Compile();
    }
}
