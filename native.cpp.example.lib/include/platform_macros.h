//
// Copyright (c) 2019
// Vasyaev Anton
//
#ifndef CALLISTO_FRAMEWORK_PLATFORM_MACROS_H
#define CALLISTO_FRAMEWORK_PLATFORM_MACROS_H

//
// DETECT OS
#if defined(_WIN64) || defined(_WIN32)
#define CALLISTO_PLATFORM_WINDOWS
#endif

#if defined(__linux__)
#define CALLISTO_PLATFORM_LINUX
#endif


//
// DETECT ARCHITECTURE
//
#if defined(__i386__) || defined(_M_IX86) || defined(_MI86)
    #define CALLISTO_PLATFORM_I386
#endif

#if defined(_M_AMD64) || defined(__amd64__)
    #define CALLISTO_PLATFORM_AMD64
#endif

#if defined(CALLISTO_PLATFORM_I386) || defined(CALLISTO_PLATFORM_AMD64)
    #define CALLISTO_PLATFORM_CPUX86
#endif


// DETECT BIT DESIGN
#if defined(CALLISTO_PLATFORM_LINUX)
    #if defined(__amd64__)
        #define CALLISTO_PLATFORM_64BIT
    #else
        #define CALLISTO_PLATFORM_32BIT
    #endif
#endif

#if defined(CALLISTO_PLATFORM_WINDOWS)
    #if defined(_WIN64)
        #define CALLISTO_PLATFORM_64BIT
    #else
        #define CALLISTO_PLATFORM_32BIT
    #endif
#endif

#endif