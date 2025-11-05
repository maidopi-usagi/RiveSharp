#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd "${SCRIPT_DIR}/.." && pwd)"

if [[ $# -eq 0 ]]; then
  CONFIGS=(release)
else
  CONFIGS=()
  for arg in "$@"; do
    CONFIGS+=("$(printf '%s' "$arg" | tr '[:upper:]' '[:lower:]')")
  done
fi

ARCH="$(uname -m)"
RUNTIME_ROOT="${ROOT_DIR}/dotnet/RiveRenderer/runtimes/osx-${ARCH}/native"
ARTIFACT_ROOT="${ROOT_DIR}/artifacts/native/osx-${ARCH}"

mkdir -p "${RUNTIME_ROOT}"

for config in "${CONFIGS[@]}"; do
  echo "==> Building river-renderer (${config})"
  (
    cd "${ROOT_DIR}/extern/river-renderer"
    RIVE_PREMAKE_ARGS="--with_rive_text" ./build/build_rive.sh "${config}" -- rive_pls_renderer
  )

  BUILD_DIR="${ROOT_DIR}/renderer_ffi/build-${config}"
  echo "==> Configuring renderer_ffi (${config})"
  if [[ "${config}" == "release" ]]; then
    CMAKE_CFG="Release"
  elif [[ "${config}" == "debug" ]]; then
    CMAKE_CFG="Debug"
  else
    CMAKE_CFG="$(printf '%s' "${config:0:1}" | tr '[:lower:]' '[:upper:]')$(printf '%s' "${config:1}" | tr '[:upper:]' '[:lower:]')"
  fi

  cmake -S "${ROOT_DIR}/renderer_ffi" -B "${BUILD_DIR}" -G "Unix Makefiles" -DCMAKE_BUILD_TYPE="${CMAKE_CFG}"

  echo "==> Building renderer_ffi (${config})"
  cmake --build "${BUILD_DIR}"

  FFI_LIB="${BUILD_DIR}/librive_renderer_ffi.dylib"
  if [[ ! -f "${FFI_LIB}" ]]; then
    echo "error: expected ${FFI_LIB} to exist" >&2
    exit 1
  fi

  DEST_DIR="${RUNTIME_ROOT}/${config}"
  mkdir -p "${DEST_DIR}"

  echo "==> Copying artifacts to ${DEST_DIR}"
  cp "${FFI_LIB}" "${DEST_DIR}/"

  for native_lib in librive.a librive_harfbuzz.a librive_sheenbidi.a librive_yoga.a librive_pls_renderer.a libminiaudio.a; do
    SOURCE_LIB="${ROOT_DIR}/extern/river-renderer/out/${config}/${native_lib}"
    if [[ -f "${SOURCE_LIB}" ]]; then
      cp "${SOURCE_LIB}" "${DEST_DIR}/"
    fi
  done

  if [[ -n "${CODESIGN_IDENTITY:-}" ]]; then
    if command -v codesign >/dev/null 2>&1; then
      echo "==> Codesigning artifacts with identity '${CODESIGN_IDENTITY}'"
      codesign_flags=("--force" "--sign" "${CODESIGN_IDENTITY}")
      if [[ -n "${CODESIGN_FLAGS:-}" ]]; then
        # shellcheck disable=SC2206
        extra_flags=(${CODESIGN_FLAGS})
        codesign_flags+=("${extra_flags[@]}")
      fi

      while IFS= read -r signed_file; do
        codesign "${codesign_flags[@]}" "${signed_file}"
      done < <(find "${DEST_DIR}" -maxdepth 1 -type f -name '*.dylib')
    else
      echo "warning: codesign tool not found; skipping codesign step" >&2
    fi
  fi

  STAGE_DIR="${ARTIFACT_ROOT}/${config}"
  mkdir -p "${STAGE_DIR}"
  cp -R "${DEST_DIR}"/* "${STAGE_DIR}/"
  (cd "${STAGE_DIR}" && ls -1 > manifest.txt)
done

echo "Build complete. Artifacts staged under ${RUNTIME_ROOT}";
