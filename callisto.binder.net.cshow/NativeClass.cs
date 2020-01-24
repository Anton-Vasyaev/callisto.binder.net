using System;
using System.Security;
using System.Runtime.InteropServices;

namespace Callisto.Binder.Net.CShow
{
    public class NativeClass
    {
        #region NativeFunctions

        public static class Signatures
        {
            [SuppressUnmanagedCodeSecurity]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, SetLastError = false)]
            public delegate void NativeWrap_NativeClass_copyBytes(IntPtr src, IntPtr dst, IntPtr size);
        }

        public Signatures.NativeWrap_NativeClass_copyBytes CopyBytes { get; private set; }

        #endregion

        #region Scopes

        public Point3fClass Point3fClass { get; private set; }

        #endregion
    }
}
