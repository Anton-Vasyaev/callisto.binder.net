#ifndef NATIVE_EXAMPLE_C_FUNCS_NATIVE_CLASS_H
#define NATIVE_EXAMPLE_C_FUNCS_NATIVE_CLASS_H

#include <lib_export.h>
#include <types.h>

#include "NativeClassNested/FuncsPoint3fClass.h"


extern "C" CALLISTO_EXPORT void CALLISTO_CALL 
NativeWrap_NativeClass_copyBytes(
    t_byte* src,
    t_byte* dst,
    t_size size
);

#endif