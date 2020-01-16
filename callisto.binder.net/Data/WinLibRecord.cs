using System;
using System.Collections.Generic;
using System.Text;

namespace Callisto.Binder.Net
{
	public class WinLibRecord
	{
		#region Properties

		public IReadOnlyDictionary<
			(PlatformArch arch, BitDesign bitDesign),
			string> Libs => _libs;

		#endregion

		#region Data

		private Dictionary<(PlatformArch arch, BitDesign bitDesign), string> _libs =
			new Dictionary<(PlatformArch arch, BitDesign bitDesign), string>();

		#endregion


		#region .ctor

		public WinLibRecord(
			string libIdentification,
			IEnumerable<((PlatformArch arch, BitDesign bitDesign) system, string path)> libs
		)
		{
			foreach (var lib in libs) _libs.Add(lib.system, lib.path);
		}

		#endregion

		#region Methods

		public string GetLibrary((PlatformArch arch, BitDesign bitDesign) system)
		{
			if (_libs.ContainsKey(system)) return _libs[system];
			else throw new Exception(
				$"Not find win library for arch '{system.arch}' and bit design '{system.bitDesign}");
		}

		#endregion
	}
}
