cmake_minimum_required(VERSION 3.20)
project(rive_renderer_ffi LANGUAGES CXX)

set(CMAKE_LIBRARY_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/out/${CMAKE_BUILD_TYPE})
set(CMAKE_ARCHIVE_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/out/${CMAKE_BUILD_TYPE})
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_BINARY_DIR}/out/${CMAKE_BUILD_TYPE})

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)

set(RIVE_RENDERER_ROOT "" CACHE PATH "Path to the river-renderer source tree")
if(NOT RIVE_RENDERER_ROOT)
    set(_rive_candidates
        "${CMAKE_SOURCE_DIR}/extern/river-renderer"
        "${CMAKE_CURRENT_LIST_DIR}/../extern/river-renderer"
    )
    foreach(_candidate IN LISTS _rive_candidates)
        if(EXISTS "${_candidate}")
            set(RIVE_RENDERER_ROOT "${_candidate}")
            break()
        endif()
    endforeach()
endif()

if(NOT RIVE_RENDERER_ROOT OR NOT EXISTS "${RIVE_RENDERER_ROOT}")
    message(FATAL_ERROR
        "Could not locate the river-renderer submodule. "
        "Set RIVE_RENDERER_ROOT to the river-renderer checkout or ensure extern/river-renderer is present.")
endif()

add_library(rive_renderer_ffi SHARED
    src/rive_renderer_ffi.cpp
)

target_include_directories(rive_renderer_ffi
    PUBLIC
        ${CMAKE_CURRENT_SOURCE_DIR}/include
        ${RIVE_RENDERER_ROOT}/include
        ${RIVE_RENDERER_ROOT}/renderer/include
)

target_compile_definitions(rive_renderer_ffi PRIVATE RIVE_RENDERER_FFI_IMPLEMENTATION)

set(RIVE_RENDERER_LIB_SEARCH_ROOT "${RIVE_RENDERER_ROOT}/out")
file(GLOB_RECURSE RIVE_RENDERER_NATIVE_LIBS
    "${RIVE_RENDERER_LIB_SEARCH_ROOT}/**/*.a"
    "${RIVE_RENDERER_LIB_SEARCH_ROOT}/**/*.dylib"
    "${RIVE_RENDERER_LIB_SEARCH_ROOT}/**/*.so"
)

if(RIVE_RENDERER_NATIVE_LIBS)
    list(REMOVE_DUPLICATES RIVE_RENDERER_NATIVE_LIBS)
    target_link_libraries(rive_renderer_ffi PRIVATE ${RIVE_RENDERER_NATIVE_LIBS})
else()
    message(FATAL_ERROR
        "No prebuilt river-renderer libraries found under ${RIVE_RENDERER_LIB_SEARCH_ROOT}. "
        "Run ${RIVE_RENDERER_ROOT}/build/build_rive.sh (use --with_rive_text) to generate the native renderer artifacts before building rive_renderer_ffi.")
endif()

if (WIN32)
    target_compile_definitions(rive_renderer_ffi PRIVATE NOMINMAX WIN32_LEAN_AND_MEAN)
    target_link_libraries(rive_renderer_ffi PRIVATE d3d12 dxgi dxguid)
endif()

set_target_properties(rive_renderer_ffi PROPERTIES
    OUTPUT_NAME "rive_renderer_ffi"
)

include(GNUInstallDirs)

install(TARGETS rive_renderer_ffi
    ARCHIVE DESTINATION ${CMAKE_INSTALL_LIBDIR}
    LIBRARY DESTINATION ${CMAKE_INSTALL_LIBDIR}
    RUNTIME DESTINATION ${CMAKE_INSTALL_BINDIR})

install(FILES include/rive_renderer_ffi.h DESTINATION ${CMAKE_INSTALL_INCLUDEDIR})
