using System.Collections.Generic;

using Callisto.Binder.Net.Windows;

namespace Callisto.Binder.Net.Auxiliary
{
	static class Demangle
	{
		public static string DemangleMSVC(string mangledName, IEnumerable<UndName> flags)
		{
			var demangledBytes = new byte[1024];

			uint resultFlag = 0;

			foreach(var flag in flags)
			{
				resultFlag |= (uint)flag;
			}

			while (true)
			{
				var resultLength = ImageHlpWrapper.Bindings.UnDecorateSymbolName(
					mangledName,
					demangledBytes,
					(uint)demangledBytes.Length,
					(uint)resultFlag
				);
				if(resultLength == (demangledBytes.Length - 2))
				{
					demangledBytes = new byte[demangledBytes.Length * 2];
					continue;
				}
				else
				{
					return demangledBytes.DecodeASCIIWithNull();
				}
			}
		}
	}
}
