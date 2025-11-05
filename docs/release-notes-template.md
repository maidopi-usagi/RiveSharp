# Release Notes Template

## Summary
- Highlight key changes.
- Mention native backend updates (Metal / Vulkan / D3D).

## Native Artifacts
| Platform | Architecture | Configuration | Notes |
|----------|--------------|---------------|-------|
| macOS    | arm64 / x64  | Release       | `librive_renderer_ffi.dylib`, `librive*.a` |
| Windows  | x64 / ARM64  | Release       | `rive_renderer_ffi.dll`, `rive*.lib` |
| Linux    | x64          | Release       | `librive_renderer_ffi.so`, `librive*.a` |

## Managed Package
- NuGet ID: `RiveRenderer`
- Version: `<set-version>`
- Includes runtime-specific native libraries.

## Known Issues
- GPU renderer static library currently requires manual build step.
- Vulkan validation pending CI toolchain installation.

