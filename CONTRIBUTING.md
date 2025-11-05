# Contributing to RiveSharp Renderer

Thanks for your interest in improving the project! This guide covers the recommended workflow for proposing changes, running local checks, and submitting pull requests.

## Getting Started

1. **Fork and clone** this repository.
2. **Initialize submodules** (the renderer lives under `extern/river-renderer`):
   ```bash
   git submodule update --init --recursive
   ```
3. **Install prerequisites** for your platform:
   - macOS: Xcode command line tools, CMake, Ninja
   - Linux: build-essential, CMake, Ninja, Vulkan SDK (if enabling Vulkan)
   - Windows: Visual Studio 2022 with C++ workloads, Ninja, Vulkan SDK
4. Use the platform build scripts under `scripts/` to produce native artifacts. See [`docs/build-guides.md`](docs/build-guides.md) for details.

## Development Workflow

- **Managed code** lives under `dotnet/`. Run `dotnet format` (or the verification script) to keep code style consistent.
- **Native code** for the shim resides in `renderer_ffi/`. Use the provided `.clang-format` configuration. The `scripts/check-clang-format.sh` and `scripts/run-clang-tidy.sh` helpers mirror the CI checks.
- **Tests and validation**:
  ```bash
  dotnet test dotnet/RiveRenderer.Tests/RiveRenderer.Tests.csproj -c Release
  scripts/validate-render.sh
  ```
  The render validation harness ensures the null backend can record and submit a frame end-to-end.
- **Documentation** updates live under `docs/`. Please keep the build/release plans in sync with major changes.

## Submitting Changes

1. Start from a topic branch and keep commits focused.
2. Ensure CI scripts succeed locally (`scripts/check-clang-format.sh`, `scripts/dotnet-format-verify.sh`, `dotnet test`).
3. Update relevant docs and changelog sections (release notes, plans, or guides) when behavior changes.
4. Open a pull request describing the motivation, testing performed, and any follow-up work.
5. Be responsive to feedback. We aim for constructive, respectful collaboration per the [Code of Conduct](CODE_OF_CONDUCT.md).

## Questions & Support

If you encounter issues building or running the project, open a discussion or GitHub issue with platform details, logs, and steps to reproduce. Contributions to documentation, samples, tests, and automation are all welcome!
