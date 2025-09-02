#include <cstdint>

#ifdef _MSC_VER
#define EXPORT extern "C" __declspec(dllexport)
#define EXPORT_CALL __stdcall
#else
#define EXPORT extern "C" __attribute__((__visibility__("default")))
#define EXPORT_CALL
#endif

enum class status_code : std::uint32_t
{
	success,

	fail,

	input_too_large,
	output_buffer_too_small,
	invalid_data,
	invalid_argument,
	not_found,

	unsupported,
};

EXPORT status_code EXPORT_CALL test_a_thing(
	const char* src,
	char* dst,
	std::size_t length)
{
	for (std::size_t i = 0; i < length; i++)
		dst[i] = src[length - i - 1];

	return status_code::success;
}