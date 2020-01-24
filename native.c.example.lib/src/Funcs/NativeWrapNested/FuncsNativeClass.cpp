// parent header
#include <Funcs/NativeWrapNested/FuncsNativeClass.h>
// std
#include <memory>


void NativeWrap_NativeClass_copyBytes(
    t_byte* src,
    t_byte* dst,
    t_size size
)
{
    std::memcpy(dst, src, size);
}