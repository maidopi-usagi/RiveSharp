#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd "${SCRIPT_DIR}/.." && pwd)"

if [[ $# -eq 0 ]]; then
  CONFIG="release"
else
  CONFIG="$(printf '%s' "$1" | tr '[:upper:]' '[:lower:]')"
fi

to_cmake_cfg() {
  local value="$1"
  case "$value" in
    release) echo "Release" ;;
    debug) echo "Debug" ;;
    minsize) echo "MinSizeRel" ;;
    relwithdebinfo) echo "RelWithDebInfo" ;;
    *) echo "$(printf '%s' "${value:0:1}" | tr '[:lower:]' '[:upper:]')$(printf '%s' "${value:1}")" ;;
  esac
}

CMAKE_CFG="$(to_cmake_cfg "${CONFIG}")"

if [[ -z "${EMSDK:-}" ]]; then
  echo "error: EMSDK environment variable not set. Source emsdk_env.sh before running this script." >&2
  exit 1
fi

if ! command -v emcmake >/dev/null 2>&1; then
  echo "error: emcmake not found. Ensure the Emscripten environment is active." >&2
  exit 1
fi

if ! command -v emmake >/dev/null 2>&1; then
  echo "error: emmake not found. Ensure the Emscripten environment is active." >&2
  exit 1
fi

OUTPUT_RUNTIME="${ROOT_DIR}/dotnet/RiveRenderer/runtimes/browser-wasm/native/${CONFIG}"
OUTPUT_ARTIFACTS="${ROOT_DIR}/artifacts/native/browser-wasm/${CONFIG}"
mkdir -p "${OUTPUT_RUNTIME}" "${OUTPUT_ARTIFACTS}"

echo "==> Building river-renderer (wasm, ${CONFIG})"
(
  cd "${ROOT_DIR}/extern/river-renderer"
  RIVE_PREMAKE_ARGS="--with_rive_text" ./build/build_rive.sh wasm "${CONFIG}"
)

CMAKE_BUILD_DIR="${ROOT_DIR}/renderer_ffi/build-wasm-${CONFIG}"
mkdir -p "${CMAKE_BUILD_DIR}"

echo "==> Configuring renderer_ffi (wasm, ${CONFIG})"
emcmake cmake \
  -S "${ROOT_DIR}/renderer_ffi" \
  -B "${CMAKE_BUILD_DIR}" \
  -DCMAKE_BUILD_TYPE="${CMAKE_CFG}" \
  -DRIVE_RENDERER_BUILD_WASM=ON

echo "==> Building renderer_ffi (wasm, ${CONFIG})"
emmake cmake --build "${CMAKE_BUILD_DIR}"

WASM_OUT="${CMAKE_BUILD_DIR}/out/${CMAKE_CFG}/librive_renderer_ffi.wasm"
JS_OUT="${CMAKE_BUILD_DIR}/out/${CMAKE_CFG}/rive_renderer_ffi.js"

if [[ ! -f "${WASM_OUT}" ]]; then
  echo "error: expected wasm artifact '${WASM_OUT}' not found." >&2
  exit 1
fi

cp "${WASM_OUT}" "${OUTPUT_RUNTIME}/"
if [[ -f "${JS_OUT}" ]]; then
  cp "${JS_OUT}" "${OUTPUT_RUNTIME}/"
fi

cp -R "${OUTPUT_RUNTIME}/." "${OUTPUT_ARTIFACTS}/"
(cd "${OUTPUT_ARTIFACTS}" && ls -1 > manifest.txt)

echo "WASM build complete. Artifacts staged under ${OUTPUT_RUNTIME}"
