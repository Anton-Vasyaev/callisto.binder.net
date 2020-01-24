using System.Security;
using System.Runtime.InteropServices;

namespace Callisto.Binder.Net.CShow
{
    public class Point3fClass
    {
        #region NativeFunction

        public static class Signatures
        {
            [SuppressUnmanagedCodeSecurity]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, SetLastError = false)]
            public delegate point3f NativeWrap_NativeClass_Point3fClass_constructPoint3f(
                float x, 
                float y, 
                float z
            );
        }

        public Signatures.NativeWrap_NativeClass_Point3fClass_constructPoint3f ConstructPoint3f { get; private set; }

        #endregion
    }
}
