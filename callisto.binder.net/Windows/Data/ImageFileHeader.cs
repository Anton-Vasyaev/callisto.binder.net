using System.Runtime.InteropServices;

namespace Callisto.Binder.Net.Windows
{
	[StructLayout(LayoutKind.Sequential)]
	public struct ImageFileHeader
	{
		public ushort Machine;
		public ushort NumberOfSections;
		public uint TimeDateStamp;
		public uint PointerToSymbolTable;
		public uint NumberOfSymbols;
		public ushort SizeOfOptionalHeader;
		public ushort Characteristics;
	}
}
