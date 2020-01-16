using System;
using System.Collections.Generic;
using System.Text;

namespace Callisto.Binder.Net
{
	public interface ILoadedLibrary : IDisposable
	{
		string Path { get; }

		bool IsDisposed { get; }

		IntPtr GetFunctionAdress(string functionName);
	}
}
