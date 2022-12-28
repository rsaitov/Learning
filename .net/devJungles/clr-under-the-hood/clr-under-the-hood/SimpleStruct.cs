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
}
