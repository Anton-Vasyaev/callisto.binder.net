using System;
using System.Collections.Generic;
using System.Text;

namespace Callisto.Binder.Net.Auxiliary
{
	static class EncondingAuxiliary
	{
		public static string DecodeASCIIWithNull(this byte[] buffer)
		{
			int count = Array.IndexOf<byte>(buffer, 0, 0);
			if (count < 0) count = buffer.Length;
			return Encoding.ASCII.GetString(buffer, 0, count);
		}
	}
}
