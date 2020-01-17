using System.Security;
using System.Runtime.InteropServices;

namespace Callisto.Binder.Net.Show
{
    public class Point3fClass
    {
        #region Methods

        public static class Signatures
        {
            [SuppressUnmanagedCodeSecurity]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, SetLastError = false)]
            public delegate point3f construct_point3f(float x, float y, float z);
        }

        public Signatures.construct_point3f ConstructPoint3f;

        #endregion
    }
}
