using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using Callisto.Binder.Net.Auxiliary;

namespace Callisto.Binder.Net.Windows
{
	public class WinEntryPointSearcher : IEntryPointSearcher
	{
		public unsafe IReadOnlyList<FunctionEntryName> EnumerateEntryPoints(string dllPath)
		{
			var entries = new List<FunctionEntryName>();

			int count = 0;
			IntPtr dNameRVAsPtr = IntPtr.Zero; // DWORD*
			IntPtr imageExportDirectoryPtr; // IMAGE_EXPORT_DIRECTORY*
			ulong cDirSize;
			LoadedImage loadedImage;

			var unmangledNameBytes = new byte[1024];

			if (ImageHlpWrapper.Bindings.MapAndLoad(
					dllPath, 
					null, 
					out loadedImage, 
					1, 
					1) == 1
			)
			{
				try
				{
					imageExportDirectoryPtr = ImageHlpWrapper.Bindings.ImageDirectoryEntryToData(
						loadedImage.MappedAddress,
						0, // false
						0, //IMAGE_DIRECTORY_ENTRY_EXPORT,
						out cDirSize
					);

					var imageExportDirectory = (ImageExportDirectory*)imageExportDirectoryPtr;
					if (imageExportDirectoryPtr != IntPtr.Zero)
					{
						var dNameRVAs = (uint*)ImageHlpWrapper.Bindings.ImageRvaToVa(
							loadedImage.FileHeader,
							loadedImage.MappedAddress,
							imageExportDirectory->AddressOfNames,
							IntPtr.Zero
						);
						for (int i = 0; i < imageExportDirectory->NumberOfNames; i++)
						{
							var fnamePtr = ImageHlpWrapper.Bindings.ImageRvaToVa(
								loadedImage.FileHeader,
								loadedImage.MappedAddress,
								dNameRVAs[i],
								IntPtr.Zero
							);
							var functionName = Marshal.PtrToStringAnsi(fnamePtr);
							var demangledSignature = Demangle.DemangleMSVC(
								functionName, 
								new UndName[]{ UndName.COMPLETE}
							);
							var demangledName = Demangle.DemangleMSVC(
								functionName,
								new UndName[] { UndName.NAME_ONLY }
							);
							entries.Add(new FunctionEntryName(
								functionName,
								demangledSignature,
								demangledName));

						}
					}
				}
				finally
				{
					ImageHlpWrapper.Bindings.UnMapAndLoad(out loadedImage);
				}
			}

			return entries;
		}
	}
}
