# RiveSharp Renderer

![CI](https://img.shields.io/github/actions/workflow/status/wieslawsoltes/RiveSharp/build.yml?branch=main)
![License](https://img.shields.io/badge/license-MIT-blue.svg)

High-performance bindings for the Rive GPU renderer, exposing a C-compatible shim (`rive_renderer_ffi`) and ergonomic .NET APIs.

## Features
- Native renderer interop (paths, paints, gradients, image meshes, text) via `rive_renderer_ffi`
- Managed wrappers (`RiveRenderer` library) with safe handles and span-friendly uploads
- Cross-platform build scripts (macOS, Linux, Windows) and CI automation

## Quick Start

```bash
# Native + managed build (macOS/Linux)
./scripts/build.sh -c Release

# Managed-only build & pack
./scripts/build-managed.sh -c Release

# Windows PowerShell
./scripts/build.ps1 -Configuration Release
```

NuGet package artifacts are written to `artifacts/nuget/`.

## Documentation
- [Renderer Interop Plan](docs/renderer-interop-plan.md)
- [Build & Release Plan](docs/build-and-release-plan.md)
- [Build Guides](docs/build-guides.md)
- [Render Validation Harness](docs/samples/render-validation.md)

## Roadmap
See [Phases 7–9](docs/renderer-interop-plan.md) for GPU surface interop, Avalonia sample, render validation, and release automation tasks.

## Contributing

We welcome issues and pull requests! Please review the [Contributing Guide](CONTRIBUTING.md) and the [Code of Conduct](CODE_OF_CONDUCT.md) before getting started.

## License
MIT
