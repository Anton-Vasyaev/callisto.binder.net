#ifndef NATIVE_EXAMPLE_C_FUNCS_NATIVE_WRAP_H
#define NATIVE_EXAMPLE_C_FUNCS_NATIVE_WRAP_H

#include <lib_export.h>
#include <types.h>

#include "NativeWrapNested/FuncsNativeClass.h"


extern "C" CALLISTO_EXPORT t_float32 CALLISTO_CALL 
NativeWrap_nativePow(
	t_float32 value, 
	t_float32 pow_value
);

#endif