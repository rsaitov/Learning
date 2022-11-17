# Features in C# 11 (by Nick Chapsas)

## Links

YouTube: https://www.youtube.com/watch?v=cqCBhkNroDI

1. [Raw string literals](#raw-string-literals)
2. [List patterns](#list-patterns)
3. [Generic attributes](#generic-attributes)
4. [Extended nameof](#extended-nameof)
5. [UTF8 string literals](#utf8-string-literals)
6. [String interpolated new line](#string-interpolated-new-line)
7. [Generic math](#generic-math)
8. [Required members](#required-members)
9. [File scoped types](#file-scoped-types)
10. [Pattern match span](#pattern-match-span)
11. [Auto default struct](#auto-default-struct)
12. [Improved method group](#improved-method-group)
13. [Numberic int pointer aliases](#numberic-int-pointer-aliases)
14. [Ref fields scoped](#ref-fields-scoped)

## Raw string literals

Multiple quotes

```C#
using System.Text;

var sb = new StringBuilder();

// v1: escaping with backslash 
// var xmlPrologue = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";

// v2: verbatim string symbol and escaping with quotes
// var xmlPrologue = @"<?xml version=""1.0"" encoding=""UTF-8""?>";

// raw string literals
var xmlPrologue = """<?xml version="1.0" encoding="UTF-8"?>""";
var xmlPrologue = """"<?xml version="""1.0""" encoding="UTF-8"?>"""";

var jsonText = """
{
    "name": "Rinat Saitov"
}
""";

var name = "Rinat Saitov";
var jsonTextInterpolated = $$"""
{
    "name": {{name}}    // double brackets because of using brackets in json-block
}
""";

sb.Append(xmlPrologue);
sb.Append(jsonText);
sb.Append(jsonTextInterpolated);

// More XML writing stuff

Console.WriteLine(sb.ToString());
```
## List patterns
```C#

int[] numbers = { 1, 2, 3 };

Console.WriteLine(numbers is [1, 2, 3]);            // true
Console.WriteLine(numbers is [1, 2, 4]);            // false
Console.WriteLine(numbers is [1, 2, 3, 4]);         // false

Console.WriteLine(numbers is [0 or 1, <= 2, >= 3]); // true

if (numbers is [var first, _, _])
{
    Console.WriteLine(first);
}

if (numbers is [var first, .. var rest])
{
    Console.WriteLine(rest); // int[2] {2, 3}
}

// String array examples
var emptyName = Array.Empty<string>();
var myName = new[] { "Rinat Saitov" };
var myNameBrokenDown = new[] { "Rinat",  "Saitov" };
var myNameBrokenDownFurther = new[] { "Rinat",  "Saitov", "2nd" };

var text = myName switch {
    [] => "Name was empty",
    [var fullName] => $"My full name is: {fullName}",
    [var firstName, var lastName] => $"My full name is: {firstName} {lastName}"
};

Console.WriteLine(text);

```
## Generic attributes

```C#

[Validator(UserValidator)]
public class User
{

}

public class ValidatorAttribute<T> : Attribute
{
    public ValidatorAttribute()
    {
        ValidatorType = typeof(T);
    }
}

``` 

## Extended nameof

Nameof in attributes

```C#

public class MyClass
{
    // before C# 11 only available
    // [Name("text")]

    [Name(nameof(text))]
    public void Test(string text)
    {

    }
}

public class NameAttribute : Attribute {...}

```

## UTF8 string literals

```C#

string text = "Rinat Saitov"; // UTF16
ReadOnlySpan<byte> text = "Rinat Saitov"u8; // UTF8

ReadOnlySpan<byte> u16A = Encoding.Unicode.GetBytes("A");
ReadOnlySpan<byte> u8A = "A"u8;

```
## String interpolated new line

```C#

var world = "World";

Console.WriteLine($"Hello, {world
    .ToLower()}!"); // new line works in interpolation C# 11

```

## Generic math

```C#

var numbers = { 1, 2, 3, 4, 5, 0.69 }

var sum = AddAll(numbers);

Console.WriteLine(sum);

T AddAll<T>(T[] values) where T : INumber<T>
{
    T result = T.Zero;
    foreach (var value in values)
    {
        result += value;
    }

    return result;
}

```

## Required members

Require to init properties on creation

```C#

var user = new User 
{
    FullName = "Rinat Saitov"
};

class User
{
    public required string FullName { get; init; }
}


```

## File scoped types

```C#

User1.cs
// type visible only within this file
file class User
{

}

User2.cs
// global visible type
public class User
{

}

```

Can hide types, which used in other type's logic.

## Pattern match span

```C#

ReadOnlySpan<char> test = "Rinat Saitov";

Console.WriteLine(text is "Rinat Saitov");  //true
Console.WriteLine(text is ['R', ..]);       //true
Console.WriteLine(text is ['A', ..]);       //false

```

## Auto default struct

```C#

public struct Point
{
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        //Y = y; 
        //not available in C# < 11
        //C# 11 fills field with default value
    }
}

```

## Improved method group

```C#

// C# < 11: Sum() cached and optimized
// C# 11: SumMethodGroup() works exactly as Sum()
public int Sum()
{    
    return Ages.Where(x => Filter(x)).Sum();
}

public int SumMethodGroup()
{
    return Ages.Where(Filter).Sum();
}

static bool Filter(int age)
{
    return age > 50;
}

```

## Numberic int pointer aliases

```C#

IntPtr intPtr = IntPtr.Zero;
UIntPtr intPtr = UIntPtr.Zero;

// the same in C# 11
nint intPtr = nint.Zero;
nuint intPtr = nuint.Zero;

```

## Ref fields scoped

```C#

public void Append(scoped ReadOnlySpan<char> value)
{
    ...
}

Span<int> CreateSpan(scoped ref int parameter)
{
    ...
}

Span<int> CreateSpan2()
{
    scoped Span<int> span = stackalloc int[420];
    return Span<int>.Empty;
}

```
