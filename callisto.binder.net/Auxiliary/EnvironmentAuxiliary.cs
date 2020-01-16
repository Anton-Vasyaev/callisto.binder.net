using System;
using System.Runtime.InteropServices;

namespace Callisto.Binder.Net.Auxiliary
{
	public static class EnvironmentAuxiliary
	{
		public static (OSName, PlatformArch, BitDesign) GetEnvironment()
		{
			OSName os;
			PlatformArch arch;
			BitDesign bitDesign;

			switch(Environment.OSVersion.Platform)
			{
				case PlatformID.Win32NT:
					os = OSName.Windows;
					break;
				case PlatformID.Unix:
					os = OSName.Unix;
					break;
				case PlatformID.MacOSX:
					os = OSName.OSX;
					break;
				default:
					os = OSName.None;
					break;
			}

			switch(RuntimeInformation.ProcessArchitecture)
			{
				case Architecture.X64:
				case Architecture.X86:
					arch = PlatformArch.x86;
					break;
				case Architecture.Arm:
				case Architecture.Arm64:
					arch = PlatformArch.ARM;
					break;
				default:
					arch = PlatformArch.None;
					break;
			}

			bitDesign = Environment.Is64BitProcess ? BitDesign.x64 : BitDesign.x32;

			return (os, arch, bitDesign);
		}
	}
}
