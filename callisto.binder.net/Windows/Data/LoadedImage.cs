using System;
using System.Collections.Generic;
using System.Text;

namespace Callisto.Binder.Net.Windows
{
	public struct LoadedImage
	{
		public IntPtr		ModuleName;			// PSTR
		public IntPtr		hFile;				// HANDLE
		public IntPtr		MappedAddress;		// PUCHAR

		public IntPtr		FileHeader;			// PIMAGE_NT_HEADERS*POINTER_SIZE*

		public IntPtr		LastRvaSection;		// PIMAGE_SECTION_HEADER
		public ulong		NumberOfSections;	// ULONG
		public IntPtr		Sections;			// PIMAGE_SECTION_HEADER
		public ulong		Characteristics;	// ULONG
		public byte			fSystemImage;		// BOOLEAN
		public byte			fDOSImage;			// BOOLEAN
		public byte			fReadOnly;			// BOOLEAN
		public byte			Version;			// UCHAR
		public ListEntry	Links;				// LIST_ENTRY
		public ulong		SizeOfImage;		// ULONG

	}
}
