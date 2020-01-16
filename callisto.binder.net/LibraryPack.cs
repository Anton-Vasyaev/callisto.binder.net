using System;
using System.Collections.Generic;
using System.Text;

namespace Callisto.Binder.Net
{
	public class LibraryPack : IDisposable
	{
		#region Properties

		public bool IsDisposed { get; private set; } = false;

		public IReadOnlyList<ILoadedLibrary> LoadedLibraries => _loadedLibraries;

		#endregion

		#region Data

		private List<ILoadedLibrary> _loadedLibraries = new List<ILoadedLibrary>();

		#endregion


		#region ConstructAndDestruct

		~LibraryPack()
		{
			Dispose(false);
		}

		protected virtual void Dispose(bool disposing)
		{
			if(!IsDisposed)
			{
				foreach(var lib in _loadedLibraries)
				{
					lib.Dispose();
				}
				IsDisposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		#endregion

		#region Methods

		public void AddLibrary(ILoadedLibrary library)
		{
			_loadedLibraries.Add(library);
		}

		#endregion
	}
}
