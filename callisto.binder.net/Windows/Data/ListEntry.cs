using System;
using System.Runtime.InteropServices;

namespace Callisto.Binder.Net.Windows
{
	[StructLayout(LayoutKind.Sequential)]
	public struct ListEntry
	{
		public IntPtr Flink; // LIST_ENTRY
		public IntPtr Blink; // LIST_ENTRY
	}
}
