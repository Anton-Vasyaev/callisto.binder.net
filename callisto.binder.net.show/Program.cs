using System;
using System.Linq;
using System.Reflection;

namespace callisto.binder.net.show
{
	class Program
	{

		static void ShowTypeEnumerate(Type type)
		{
			var field = type.GetFields().Where(x => x.Name == "LibraryPack").Single();

			Console.WriteLine($"Name:{field.Name}");
			Console.WriteLine($"Flags:");
			type = field.DeclaringType;
			Console.WriteLine($"Type:{type}");
			Console.WriteLine($"Member type:{field.MemberType}");
		}

		static void Main(string[] args)
		{
			ShowTypeEnumerate(typeof(NativeExampleLib));
		}
	}
}
