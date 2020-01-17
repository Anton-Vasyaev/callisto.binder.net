using System;
using System.Security;
using System.Runtime.InteropServices;

namespace Callisto.Binder.Net.Show
{
    public class NativeClass
    {
        #region Methods

        public static class Signatures
        {
            [SuppressUnmanagedCodeSecurity]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, SetLastError = false)]
            public delegate void copy_bytes(IntPtr src, IntPtr dst, IntPtr size);
        }

        public Signatures.copy_bytes CopyBytes;

        #endregion

        #region Data

        public Point3fClass Point3fClass;

        #endregion

        #region .ctor

        public NativeClass()
        {
            Point3fClass = new Point3fClass();
        }

        #endregion
    }
}
