namespace Callisto.Binder.Net.Windows
{
	public static class DefineImageConstants
	{
		public static readonly int IMAGE_NUMBEROF_DIRECTORY_ENTRIES = 16;

		public static readonly int IMAGE_SIZEOF_FILE_HEADER = 20;

		public static readonly int IMAGE_FILE_RELOCS_STRIPPED           = 0x0001;  // Relocation info stripped from file.
		public static readonly int IMAGE_FILE_EXECUTABLE_IMAGE          = 0x0002;  // File is executable  (i.e. no unresolved external references).
		public static readonly int IMAGE_FILE_LINE_NUMS_STRIPPED        = 0x0004;  // Line nunbers stripped from file.
		public static readonly int IMAGE_FILE_LOCAL_SYMS_STRIPPED       = 0x0008;  // Local symbols stripped from file.
		public static readonly int IMAGE_FILE_AGGRESIVE_WS_TRIM         = 0x0010;  // Aggressively trim working set
		public static readonly int IMAGE_FILE_LARGE_ADDRESS_AWARE       = 0x0020;  // App can handle >2gb addresses
		public static readonly int IMAGE_FILE_BYTES_REVERSED_LO         = 0x0080;  // Bytes of machine word are reversed.
		public static readonly int IMAGE_FILE_32BIT_MACHINE             = 0x0100;  // 32 bit word machine.
		public static readonly int IMAGE_FILE_DEBUG_STRIPPED            = 0x0200;  // Debugging info stripped from file in .DBG file
		public static readonly int IMAGE_FILE_REMOVABLE_RUN_FROM_SWAP   = 0x0400;  // If Image is on removable media, copy and run from the swap file.
		public static readonly int IMAGE_FILE_NET_RUN_FROM_SWAP         = 0x0800;  // If Image is on Net, copy and run from the swap file.
		public static readonly int IMAGE_FILE_SYSTEM                    = 0x1000;  // System File.
		public static readonly int IMAGE_FILE_DLL                       = 0x2000;  // File is a DLL.
		public static readonly int IMAGE_FILE_UP_SYSTEM_ONLY            = 0x4000;  // File should only be run on a UP machine
		public static readonly int IMAGE_FILE_BYTES_REVERSED_HI         = 0x8000;  // Bytes of machine word are reversed.

		public static readonly int IMAGE_FILE_MACHINE_UNKNOWN            = 0;
		public static readonly int IMAGE_FILE_MACHINE_TARGET_HOST        = 0x0001;  // Useful for indicating we want to interact with the host and not a WoW guest.
		public static readonly int IMAGE_FILE_MACHINE_I386               = 0x014c;  // Intel 386.
		public static readonly int IMAGE_FILE_MACHINE_R3000              = 0x0162;  // MIPS little-endian, 0x160 big-endian
		public static readonly int IMAGE_FILE_MACHINE_R4000              = 0x0166;  // MIPS little-endian
		public static readonly int IMAGE_FILE_MACHINE_R10000             = 0x0168;  // MIPS little-endian
		public static readonly int IMAGE_FILE_MACHINE_WCEMIPSV2          = 0x0169;  // MIPS little-endian WCE v2
		public static readonly int IMAGE_FILE_MACHINE_ALPHA              = 0x0184;  // Alpha_AXP
		public static readonly int IMAGE_FILE_MACHINE_SH3                = 0x01a2;  // SH3 little-endian
		public static readonly int IMAGE_FILE_MACHINE_SH3DSP             = 0x01a3;
		public static readonly int IMAGE_FILE_MACHINE_SH3E               = 0x01a4;  // SH3E little-endian
		public static readonly int IMAGE_FILE_MACHINE_SH4                = 0x01a6;  // SH4 little-endian
		public static readonly int IMAGE_FILE_MACHINE_SH5                = 0x01a8;  // SH5
		public static readonly int IMAGE_FILE_MACHINE_ARM                = 0x01c0;  // ARM Little-Endian
		public static readonly int IMAGE_FILE_MACHINE_THUMB              = 0x01c2;  // ARM Thumb/Thumb-2 Little-Endian
		public static readonly int IMAGE_FILE_MACHINE_ARMNT              = 0x01c4;  // ARM Thumb-2 Little-Endian
		public static readonly int IMAGE_FILE_MACHINE_AM33               = 0x01d3;
		public static readonly int IMAGE_FILE_MACHINE_POWERPC            = 0x01F0;  // IBM PowerPC Little-Endian
		public static readonly int IMAGE_FILE_MACHINE_POWERPCFP          = 0x01f1;
		public static readonly int IMAGE_FILE_MACHINE_IA64               = 0x0200;  // Intel 64
		public static readonly int IMAGE_FILE_MACHINE_MIPS16             = 0x0266;  // MIPS
		public static readonly int IMAGE_FILE_MACHINE_ALPHA64            = 0x0284;  // ALPHA64
		public static readonly int IMAGE_FILE_MACHINE_MIPSFPU            = 0x0366;  // MIPS
		public static readonly int IMAGE_FILE_MACHINE_MIPSFPU16          = 0x0466;  // MIPS
		public static readonly int IMAGE_FILE_MACHINE_AXP64 = IMAGE_FILE_MACHINE_ALPHA64;
		public static readonly int IMAGE_FILE_MACHINE_TRICORE            = 0x0520;  // Infineon
		public static readonly int IMAGE_FILE_MACHINE_CEF                = 0x0CEF;
		public static readonly int IMAGE_FILE_MACHINE_EBC                = 0x0EBC;  // EFI Byte Code
		public static readonly int IMAGE_FILE_MACHINE_AMD64              = 0x8664;  // AMD64 (K8)
		public static readonly int IMAGE_FILE_MACHINE_M32R               = 0x9041;  // M32R little-endian
		public static readonly int IMAGE_FILE_MACHINE_ARM64              = 0xAA64;  // ARM64 Little-Endian
		public static readonly int IMAGE_FILE_MACHINE_CEE                = 0xC0EE;
	}
}
