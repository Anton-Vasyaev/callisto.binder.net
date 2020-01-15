using System;
using System.Collections.Generic;
using System.Text;

namespace Callisto.Binder.Net.Data
{
	enum RTLD
	{
		LAZY			= 0x00001,	/* Lazy function call binding.  */
		NOW				= 0x00002,	/* Immediate function call binding.  */
		BINDING_MASK	= 0x3,		/* Mask of binding time value.  */
		NOLOAD			= 0x00004,	/* Do not load the object.  */
		DEEPBIND		= 0x00008,	/* Use deep binding.  */

		/* If the following bit is set in the MODE argument to `dlopen',
		   the symbols of the loaded object and its dependencies are made
		   visible as if the object were linked directly into the program.  */
		GLOBAL			= 0x00100,
		/* Unix98 demands the following flag which is the inverse to RTLD_GLOBAL.
		   The implementation does this by default and so we can define the
		   value to zero.  */
		RTLD_LOCAL		= 0,
		/* Do not delete object when closed.  */
		RTLD_NODELETE	= 0x01000
	}
}
