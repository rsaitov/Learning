var str = "Hello, World!";
var ptrStr = AddressOf(str);
Console.WriteLine($"str address: {ptrStr}");

var ptrInt = AddressOf(17);
Console.WriteLine($"int address: {ptrInt}");

uint[] arr = { 0xAABBCCDD, 0xAABBCCDD, 0xAABBCCDD, 0xAABBCCDD, 0xFFFFFFFF };
var ptrArr = AddressOf(arr);
Console.WriteLine($"arr address: {ptrArr}");

// return RAM address of the object
unsafe IntPtr AddressOf(object o)
{
    TypedReference mk = __makeref(o);
    return **(IntPtr**)&mk;
}