#ifndef NATIVE_EXAMPLE_HPP
#define NATIVE_EXAMPLE_HPP

#include <lib_export.h>
#include <types.h>

namespace NativeWrap
{
    struct point3f
    {
        t_float32 x;
        t_float32 y;
        t_float32 z;
    };
    
    class CALLISTO_EXPORT NativeClass
    {
    public:
        static void CALLISTO_CALL copy_bytes(
            t_byte* src,
            t_byte* dst,
            t_size size
        );

        class CALLISTO_EXPORT Point3fClass
        {
        public:
            static point3f CALLISTO_CALL construct_point3f(
                t_float32 x,
                t_float32 y,
                t_float32 z
            );
        };
    };

	CALLISTO_EXPORT t_float32 CALLISTO_CALL native_pow(t_float32 value, t_int32 pow_value);
}


extern "C"
{
	CALLISTO_EXPORT t_float32 CALLISTO_CALL native_mul(t_float32 value1, t_float32 value2);
}

#endif