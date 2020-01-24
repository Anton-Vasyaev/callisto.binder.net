// parent header
#include <Funcs/FuncsNativeWrap.h>
#include <algorithm>

t_float32 NativeWrap_nativePow(
	t_float32 value,
	t_float32 pow_value
)
{
	return pow(value, pow_value);
}