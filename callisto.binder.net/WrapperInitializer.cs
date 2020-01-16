using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

using Callisto.Binder.Net.Auxiliary;
using Callisto.Binder.Net.Windows;

namespace Callisto.Binder.Net
{
	public static class WrapperInitializer
	{
		#region Helper

		#endregion

		#region Methods

		private static LibraryPack Load(
			IEnumerable<WinLibRecord> winLibs,
			IEnumerable<UnixLibRecord> unixLibs
		)
		{
			var (osName, arch, bitDesign) = EnvironmentAuxiliary.GetEnvironment();

			var libraryPack = new LibraryPack();

			try
			{
				if (osName == OSName.Windows)
				{
					if(winLibs == null)
					{
						throw new Exception("Not find native libraries for Windows");
					}
					foreach (var winLib in winLibs)
					{
						WinLoadedLibrary loadedLibrary = null;
						try
						{
							var path = winLib.GetLibrary((arch, bitDesign));
							loadedLibrary = new WinLoadedLibrary(path);
							libraryPack.AddLibrary(loadedLibrary);
						}
						catch(Exception exc)
						{
							loadedLibrary?.Dispose();
							throw exc;
						}
					}
				}
				else
				{
					if (unixLibs == null)
					{
						throw new Exception("Not find native libraries for Unix");
					}
					throw new NotImplementedException(
						$"Not implement loading for os '{osName}");
				}
			}
			catch(Exception exc)
			{
				libraryPack.Dispose();
				throw exc;
			}

			return libraryPack;
		}

		public static void BindMethods(
			Type type,
			IEnumerable<Type> parentTypes,
			ILoadedLibrary loadedLibrary,
			IEnumerable<FunctionEntryName> entryPoints
		)
		{
			// concat entry name like "foo::bar::static_class:getBar"
			var resultParentName = "";
			if (parentTypes != null)
			{
				foreach (var parentType in parentTypes)
				{
					resultParentName += $"{parentType.Name}::";
				}
				resultParentName += $"{type.Name}::";
			}
			else parentTypes = new List<Type>();

			// initialize delegates
			foreach(var field in type.GetFields(BindingFlags.Public | BindingFlags.Static))
			{
				if(field.IsInitOnly)
				{
					var entryName = $"{resultParentName}{field.Name}";

					var findEntryPoints = entryPoints.Where(x => x.DemangledName == entryName);
					var findEntryPointsLength = findEntryPoints.Count();

					if(findEntryPointsLength >= 2)
					{
						var stringBuilder = new StringBuilder();
						stringBuilder.Append("Not implement bind build for situation");
						stringBuilder.Append(", when find more two entry points with identical names:\n");
						foreach(var findEntryPoint in findEntryPoints)
						{
							stringBuilder.Append($"{findEntryPoint.DemangledSignature}\n");
						}
						throw new NotImplementedException(stringBuilder.ToString());
					}
					else if(findEntryPointsLength == 0)
					{
						throw new Exception($"Not find entry point for name:{entryName}");
					}

					var entryPtr = loadedLibrary.GetFunctionAdress(findEntryPoints.First().DemangledSignature);
					var method = Marshal.GetDelegateForFunctionPointer(entryPtr, field.FieldType);
					field.SetValue(null, method);
				}
			}

			// initialize childrens scope
			var newParentTypes = parentTypes.ToList();
			newParentTypes.Add(type);

			var nestedClasses = type.GetMembers()
				.Where(	x => x.MemberType == MemberTypes.NestedType &&
						x.Name != "Signatures");

			foreach (var nestedClass in nestedClasses)
			{
				BindMethods(
					nestedClass.DeclaringType,
					newParentTypes,
					loadedLibrary,
					entryPoints);
			}
		}

		public static void Initialize(
			Type type, 
			IEnumerable<WinLibRecord> winLibs,
			IEnumerable<UnixLibRecord> unixLibs,
			bool mismatchThrow = false
		)
		{
			LibraryPack libraryPack = null;

			try
			{
				libraryPack = Load(winLibs, unixLibs);

				var (osName, arch, bitDesing) = EnvironmentAuxiliary.GetEnvironment();
				var entryPointSearcher = EntryPointSearchProvider.EntryPointSearcher(osName);
				var bindingLib = libraryPack.LoadedLibraries.Last();

				var entries = entryPointSearcher.EnumerateEntryPoints(bindingLib.Path);

				BindMethods(
					type,
					null,
					bindingLib,
					entries);

				var libraryPackField = type.GetFields().Where(x => x.Name == "LibraryPack").Single();
				libraryPackField.SetValue(null, libraryPack);

			}
			catch(Exception exc)
			{
				libraryPack?.Dispose();
				throw exc;
			}

		}

		#endregion
	}
}
