using System;
using System.Collections.Generic;
using System.Text;

namespace Callisto.Binder.Net
{
	public struct FunctionEntryName
	{
		public string MangledName { get; }

		public string DemangledSignature { get; }

		public string DemangledName { get; }

		public FunctionEntryName(
			string mangledName,
			string demangledSignature,
			string demangledName
		)
		{
			MangledName = mangledName;
			DemangledSignature = demangledSignature;
			DemangledName = demangledName;
		}
	}
}
