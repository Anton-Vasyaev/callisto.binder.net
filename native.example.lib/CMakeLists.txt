cmake_minimum_required(VERSION 3.10)
project(native.example.lib)

set(CMAKE_CXX_STANDARD 17)

#-------------------------------------------------------------------------------------------------#
#--------------------------------------------CONFIGURE--------------------------------------------#
#-------------------------------------------------------------------------------------------------#
# DETECT BIT DESIGN (x32/x64)
if(CMAKE_SIZEOF_VOID_P EQUAL 8)
	set(CALLISTO_BIT_DESIGN "x64" CACHE INTERNAL "" FORCE)
	set(CALLISTO_BIT_DESIGN_N 64)
elseif(CMAKE_SIZEOF_VOID_P EQUAL 4)
	set(CALLISTO_BIT_DESIGN "x32" CACHE INTERNAL "" FORCE)
	set(CALLISTO_BIT_DESIGN_N 32)
else()
	message(FATAL_ERROR "Not have project configuration for bit design:${CMAKE_SIZEOF_VOID_P}-byte")
endif()
# DETECT BUILD TYPE (Debug/Release)
if(CMAKE_BUILD_TYPE MATCHES "Debug")
	set(CALLISTO_BUILD_TYPE "Debug" CACHE INTERNAL "" FORCE)
	set(CALLISTO_BUILD_TYPE_POSTFIX "d" CACHE INTERNAL "" FORCE)
else()
	set(CALLISTO_BUILD_TYPE "Release" CACHE INTERNAL "" FORCE)
	set(CALLISTO_BUILD_TYPE_POSTFIX "" CACHE INTERNAL "" FORCE)
endif()
# DETECT OS
set(CALLISTO_OS_NAME ${CMAKE_SYSTEM_NAME} CACHE INTERNAL "" FORCE)
if(CALLISTO_OS_NAME STREQUAL "Windows")
    set(CALLISTO_SHARED_LIB_EXT_NAME "dll" CACHE INTERNAL "" FORCE)
    set(CALLISTO_STATIC_LIB_EXT_NAME "lib" CACHE INTERNAL "" FORCE)
elseif(CALLISTO_OS_NAME STREQUAL "Linux")
    set(CALLISTO_SHARED_LIB_EXT_NAME "so" CACHE INTERNAL "" FORCE)
    set(CALLISTO_STATIC_LIB_EXT_NAME "a" CACHE INTERNAL "" FORCE)
else()
	message(FATAL_ERROR "Not have project configuration for os:${CALLISTO_OS_NAME}")
endif()

# SET GLOBAL AUXILIARY VARIABLES
string(
	CONCAT BUILD_RESULT_PATH
	"${CALLISTO_OS_NAME}"
	"/${CALLISTO_BIT_DESIGN}"
	"/${CALLISTO_BUILD_TYPE}"
)
set(CALLISTO_BUILD_PATH ${BUILD_RESULT_PATH} CACHE INTERNAL "" FORCE)

set(ARCHIVE_DIR ${CMAKE_CURRENT_SOURCE_DIR}/build_result/${CALLISTO_BUILD_PATH}/archive)
set(LIB_DIR ${CMAKE_CURRENT_SOURCE_DIR}/build_result/${CALLISTO_BUILD_PATH}/lib)
set(DLL_DIR ${CMAKE_CURRENT_SOURCE_DIR}/build_result/${CALLISTO_BUILD_PATH}/dll)

set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${ARCHIVE_DIR})
set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${LIB_DIR})
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${DLL_DIR})

include_directories(include)

file(GLOB_RECURSE lib_source_list "include/*.hpp" "include/*.h" "src/*.hpp" "src/*.h" "src/*.cpp")
add_library(nativeexample_${CALLISTO_BIT_DESIGN} SHARED ${lib_source_list})