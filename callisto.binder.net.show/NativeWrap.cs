﻿using System.Runtime.InteropServices;
using System.Security;

namespace Callisto.Binder.Net.CppShow
{
    public class NativeWrap
    {
        #region NativeFunctions

        public static class Signatures
        {
            [SuppressUnmanagedCodeSecurity]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, SetLastError = false)]
            public delegate float native_pow(float value, int pow_value);
        }

        public Signatures.native_pow NativePow { get; private set; }

		#endregion

		#region Scope

		public NativeClass NativeClass { get; private set; }

		#endregion
	}
}
