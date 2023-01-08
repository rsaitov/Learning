using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clr_under_the_hood
{
    public static class PointerAddress
    {
        public static void Execute()
        {
            var str = "Hello, World!";
            var ptrStr = AddressViewer.AddressOf(str);
            Console.WriteLine($"str address: {ptrStr}");

            var ptrInt = AddressViewer.AddressOf(17);
            Console.WriteLine($"int address: {ptrInt}");

            uint[] arr = { 0xAABBCCDD, 0xAABBCCDD, 0xAABBCCDD, 0xAABBCCDD, 0xFFFFFFFF };
            var ptrArr = AddressViewer.AddressOf(arr);
            Console.WriteLine($"arr address: {ptrArr}");
            
            var myStruct1 = new SimpleStruct2() { B1 = 0xAA, I = 0xBBBBBBBB, S = 0xEEEE, S1 = "Hello" };
            var ptrMyStruct1 = AddressViewer.AddressOf(myStruct1);
            Console.WriteLine($"myStruct address: {ptrMyStruct1}");

            var myStruct2 = new SimpleStruct2() { B1 = 0xBB, I = 0xCCBBBBBB, S = 0xDDEE, S1 = "World" };
            var ptrMyStruct2 = AddressViewer.AddressOf(myStruct2);
            Console.WriteLine($"myStruct address: {ptrMyStruct2}");

            SimpleStruct[] arrStruct = {
                new SimpleStruct() { B1 = 0xAA, I = 0xBBBBBBBB, S = 0xEEEE },
                new SimpleStruct() { B1 = 0xFF, I = 0xBBBBBBFF, S = 0xEEFF }
            };

            var ptrArrStruct = AddressViewer.AddressOf(arrStruct);
            Console.WriteLine($"ptrArrStruct address: {ptrArrStruct}");
        }
    }
}
