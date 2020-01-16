using System;
using System.Collections.Generic;
using System.Text;

namespace Callisto.Binder.Net
{
	public interface IEntryPointSearcher
	{
		IReadOnlyList<FunctionEntryName> EnumerateEntryPoints(string dllPath);
	}
}
