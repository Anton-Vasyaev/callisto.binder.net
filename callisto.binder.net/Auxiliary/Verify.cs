using System;

namespace Callisto.Binder.Net.Auxiliary
{
	static class Verify
	{
		public static void IsNotNull(object reference, string name)
		{
			if(reference == null)
			{
				throw new ArgumentException($"{name} was null");
			}
		}

		public static void IsNotNullPtr(IntPtr pointer, string name)
		{
			if(pointer == IntPtr.Zero)
			{
				throw new ArgumentException($"pointer {name} was nullptr");
			}
		}

		public static void IsNotNullOrEmpty(string str, string name)
		{
			if(string.IsNullOrEmpty(str))
			{
				throw new ArgumentException($"{name} wass null or empty");
			}
		}
	}
}
