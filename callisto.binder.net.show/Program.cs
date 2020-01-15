using Callisto.Binder.Net.Windows;
using System;

namespace callisto.binder.net.show
{
	class Program
	{
		static void Main(string[] args)
		{
			var dllPath = "C:\\Users\\User\\Desktop\\dll\\nvinfer.dll";

			var entrySearcher = new WinEntryPointSearcher();

			var entries = entrySearcher.EnumerateEntryPoints(dllPath);

			var library = new WinLoadedLibrary(dllPath);

			foreach(var entry in entries)
			{
				Console.WriteLine($"Mangled:{entry.MangledName}");
				Console.WriteLine($"Signature:{entry.DemangledSignature}");
				Console.WriteLine($"Name:{entry.DemangledName}\n");
			}
		}
	}
}
