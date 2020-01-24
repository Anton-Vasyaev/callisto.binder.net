using System;
using System.Security;
using System.Runtime.InteropServices;

namespace Callisto.Binder.Net.CppShow
{
    public class NativeClass
    {
        #region NativeFunctions

        public static class Signatures
        {
            [SuppressUnmanagedCodeSecurity]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, SetLastError = false)]
            public delegate void copy_bytes(IntPtr src, IntPtr dst, IntPtr size);
        }

        public Signatures.copy_bytes CopyBytes { get; private set; }

        #endregion

        #region Scopes

        public Point3fClass Point3fClass { get; private set; }

        #endregion
    }
}
