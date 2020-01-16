using System;
using System.Collections.Generic;
using System.Text;

namespace Callisto.Binder.Net
{
	public class UnixLibRecord
	{
		#region Properties

		public IReadOnlyDictionary<
			(PlatformArch arch, BitDesign bitDesign), 
			(string path, RTLD flag)> Libs => _libs;

		#endregion

		#region Data

		private Dictionary<(PlatformArch arch, BitDesign bitDesign), (string path, RTLD flag)> _libs =
			new Dictionary<(PlatformArch arch, BitDesign bitDesign), (string path, RTLD flag)>();

		#endregion


		#region .ctor

		public UnixLibRecord(
			string libIdentification,
			IEnumerable<((PlatformArch arch, BitDesign bitDesign) system, (string path, RTLD flag) libPath)> libs
		)
		{
			foreach (var lib in libs) _libs.Add(lib.system, lib.libPath);
		}

		#endregion

		#region Methods

		public (string path, RTLD flag) GetLibrary((PlatformArch arch, BitDesign bitDesign) system)
		{
			if (_libs.ContainsKey(system)) return _libs[system];
			else throw new Exception(
				$"Not find unix library for arch '{system.arch}' and bit design '{system.bitDesign}");
		}

		#endregion
	}
}
