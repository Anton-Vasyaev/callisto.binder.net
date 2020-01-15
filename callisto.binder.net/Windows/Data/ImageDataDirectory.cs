using System.Runtime.InteropServices;

namespace Callisto.Binder.Net.Windows
{
	[StructLayout(LayoutKind.Sequential)]
	public struct ImageDataDirectory
	{
		public uint VirtualAddress;
		public uint Size;
	}
}
