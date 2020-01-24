#ifndef NATIVE_EXAMPLE_C_FUNCS_POINT3F_CLASS_H
#define NATIVE_EXAMPLE_C_FUNCS_POINT3F_CLASS_H

#include <lib_export.h>
#include <data.h>

extern "C" CALLISTO_EXPORT NativeWrap::point3f CALLISTO_CALL 
NativeWrap_NativeClass_Point3fClass_constructPoint3f(
    t_float32 x,
    t_float32 y,
    t_float32 z
);

#endif