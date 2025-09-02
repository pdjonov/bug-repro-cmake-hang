cmake_minimum_required(VERSION 3.22)

project(Cobalt.Content.Native VERSION 1.0 DESCRIPTION "Native libs and P/Invoke bridge functions")

if(MSVC)
	add_compile_options(/Zc:preprocessor)
endif()

set(CMAKE_MODULE_PATH ${CMAKE_SOURCE_DIR}/../../CMakeModules)
set(EXTERNAL_LIBS_DIR ${CMAKE_SOURCE_DIR}/../../External)

set(cpp_src
	test.cpp)
set(link_libs
	)
# Cobalt.Content.Native

add_library(Cobalt.Content.Native SHARED ${cpp_src})
set_target_properties(Cobalt.Content.Native PROPERTIES
	PREFIX ""
	# why is this different on Windows and Linux?
	LIBRARY_OUTPUT_DIRECTORY ${CMAKE_CURRENT_LIST_DIR}/bin/${CMAKE_BUILD_TYPE}
	RUNTIME_OUTPUT_DIRECTORY ${CMAKE_CURRENT_LIST_DIR}/bin/${CMAKE_BUILD_TYPE}

	CXX_STANDARD 20
	CXX_STANDARD_REQUIRED true
	CXX_VISIBILITY_PRESET hidden)
if(GCC)
	# Don't needlessly defer errors until runtime.
	target_link_options(Cobalt.Content.Native PUBLIC
		-Wl,--no-undefined)
endif()

target_link_libraries(Cobalt.Content.Native ${link_libs})

if(MSVC)
	# Suppress a ton of spam about VK libs not having debug symbols.
	target_link_options(Cobalt.Content.Native PRIVATE /ignore:4099)
endif()