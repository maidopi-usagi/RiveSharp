#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd "${SCRIPT_DIR}/.." && pwd)"

CONFIG="${1:-release}"
CONFIG_LOWER="$(echo "${CONFIG}" | tr '[:upper:]' '[:lower:]')"

case "$(uname -s)" in
    Darwin)
        RUNTIME_DIR="${ROOT_DIR}/dotnet/RiveRenderer/runtimes/osx-$(uname -m)/native/${CONFIG_LOWER}"
        export DYLD_LIBRARY_PATH="${RUNTIME_DIR}:${DYLD_LIBRARY_PATH:-}"
        ;;
    Linux)
        RUNTIME_DIR="${ROOT_DIR}/dotnet/RiveRenderer/runtimes/linux-$(uname -m)/native/${CONFIG_LOWER}"
        export LD_LIBRARY_PATH="${RUNTIME_DIR}:${LD_LIBRARY_PATH:-}"
        ;;
    MINGW*|MSYS*|CYGWIN*|Windows_NT)
        RUNTIME_DIR="${ROOT_DIR}/dotnet/RiveRenderer/runtimes/win-x64/native/${CONFIG_LOWER}"
        export PATH="${RUNTIME_DIR};${PATH}"
        ;;
    *)
        echo "warning: unsupported platform for render validation" >&2
        RUNTIME_DIR=""
        ;;
esac

if [[ -n "${RUNTIME_DIR}" && ! -d "${RUNTIME_DIR}" ]]; then
    echo "error: runtime directory ${RUNTIME_DIR} not found. Build native artifacts first." >&2
    exit 1
fi

echo "Running render validation (config=${CONFIG_LOWER})" >&2
dotnet run --project "${ROOT_DIR}/samples/Validation/RiveRenderer.RenderValidation/RiveRenderer.RenderValidation.csproj" -c Release
