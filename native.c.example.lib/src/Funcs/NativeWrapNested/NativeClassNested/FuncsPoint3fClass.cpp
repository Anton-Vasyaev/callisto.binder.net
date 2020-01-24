// parent header
#include <Funcs/NativeWrapNested/NativeClassNested/FuncsPoint3fClass.h>

NativeWrap::point3f
NativeWrap_NativeClass_Point3fClass_constructPoint3f(
    t_float32 x,
    t_float32 y,
    t_float32 z
)
{
    return NativeWrap::point3f{ x, y, z };
}