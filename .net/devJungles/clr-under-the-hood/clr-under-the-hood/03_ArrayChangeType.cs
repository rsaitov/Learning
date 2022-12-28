namespace clr_under_the_hood
{
    public static class ArrayChangeType
    {
        internal static void Execute()
        {
            byte[] buffer = { 0xAA, 0xBB, 0xCC, 0xDD, 0xAA, 0xBB, 0xCC, 0xDD, 0xAA, 0xBB, 0xCC, 0xDD };
            int[] arrBuffer = ChangeType(buffer);
            Console.WriteLine(arrBuffer);
        }

        /// <summary>
        /// Changes array type: byte[] -> int[]
        /// Byte array:
        ///             00 00 00 00     // Synch block  
        /// 1d 2a 3b 4c 00 00 00 00     // Object-type reference
        /// 0c 00 00 00 00 00 00 00     // Array length
        /// 11 22 33 44 55 66 77 88     // Array elements...
        /// </summary>
        private static unsafe int[] ChangeType(byte[] arr)
        {
            // void* - pointer of undefined type
            var sPtr = (long*)AddressViewer.AddressOf(arr).ToPointer();

            // Reach Object-type value of empty int array
            var ptrArrInt = (long*)AddressViewer.AddressOf(Array.Empty<int>());

            // Change array type from byte to int
            *sPtr = *ptrArrInt;

            // Change array length. 
            *(sPtr + 1) = arr.LongLength / sizeof(int);

            // Return casted array
            return (int[])(object)arr;
        }
    }
}
