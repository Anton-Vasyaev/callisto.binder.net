#ifndef NATIVE_EXAMPLE_FUNCS_LIB_H
#define NATIVE_EXAMPLE_FUNCS_LIB_H

#include <lib_export.h>
#include <types.h>


extern "C" CALLISTO_EXPORT t_float32 CALLISTO_CALL 
nativeMul(
	t_float32 value1, 
	t_float32 value2
);


#endif