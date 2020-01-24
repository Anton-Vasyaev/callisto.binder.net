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
        #region Data

        private static List<string> _reservedInstances = new List<string>()
        {
            "_libraryPack"
        };

        private static List<string> _reservedNestedTypes = new List<string>()
        {
            "Signatures"
        };

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

		#region InitializeCppScopes

		/// <summary>
		/// Bind entry points from native library for static delegates of class 'Type'
		/// </summary>
		/// <param name="type">
		/// Type of static class, representation native lib or C++ scope.
		/// </param>
		/// <param name="parentTypes">
		/// enumeration of parent types of this C++ scope like "parent1::parent2::type",
		/// if parentTypes == null, this type evaluated as native lib (no scope)
		/// </param>
		/// <param name="loadedLibrary">loaded library, from which taken entry points</param>
		/// <param name="entryPoints">enumeration of demangled entry points</param>
		public static void BindMethodsWithScope(
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

                var newParentTypes = parentTypes.ToList();
                newParentTypes.Add(type);
                parentTypes = newParentTypes;
			}
			else parentTypes = new List<Type>();

            // initialize delegates
            var delegates = type.GetProperties(BindingFlags.Public | BindingFlags.Static).Where(
                x => 
                (x.PropertyType.BaseType == typeof(MulticastDelegate) ||
                 x.PropertyType.BaseType == typeof(Delegate)) 
            );
			foreach(var delegateProp in delegates)
			{
				var entryName = $"{resultParentName}{delegateProp.PropertyType.Name}";

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

				var entryPtr = loadedLibrary.GetFunctionAdress(findEntryPoints.First().MangledName);
				var method = Marshal.GetDelegateForFunctionPointer(entryPtr, delegateProp.PropertyType);
				delegateProp.SetValue(null, method);
			}

            // recursive initializing of nested clasess (types), which is a present C++ scope
            var nestedClasses = type.GetNestedTypes().Where(x => !_reservedNestedTypes.Contains(x.Name));

			foreach (var nestedType in nestedClasses)
			{
				BindMethodsWithScope(
					nestedType,
					parentTypes,
					loadedLibrary,
					entryPoints);
			}

            // recursive initializing of instances, whose types is a present C++ scope
            var instanceProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Static).Where(
                x => 
                x.PropertyType != typeof(LibraryPack) &&
                x.PropertyType.BaseType != typeof(Delegate) &&
                x.PropertyType.BaseType != typeof(MulticastDelegate) 
            );
            foreach (var instanceProperty in instanceProperties)
            {
                var scopeOb = Activator.CreateInstance(instanceProperty.PropertyType);
                instanceProperty.SetValue(null, scopeOb);
                BindMethodsWithScope(
                    scopeOb,
                    parentTypes,
                    loadedLibrary,
                    entryPoints
                );
            }
        }

        public static void BindMethodsWithScope(
            object scopeObject,
            IEnumerable<Type> parentTypes,
            ILoadedLibrary loadedLibrary,
            IEnumerable<FunctionEntryName> entryPoints
        )
        {
            var scopeType = scopeObject.GetType();
            // concat entry name like "foo::bar::static_class:getBar"
            var resultParentName = "";
            if (parentTypes != null)
            {
                foreach (var parentType in parentTypes)
                {
                    resultParentName += $"{parentType.Name}::";
                }
                resultParentName += $"{scopeType.Name}::";

                var newParentTypes = parentTypes.ToList();
                newParentTypes.Add(scopeType);
                parentTypes = newParentTypes;
            }
            else parentTypes = new List<Type>();

            // initialize delegates
            var delegateProps = scopeType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(
                x => 
                (x.PropertyType.BaseType == typeof(MulticastDelegate) ||
                 x.PropertyType.BaseType == typeof(Delegate))
            );
            foreach (var delegateProp in delegateProps)
            {
                var entryName = $"{resultParentName}{delegateProp.PropertyType.Name}";

                var findEntryPoints = entryPoints.Where(x => x.DemangledName == entryName);
                var findEntryPointsLength = findEntryPoints.Count();

                if (findEntryPointsLength >= 2)
                {
                    var stringBuilder = new StringBuilder();
                    stringBuilder.Append("Not implement bind build for situation");
                    stringBuilder.Append(", when find more two entry points with identical names:\n");
                    foreach (var findEntryPoint in findEntryPoints)
                    {
                        stringBuilder.Append($"{findEntryPoint.DemangledSignature}\n");
                    }
                    throw new NotImplementedException(stringBuilder.ToString());
                }
                else if (findEntryPointsLength == 0)
                {
                    throw new Exception($"Not find entry point for name:{entryName}");
                }

                var entryPtr = loadedLibrary.GetFunctionAdress(findEntryPoints.First().MangledName);
                var method = Marshal.GetDelegateForFunctionPointer(entryPtr, delegateProp.PropertyType);
                delegateProp.SetValue(scopeObject, method);
            }

            // recursive initializing of instances, whose types is a present C++ scope
            var instanceProperties = scopeType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(
                x => 
                x.PropertyType != typeof(LibraryPack) &&
                x.PropertyType.BaseType != typeof(Delegate) &&
                x.PropertyType.BaseType != typeof(MulticastDelegate) 
            );
            foreach (var instanceProperty in instanceProperties)
            {
                var scopeOb = Activator.CreateInstance(instanceProperty.PropertyType);
                instanceProperty.SetValue(scopeObject, scopeOb);
                BindMethodsWithScope(
                    scopeOb,
                    parentTypes,
                    loadedLibrary,
                    entryPoints
                );
            }
        }

        /// <summary>
        /// Initialize Type's, delegates and nested classes entry points from
        /// dynamic libs (so, dll), where entry points mangled name contain C++ scope
        /// (namespaces, classes and others) like 
        /// nestedClassType::nestedClassType2::entry_function_name.
        /// </summary>
        /// <param name="type">
        /// Top Level Type, who contain scopes in static nested classes (Types) or
        /// instances, but type not present a C++ scope
        /// </param>
        /// <param name="winLibs">
        /// list of windows pathes to dynamic libs, who need loading before working logic of Type
        /// will start to work. order of dynamic libs loading determined by order of in pathe's list.
        /// </param>
        /// <param name="unixLibs">
        /// list of unix (include linux) pathes to dynamic libs, who need loading before working logic of Type
        /// will start to work. Order of dynamic libs loading determined by order of in pathes list.
        /// </param>
        public static void InitializeWithScope(
			Type type, 
			IEnumerable<WinLibRecord> winLibs,
			IEnumerable<UnixLibRecord> unixLibs
		)
		{
            // validation
            var libraryPackField = type.GetField("_libraryPack", BindingFlags.NonPublic | BindingFlags.Static);

            if (libraryPackField == null || libraryPackField.FieldType != typeof(LibraryPack)) throw new Exception(
                $"${type} not has static field _libraryPack or type of _libraryPack is not {nameof(LibraryPack)}"
            );

            LibraryPack libraryPack = null;

			try
			{
				libraryPack = Load(winLibs, unixLibs);

				var (osName, arch, bitDesing) = EnvironmentAuxiliary.GetEnvironment();
				var entryPointSearcher = EntryPointSearchProvider.EntryPointSearcher(osName);
				var bindingLib = libraryPack.LoadedLibraries.Last();

				var entries = entryPointSearcher.EnumerateEntryPoints(bindingLib.Path);

				BindMethodsWithScope(
					type,
					null,
					bindingLib,
					entries);

                libraryPackField.SetValue(null, libraryPack);
			}
			catch(Exception exc)
			{
				libraryPack?.Dispose();
				throw exc;
			}

		}

        #endregion

        #region InitializeC

        /// <summary>
        /// Bind entry points from native library for static delegates of class 'Type' and
        /// he's instances
        /// </summary>
        /// <param name="type">
        /// Type of static class
        /// </param>
        /// <param name="loadedLibrary">loaded library, from which taken entry points</param>
        public static void BindMethods(
            Type type,
            ILoadedLibrary loadedLibrary
        )
        {
            // initialize delegates
            var delegates = type.GetProperties(BindingFlags.Public | BindingFlags.Static).Where(
                x =>
                (x.PropertyType.BaseType == typeof(MulticastDelegate) ||
                 x.PropertyType.BaseType == typeof(Delegate))
            );
            foreach (var delegateProp in delegates)
            {
                var entryName = delegateProp.PropertyType.Name;

                var entryPtr = loadedLibrary.GetFunctionAdress(entryName);
                var method = Marshal.GetDelegateForFunctionPointer(entryPtr, delegateProp.PropertyType);
                delegateProp.SetValue(null, method);
            }

            // recursive initializing of nested clasess (types), which is a present C++ scope
            var nestedClasses = type.GetNestedTypes().Where(x => !_reservedNestedTypes.Contains(x.Name));

            foreach (var nestedType in nestedClasses) BindMethods(nestedType, loadedLibrary);

            // recursive initializing of instances, whose types is a present C++ scope
            var instanceProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Static).Where(
                x =>
                x.PropertyType != typeof(LibraryPack) &&
                x.PropertyType.BaseType != typeof(Delegate) &&
                x.PropertyType.BaseType != typeof(MulticastDelegate)
            );
            foreach (var instanceProperty in instanceProperties)
            {
                var scopeOb = Activator.CreateInstance(instanceProperty.PropertyType);
                instanceProperty.SetValue(null, scopeOb);
                BindMethods(
                    scopeOb,
                    loadedLibrary
                );
            }
        }

        public static void BindMethods(
            object scopeObject,
            ILoadedLibrary loadedLibrary
        )
        {
            var scopeType = scopeObject.GetType();

            // initialize delegates
            var delegateProps = scopeType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(
                x =>
                (x.PropertyType.BaseType == typeof(MulticastDelegate) ||
                 x.PropertyType.BaseType == typeof(Delegate))
            );
            foreach (var delegateProp in delegateProps)
            {
                var entryName = delegateProp.PropertyType.Name;
                var entryPtr = loadedLibrary.GetFunctionAdress(entryName);
                var method = Marshal.GetDelegateForFunctionPointer(entryPtr, delegateProp.PropertyType);
                delegateProp.SetValue(scopeObject, method);
            }

            // recursive initializing of instances, whose types is a present C++ scope
            var instanceProperties = scopeType.GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(
                x =>
                x.PropertyType != typeof(LibraryPack) &&
                x.PropertyType.BaseType != typeof(Delegate) &&
                x.PropertyType.BaseType != typeof(MulticastDelegate)
            );
            foreach (var instanceProperty in instanceProperties)
            {
                var scopeOb = Activator.CreateInstance(instanceProperty.PropertyType);
                instanceProperty.SetValue(scopeObject, scopeOb);
                BindMethods(
                    scopeOb,
                    loadedLibrary
                );
            }
        }

        /// <summary>
        /// Initialize Type's, delegates and nested classes entry points from
        /// dynamic libs (so, dll) with extern "C" names without mangling
        /// </summary>
        /// <param name="type">
        /// Type of static class
        /// </param>
        /// <param name="winLibs">
        /// list of windows pathes to dynamic libs, who need loading before working logic of Type
        /// will start to work. order of dynamic libs loading determined by order of in pathe's list.
        /// </param>
        /// <param name="unixLibs">
        /// list of unix (include linux) pathes to dynamic libs, who need loading before working logic of Type
        /// will start to work. Order of dynamic libs loading determined by order of in pathes list.
        /// </param>
        public static void Initialize(
            Type type,
            IEnumerable<WinLibRecord> winLibs,
            IEnumerable<UnixLibRecord> unixLibs
        )
        {
            // validation
            var libraryPackField = type.GetField("_libraryPack", BindingFlags.NonPublic | BindingFlags.Static);

            if (libraryPackField == null || libraryPackField.FieldType != typeof(LibraryPack)) throw new Exception(
                $"${type} not has static field _libraryPack or type of _libraryPack is not {nameof(LibraryPack)}"
            );

            LibraryPack libraryPack = null;

            try
            {
                libraryPack = Load(winLibs, unixLibs);

                var (osName, arch, bitDesing) = EnvironmentAuxiliary.GetEnvironment();
                var bindingLib = libraryPack.LoadedLibraries.Last();

                BindMethods(
                    type,
                    bindingLib
                );

                libraryPackField.SetValue(null, libraryPack);
            }
            catch (Exception exc)
            {
                libraryPack?.Dispose();
                throw exc;
            }

        }

        #endregion

        #endregion
    }
}
