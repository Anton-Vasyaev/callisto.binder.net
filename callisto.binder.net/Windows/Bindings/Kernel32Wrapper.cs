using Callisto.Binder.Net.Auxiliary;
using System;
using System.Runtime.InteropServices;

namespace Callisto.Binder.Net.Windows
{
	public static class Kernel32Wrapper
	{
		#region Helper

		public static class Bindings
		{
			[DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
			public static extern IntPtr LoadLibrary(
				[MarshalAs(UnmanagedType.LPStr)]string lpFileName
			);

			[DllImport("kernel32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool FreeLibrary(IntPtr hModule);

			[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
			public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
		}

		#endregion

		public static IntPtr LoadLibrary(string libPath)
		{
			Verify.IsNotNull(libPath, nameof(libPath));

			IntPtr moduleHandle = Bindings.LoadLibrary(libPath);
			if (moduleHandle == IntPtr.Zero)
			{
				var lastError = Marshal.GetLastWin32Error();
				throw new Exception($"Cannot load Win32 DLL:{libPath}, last error code:{lastError}");
			}
			return moduleHandle;
		}

		public static void FreeLibrary(IntPtr hModule)
		{
			if(!Bindings.FreeLibrary(hModule))
			{
				var lastError = Marshal.GetLastWin32Error();
				throw new Exception($"Cannot free win32 lib, last error code:{lastError}");
			}
		}

		public static IntPtr GetProcAdress(IntPtr libHandle, string procName)
		{
			Verify.IsNotNullPtr(libHandle, nameof(libHandle));
			Verify.IsNotNullOrEmpty(procName, nameof(procName));

			IntPtr procAdress = Bindings.GetProcAddress(libHandle, procName);

			if(procAdress == IntPtr.Zero)
			{
				var lastError = Marshal.GetLastWin32Error();
				throw new Exception(
					$"Cannot load proc:{procName}, last error code:{lastError}");
			}
			return procAdress;
		}
	}
}
