using System.Runtime.InteropServices;
using System.Security;

namespace Callisto.Binder.Net.Show
{
    public class NativeWrap
    {
        #region Methods

        public static class Signatures
        {
            [SuppressUnmanagedCodeSecurity]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, SetLastError = false)]
            public delegate float native_pow(float value, int pow_value);
        }

        public Signatures.native_pow NativePow { get; private set; }

        #endregion

        public NativeClass NativeClass { get; private set; }
    }
}
