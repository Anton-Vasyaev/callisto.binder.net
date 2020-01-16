using System;

using Callisto.Binder.Net.Windows;

namespace Callisto.Binder.Net
{
	public static class EntryPointSearchProvider
	{
		public static IEntryPointSearcher EntryPointSearcher(OSName osName)
		{
			switch(osName)
			{
				case OSName.Windows:
					return new WinEntryPointSearcher();
				default:
					throw new NotImplementedException($"Not find EntryPointSearcher for os '{osName}");
			}
		}
	}
}
