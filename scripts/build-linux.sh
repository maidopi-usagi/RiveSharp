#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd "${SCRIPT_DIR}/.." && pwd)"

if [[ $# -eq 0 ]]; then
  CONFIGS=(release)
else
  CONFIGS=()
  for arg in "$@"; do
    CONFIGS+=("${arg,,}")
  done
fi

ARCH="$(uname -m)"
RUNTIME_ROOT="${ROOT_DIR}/dotnet/RiveRenderer/runtimes/linux-${ARCH}/native"
ARTIFACT_ROOT="${ROOT_DIR}/artifacts/native/linux-${ARCH}"

mkdir -p "${RUNTIME_ROOT}"

if ! command -v cmake >/dev/null 2>&1; then
  echo "error: cmake is required" >&2
  exit 1
fi

for config in "${CONFIGS[@]}"; do
  echo "==> Building river-renderer (${config})"
  (
    cd "${ROOT_DIR}/extern/river-renderer"
    RIVE_PREMAKE_ARGS="--with_rive_text" ./build/build_rive.sh "${config}" -- rive_pls_renderer
  )

  BUILD_DIR="${ROOT_DIR}/renderer_ffi/build-linux-${config}"
  echo "==> Configuring renderer_ffi (${config})"
  cmake -S "${ROOT_DIR}/renderer_ffi" -B "${BUILD_DIR}" -G "Ninja" -DCMAKE_BUILD_TYPE="${config^}"

  echo "==> Building renderer_ffi (${config})"
  cmake --build "${BUILD_DIR}"

  FFI_LIB="${BUILD_DIR}/librive_renderer_ffi.so"
  if [[ ! -f "${FFI_LIB}" ]]; then
    echo "error: expected ${FFI_LIB} to exist" >&2
    exit 1
  fi

  DEST_DIR="${RUNTIME_ROOT}/${config}"
  mkdir -p "${DEST_DIR}"
  cp "${FFI_LIB}" "${DEST_DIR}/"

  for native_lib in librive.a librive_harfbuzz.a librive_sheenbidi.a librive_yoga.a librive_pls_renderer.a libminiaudio.a; do
    SOURCE_LIB="${ROOT_DIR}/extern/river-renderer/out/${config}/${native_lib}"
    if [[ -f "${SOURCE_LIB}" ]]; then
      cp "${SOURCE_LIB}" "${DEST_DIR}/"
    fi
  done

  STAGE_DIR="${ARTIFACT_ROOT}/${config}"
  mkdir -p "${STAGE_DIR}"
  cp -R "${DEST_DIR}"/* "${STAGE_DIR}/"
  (cd "${STAGE_DIR}" && ls -1 > manifest.txt)
done

echo "Build complete. Artifacts staged under ${RUNTIME_ROOT}";
