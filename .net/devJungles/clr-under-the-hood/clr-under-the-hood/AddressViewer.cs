namespace clr_under_the_hood
{
    public static class AddressViewer
    {
        // Return RAM address of the object
        public unsafe static IntPtr AddressOf(object o)
        {
            TypedReference mk = __makeref(o);
            return **(IntPtr**)&mk;
        }
    }
}
