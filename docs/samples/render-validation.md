# Render Validation Harness

The `samples/Validation/RiveRenderer.RenderValidation` console app performs a headless draw against the null backend. It exercises path creation, gradient shaders, and frame submission to ensure that the native renderer and managed bindings are working end-to-end.

## Prerequisites

- Build the native renderer for your platform using the scripts in `scripts/` so the `dotnet/RiveRenderer/runtimes/<rid>/native/<config>` directories contain the required binaries.

## Run Locally

```bash
scripts/validate-render.sh [release|debug]
```

The script sets the appropriate library search path for the current platform and runs the validation project. A successful run prints:

```
Render validation succeeded using null backend.
```

Any missing native binaries or runtime issues cause a non-zero exit code, which is propagated to CI.

## Continuous Integration

The GitHub Actions workflow includes a `render-validation` job that downloads Linux native artifacts from the matrix builds and runs the validation script in Release mode before packaging managed assemblies.
