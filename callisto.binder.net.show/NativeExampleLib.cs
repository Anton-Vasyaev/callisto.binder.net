using System;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;

using Callisto.Binder.Net;

namespace callisto.binder.net.show
{
	public static class NativeExampleLib
	{
		#region Data

		public static LibraryPack LibraryPack = null;

		#endregion

		#region ConstructAndDestruct

		static NativeExampleLib()
		{
			string dllPath = null;
			string assemblyLocation = Assembly.GetExecutingAssembly().Location;
			if (assemblyLocation != null) dllPath = Path.GetDirectoryName(assemblyLocation);
			else dllPath = Environment.CurrentDirectory;

			var winLibRecords = new WinLibRecord[]
			{
				new WinLibRecord(
					"nativeexample",
					new ((PlatformArch, BitDesign), string path)[]
					{
						((PlatformArch.x86, BitDesign.x32), Path.Combine(dllPath, "nativeexample_x32.dll")),
						((PlatformArch.x86, BitDesign.x64), Path.Combine(dllPath, "nativeexample_x64.dll")),
					})
			};

			WrapperInitializer.Initialize(
				typeof(NativeExampleLib),
				winLibRecords,
				null
			);
		}

		public static void Initialize()
		{
			// call static constructor
		}

		#endregion

		public static class Signatures
		{
			[SuppressUnmanagedCodeSecurity]
			[UnmanagedFunctionPointer(CallingConvention.Cdecl, SetLastError = false)]
			public delegate float native_pow(float value, float pow_value);
		}

		#region Methods

		public static readonly Signatures.native_pow native_pow;

		#endregion


		#region ScopeChildrens

		#region NativeWrap

		public static class NativeWrap
		{
			public static class Signatures
			{

			}

			#region Methods

			#endregion

			#region NativeClass

			public static class NativeClass
			{
				public static class Signatures
				{

				}

				#region Methods

				#endregion
			}

			#endregion
		}

		#endregion

		#endregion
	}
}
