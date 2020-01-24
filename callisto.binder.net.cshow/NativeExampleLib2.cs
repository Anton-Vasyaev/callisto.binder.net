using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

namespace Callisto.Binder.Net.CShow
{
    public class NativeExampleLib2
    {
        #region Data

        private static LibraryPack _libraryPack = null;

        #endregion

        #region NativeFunctions

        public static class Signatures
        {
            [SuppressUnmanagedCodeSecurity]
            [UnmanagedFunctionPointer(CallingConvention.Cdecl, SetLastError = false)]
            public delegate float nativeMul(float value1, float value2);
        }

        public static Signatures.nativeMul NativeMul { get; private set; }

        #endregion

        #region Scopes

        public static NativeWrap NativeWrap { get; private set; }

        #endregion

        #region .ctor

        static NativeExampleLib2()
        {
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            string dllPath = assemblyLocation != null ? Path.GetDirectoryName(assemblyLocation) : Environment.CurrentDirectory;

            var winLibRecords = new WinLibRecord[]
            {
                new WinLibRecord(
                    "nativeexample",
                    new ((PlatformArch, BitDesign), string path)[]
                    {
                        ((PlatformArch.x86, BitDesign.x32), Path.Combine(dllPath, "native_c_example_x32.dll")),
                        ((PlatformArch.x86, BitDesign.x64), Path.Combine(dllPath, "native_c_example_x64.dll")),
                    })
            };

            WrapperInitializer.Initialize(
                typeof(NativeExampleLib2),
                winLibRecords,
                null
            );
        }

        #endregion
    }
}
