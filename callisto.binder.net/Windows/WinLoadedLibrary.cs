using System;
using System.Runtime.InteropServices;

using Callisto.Binder.Net.Auxiliary;

namespace Callisto.Binder.Net.Windows
{
	public class WinLoadedLibrary : ILoadedLibrary
	{
		#region Properties

		public IntPtr Handle { get; }

		public string LibPath { get; }

		#endregion

		public bool disposed = true;


		public WinLoadedLibrary(string libPath)
		{
			Verify.IsNotNullOrEmpty(libPath, nameof(libPath));
			Handle = Kernel32Wrapper.LoadLibrary(libPath);
			LibPath = libPath;
		}

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		public IntPtr GetFunctionAdress(string functionName)
		{
			Verify.IsNotNull(functionName, nameof(functionName));
			var funcPointer = Kernel32Wrapper.Bindings.GetProcAddress(
				Handle,
				functionName
			);
			if(funcPointer == IntPtr.Zero)
			{
				var lastError = Marshal.GetLastWin32Error();
				throw new Exception(
					$"Not loaded func '{functionName}' from lib '{LibPath}', last error code:{lastError}"
				);
			}
			return funcPointer;
		}

	}
}
