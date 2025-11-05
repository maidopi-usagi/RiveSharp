# Avalonia Null-Backend Sample

The `samples/Avalonia/RiveRenderer.AvaloniaSample` project demonstrates how to bootstrap the managed bindings inside an Avalonia desktop app and validate that the native renderer is discoverable at runtime. It uses the null backend so the sample can run without GPU access.

## Prerequisites

- Follow the platform build guide to stage native binaries (`scripts/build-macos.sh`, `scripts/build-linux.sh`, or `scripts/build-windows.ps1`). The sample expects the native outputs under `dotnet/RiveRenderer/runtimes/<rid>/native/<config>`.
- .NET SDK 9.0 (preview) matching the repository SDK requirements.

## Build & Run

```bash
dotnet build samples/Avalonia/RiveRenderer.AvaloniaSample/RiveRenderer.AvaloniaSample.csproj -c Release
dotnet run --project samples/Avalonia/RiveRenderer.AvaloniaSample/RiveRenderer.AvaloniaSample.csproj -c Release
```

When the native renderer loads successfully you will see the status message:

```
Native renderer initialised using Null backend.
```

If the native library is missing, the sample reports guidance to run the platform build scripts first.

## Continuous Integration

Add the sample project to platform smoke tests once GPU backends are wired. The null backend path currently exercises SafeHandle lifetimes and native discovery without requiring a swapchain.
