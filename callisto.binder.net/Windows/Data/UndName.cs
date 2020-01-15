using System;
using System.Collections.Generic;
using System.Text;

namespace Callisto.Binder.Net.Windows
{
	public enum UndName
	{
		COMPLETE = (0x0000),  // Enable full undecoration
		NO_LEADING_UNDERSCORES = (0x0001),  // Remove leading underscores from MS extended keywords
		NO_MS_KEYWORDS = (0x0002),  // Disable expansion of MS extended keywords
		NO_FUNCTION_RETURNS = (0x0004),  // Disable expansion of return type for primary declaration
		NO_ALLOCATION_MODEL = (0x0008),  // Disable expansion of the declaration model
		NO_ALLOCATION_LANGUAGE = (0x0010),  // Disable expansion of the declaration language specifier
		NO_MS_THISTYPE = (0x0020),  // NYI Disable expansion of MS keywords on the 'this' type for primary declaration
		NO_CV_THISTYPE = (0x0040),  // NYI Disable expansion of CV modifiers on the 'this' type for primary declaration
		NO_THISTYPE = (0x0060),  // Disable all modifiers on the 'this' type
		NO_ACCESS_SPECIFIERS = (0x0080),  // Disable expansion of access specifiers for members
		NO_THROW_SIGNATURES = (0x0100),  // Disable expansion of 'throw-signatures' for functions and pointers to functions
		NO_MEMBER_TYPE = (0x0200),  // Disable expansion of 'static' or 'virtual'ness of members
		NO_RETURN_UDT_MODEL = (0x0400),  // Disable expansion of MS model for UDT returns
		P32_BIT_DECODE = (0x0800),  // Undecorate 32-bit decorated names
		NAME_ONLY = (0x1000),  // Crack only the name for primary declaration;
							   //  return just [scope::]name.  Does expand template params
		NO_ARGUMENTS = (0x2000),  // Don't undecorate arguments to function
		NO_SPECIAL_SYMS = (0x4000)  // Don't undecorate special names (v-table, vcall, vector xxx, metatype, etc)
	}
}
