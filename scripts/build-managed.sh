#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd "${SCRIPT_DIR}/.." && pwd)"

CONFIG="Release"
PACK=true

while [[ $# -gt 0 ]]; do
  case "$1" in
    -c|--configuration)
      CONFIG="$2"
      shift 2
      ;;
    --no-pack)
      PACK=false
      shift
      ;;
    *)
      echo "Unknown argument: $1" >&2
      exit 1
      ;;
  esac
done

MANAGED_DIR="${ROOT_DIR}/dotnet"
ARTIFACT_ROOT="${ROOT_DIR}/artifacts"
NUGET_DIR="${ARTIFACT_ROOT}/nuget"

mkdir -p "${NUGET_DIR}"

echo "==> Restoring managed dependencies"
dotnet restore "${MANAGED_DIR}/RiveRenderer/RiveRenderer.csproj"
dotnet restore "${MANAGED_DIR}/RiveRenderer.Tests/RiveRenderer.Tests.csproj"
dotnet restore "${MANAGED_DIR}/RiveRenderer.Benchmarks/RiveRenderer.Benchmarks.csproj"

echo "==> Building managed assemblies (${CONFIG})"
dotnet build "${MANAGED_DIR}/RiveRenderer/RiveRenderer.csproj" -c "${CONFIG}" --no-restore
dotnet build "${MANAGED_DIR}/RiveRenderer.Tests/RiveRenderer.Tests.csproj" -c "${CONFIG}" --no-restore
dotnet build "${MANAGED_DIR}/RiveRenderer.Benchmarks/RiveRenderer.Benchmarks.csproj" -c "${CONFIG}" --no-restore

echo "==> Running unit tests"
dotnet test "${MANAGED_DIR}/RiveRenderer.Tests/RiveRenderer.Tests.csproj" -c "${CONFIG}" --no-build

if [[ "${PACK}" == true ]]; then
  echo "==> Packing NuGet artifacts"
  dotnet pack "${MANAGED_DIR}/RiveRenderer/RiveRenderer.csproj" -c "${CONFIG}" --no-build -o "${NUGET_DIR}"
fi

echo "Managed build complete. Artifacts available under ${ARTIFACT_ROOT}."
