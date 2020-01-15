using System.Runtime.InteropServices;


namespace Callisto.Binder.Net.Windows
{
	[StructLayout(LayoutKind.Sequential)]
	public struct ImageOptionalHeader32
	{
		public ushort	Magic;
		public byte		MajorLinkerVersion;
		public byte		MinorLinkerVersion;
		public uint		SizeOfCode;
		public uint		SizeOfInitializedData;
		public uint		SizeOfUninitializedData;
		public uint		AddressOfEntryPoint;
		public uint		BaseOfCode;
		public uint		BaseOfData;

		//
		// NT additional fields.
		//
		public uint		ImageBase;
		public uint		SectionAlignment;
		public uint		FileAlignment;
		public ushort	MajorOperatingSystemVersion;
		public ushort	MinorOperatingSystemVersion;
		public ushort	MajorImageVersion;
		public ushort	MinorImageVersion;
		public ushort	MajorSubsystemVersion;
		public ushort	MinorSubsystemVersion;
		public uint		Win32VersionValue;
		public uint		SizeOfImage;
		public uint		SizeOfHeaders;
		public uint		CheckSum;
		public ushort	Subsystem;
		public ushort	DllCharacteristics;
		public uint		SizeOfStackReserve;
		public uint		SizeOfStackCommit;
		public uint		SizeOfHeapReserve;
		public uint		SizeOfHeapCommit;
		public uint		LoaderFlags;
		public uint		NumberOfRvaAndSizes;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		ImageDataDirectory[] DataDirectory;
	}
}
