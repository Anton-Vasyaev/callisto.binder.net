using System;
using System.Runtime.InteropServices;

namespace Callisto.Binder.Net.Windows
{
	public static class ImageHlpWrapper
	{
		public static class Bindings
		{
			[DllImport("imagehlp.dll", CallingConvention = CallingConvention.StdCall)]
			public static extern uint /* DWORD */ UnDecorateSymbolName(
				[MarshalAs(UnmanagedType.LPStr)]string name,    // PCSTR
				byte[] outputString,                            // PSTR
				uint maxStringLength,                           // DWORD
				uint flags                                      // DWORD
			);


			[DllImport("imagehlp.dll", CallingConvention = CallingConvention.StdCall)]
			public static extern int /* BOOL */ MapAndLoad(
				[MarshalAs(UnmanagedType.LPStr)] string ImageName,  // PCSTR
				[MarshalAs(UnmanagedType.LPStr)] string DllPath,    // PSTR
				out LoadedImage loadedImage,                        // PLOADED_IMAGE
				int DotDll,                                         // BOOL
				int ReadOnly                                        // BOOL
			);


			[DllImport("imagehlp.dll", CallingConvention = CallingConvention.StdCall)]
			public static extern int /* BOOL */ UnMapAndLoad(
				out LoadedImage loadedImage // PLOADED_IMAGE
			);


			[DllImport("imagehlp.dll", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr /* PVOID */ ImageDirectoryEntryToData(
				IntPtr Base,                // PVOID
				byte MappedAsImage,         // BOOLEAN
				ushort DirectoryEntry,      // USHORT
				out ulong Size              // PULONG
			);


			[DllImport("imagehlp.dll", CallingConvention = CallingConvention.StdCall)]
			public static extern IntPtr /* PVOID */ ImageRvaToVa(
				IntPtr NtHeaders,       // PIMAGE_NT_HEADERS
				IntPtr Base,            // PVOID
				ulong Rva,              // ULONG
				IntPtr LastRvaSection   // PIMAGE_SECTION_HEADER*
			);
		}
	}
}
