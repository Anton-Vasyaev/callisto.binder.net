// parent header
#include <funcs.hpp>

// std
#include <memory>
#include <cmath>

namespace NativeWrap
{
	void NativeClass::copy_bytes(
		t_byte* src, 
		t_byte* dst,
		t_size size
	)
	{
		std::memcpy(dst, src, size);
	}

	point3f NativeClass::Point3fClass::construct_point3f(
		t_float32 x, 
		t_float32 y, 
		t_float32 z
	)
	{
		return point3f{ x, y, z };
	}

	t_float32 native_pow(t_float32 value, t_int32 pow_value)
	{
		return std::pow<t_float32>(value, pow_value);
	}
}



t_float32 native_mul(t_float32 value1, t_float32 value2)
{
	return value1 * value2;

}