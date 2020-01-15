using System.Runtime.InteropServices;

namespace Callisto.Binder.Net.Windows
{
	[StructLayout(LayoutKind.Sequential)]
	public struct ImageExportDirectory
	{
		public uint Characteristics;
		public uint TimeDateStamp;
		public ushort MajorVersion;
		public ushort MinorVersion;
		public uint Name;
		public uint Base;
		public uint NumberOfFunctions;
		public uint NumberOfNames;
		public uint AddressOfFunctions;     // RVA from base of image
		public uint AddressOfNames;         // RVA from base of image
		public uint AddressOfNameOrdinals;  // RVA from base of image
	}
}
