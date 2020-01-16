using System;
using System.Runtime.InteropServices;

using Callisto.Binder.Net.Auxiliary;

namespace Callisto.Binder.Net.Windows
{
	public class WinLoadedLibrary : ILoadedLibrary
	{
		#region Properties

		public IntPtr Handle { get; }

		public string Path { get; }

		public bool IsDisposed { get; private set; }

		#endregion

		#region Data

		public bool disposed = true;

		#endregion

		#region ConstructAndDestruct

		public WinLoadedLibrary(string libPath)
		{
			Verify.IsNotNullOrEmpty(libPath, nameof(libPath));
			Handle = Kernel32Wrapper.LoadLibrary(libPath);
			Path = libPath;
		}

		~WinLoadedLibrary()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if(!IsDisposed)
			{
				Kernel32Wrapper.FreeLibrary(Handle);

				IsDisposed = true;
			}
		}

		#endregion

		#region Methods

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
					$"Not loaded func '{functionName}' from lib '{Path}', last error code:{lastError}"
				);
			}
			return funcPointer;
		}

		#endregion
	}
}
