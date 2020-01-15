using System;
using System.Collections.Generic;
using System.Text;

namespace Callisto.Binder.Net
{
	interface ILoadedLibrary : IDisposable
	{
		IntPtr GetFunctionAdress(string functionName);
	}
}
