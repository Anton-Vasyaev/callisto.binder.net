using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

using Callisto.Binder.Net;

namespace Callisto.Binder.Net.Show
{
    #region Data

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public struct point3f
    {
        public float x;
        public float y;
        public float z;
    }

    #endregion

    public static class NativeExampleLib
	{
		#region Data

		private static LibraryPack _libraryPack = null;

		#endregion

		#region ConstructAndDestruct

		static NativeExampleLib()
		{
			string dllPath = null;
			string assemblyLocation = Assembly.GetExecutingAssembly().Location;
			if (assemblyLocation != null) dllPath = Path.GetDirectoryName(assemblyLocation);
			else dllPath = Environment.CurrentDirectory;

			var winLibRecords = new WinLibRecord[]
			{
				new WinLibRecord(
					"nativeexample",
					new ((PlatformArch, BitDesign), string path)[]
					{
						((PlatformArch.x86, BitDesign.x32), Path.Combine(dllPath, "nativeexample_x32.dll")),
						((PlatformArch.x86, BitDesign.x64), Path.Combine(dllPath, "nativeexample_x64.dll")),
					})
			};

			WrapperInitializer.Initialize(
				typeof(NativeExampleLib),
				winLibRecords,
				null
			);
		}

		public static void Initialize()
		{
			// call static constructor
		}

        #endregion

        #region Methods

        public static class Signatures
		{
			[SuppressUnmanagedCodeSecurity]
			[UnmanagedFunctionPointer(CallingConvention.Cdecl, SetLastError = false)]
			public delegate float native_mul(float value1, float value2);
		}

		public static Signatures.native_mul NativeMul { get; private set; }

		#endregion

		#region ScopeChildrens

		#region NativeWrap

		public static class NativeWrap
		{
            #region Methods

            public static class Signatures
			{
                [SuppressUnmanagedCodeSecurity]
                [UnmanagedFunctionPointer(CallingConvention.Cdecl, SetLastError = false)]
                public delegate float native_pow(float value, int pow_value);
            }

            public static Signatures.native_pow NativePow { get; private set; }

            #endregion

            #region ScopeChildrens

            #region NativeClass

            public static class NativeClass
			{
                #region Methods

                public static class Signatures
				{
                    [SuppressUnmanagedCodeSecurity]
                    [UnmanagedFunctionPointer(CallingConvention.Cdecl, SetLastError = false)]
                    public delegate void copy_bytes(IntPtr src, IntPtr dst, IntPtr size);
                }

                public static Signatures.copy_bytes CopyBytes { get; private set; }

                #endregion

                #region ScopeChildrens

                #region Point3fClass

                public static class Point3fClass
                {
                    #region Methods

                    public static class Signatures
                    {
                        [SuppressUnmanagedCodeSecurity]
                        [UnmanagedFunctionPointer(CallingConvention.Cdecl, SetLastError = false)]
                        public delegate point3f construct_point3f(float x, float y, float z);
                    }

                    public static Signatures.construct_point3f ConstructPoint3f { get; private set; }

                    #endregion
                }

                #endregion

                #endregion
            }

            #endregion

            #endregion
        }

        #endregion

        #endregion
    }
}
