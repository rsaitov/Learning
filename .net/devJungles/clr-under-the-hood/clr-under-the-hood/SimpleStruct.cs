using System.Runtime.InteropServices;

namespace clr_under_the_hood
{
    [StructLayout(LayoutKind.Sequential)]   // guarantees memory usage order in RAM
    struct SimpleStruct
    {
        public byte B1;
        public ushort S;
        public uint I;
    }

    [StructLayout(LayoutKind.Sequential)]   // guarantees memory usage order in RAM
    struct SimpleStruct2
    {
        public byte B1;
        public ushort S;
        public uint I;
        public string S1;
    }
}
