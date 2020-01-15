using System;
using System.Collections.Generic;
using System.Text;

namespace Callisto.Binder.Net
{
	interface IEntryPointSearcher
	{
		IReadOnlyList<FunctionEntryName> EnumerateEntryPoints(string dllPath);
	}
}
