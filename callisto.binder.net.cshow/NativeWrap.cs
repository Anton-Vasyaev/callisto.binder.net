using System.Runtime.InteropServices;
using System.Security;

namespace Callisto.Binder.Net.CShow
{
    public class NativeWrap
    {
        #region NativeFunctions

        public static class Signatures
        {
            [SuppressUnmanagedCodeSecurity]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, SetLastError = false)]
            public delegate float NativeWrap_nativePow(float value, int pow_value);
        }

        public Signatures.NativeWrap_nativePow NativePow { get; private set; }

        #endregion

        #region Scope

        public NativeClass NativeClass { get; private set; }

        #endregion
    }
}
