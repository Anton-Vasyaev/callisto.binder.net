//
// Copyright (c) 2019
// Vasyaev Anton
//
#ifndef CALLISTO_FRAMEWORK_LIB_EXPORT_H
#define CALLISTO_FRAMEWORK_LIB_EXPORT_H

#include "platform_macros.h"


#ifdef CALLISTO_PLATFORM_WINDOWS
    #define CALLISTO_EXPORT		__declspec( dllexport ) 
    #define CALLISTO_CALL		__cdecl
#endif


#ifdef CALLISTO_PLATFORM_LINUX
    #define CALLISTO_EXPORT     //__attribute__((visibility("default")))
    #define CALLISTO_CALL       //__attribute__ ((__cdecl__))
#endif

#endif