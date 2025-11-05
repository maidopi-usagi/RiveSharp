#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd "${SCRIPT_DIR}/.." && pwd)"

if command -v clang-tidy >/dev/null 2>&1; then
    CLANG_TIDY=(clang-tidy)
elif command -v xcrun >/dev/null 2>&1 && xcrun -f clang-tidy >/dev/null 2>&1; then
    CLANG_TIDY=(xcrun clang-tidy)
else
    echo "error: clang-tidy is not installed. Install clang-tidy or set CLANG_TIDY." >&2
    exit 1
fi

TARGETS=(
    "${ROOT_DIR}/renderer_ffi/src/rive_renderer_ffi.cpp"
)

for target in "${TARGETS[@]}"; do
    if [[ ! -f "${target}" ]]; then
        echo "warning: ${target} not found; skipping." >&2
    fi
done

INCLUDE_DIRS=(
    "${ROOT_DIR}/renderer_ffi/include"
    "${ROOT_DIR}/extern/river-renderer/include"
    "${ROOT_DIR}/extern/river-renderer/renderer/include"
)

EXTRA_ARGS=()
for dir in "${INCLUDE_DIRS[@]}"; do
    EXTRA_ARGS+=("-I${dir}")
done

EXTRA_ARGS+=("-DRIVE_RENDERER_FFI_IMPLEMENTATION")
EXTRA_ARGS+=("-std=c++17")

echo "Running clang-tidy checks..."

for target in "${TARGETS[@]}"; do
    if [[ ! -f "${target}" ]]; then
        continue
    fi

    "${CLANG_TIDY[@]}" \
        "${target}" \
        --config-file "${ROOT_DIR}/.clang-tidy" \
        --warnings-as-errors=clang-analyzer-* \
        --extra-arg=-Wno-unknown-warning-option \
        --extra-arg=-Wno-ignored-attributes \
        --extra-arg=-Wno-deprecated-declarations \
        -- "${EXTRA_ARGS[@]}"
done

echo "clang-tidy check passed."
