#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd "${SCRIPT_DIR}/.." && pwd)"

FILES=()
if [[ -d "${ROOT_DIR}/renderer_ffi" ]]; then
    while IFS= read -r -d '' file; do
        FILES+=("${file}")
    done < <(find "${ROOT_DIR}/renderer_ffi" \
        \( -path '*/build-*/*' -o -path '*/out/*' -o -path '*/CMakeFiles/*' -o -path '*/_deps/*' \) -prune -o \
        \( -name '*.h' -o -name '*.hpp' -o -name '*.c' -o -name '*.cc' -o -name '*.cpp' \) -type f -print0)
fi

if [[ "${#FILES[@]}" -eq 0 ]]; then
    echo "No C/C++ files found for clang-format."
    exit 0
fi

if command -v clang-format >/dev/null 2>&1; then
    CLANG_FORMAT=(clang-format)
elif command -v xcrun >/dev/null 2>&1 && xcrun -f clang-format >/dev/null 2>&1; then
    CLANG_FORMAT=(xcrun clang-format)
else
    echo "error: clang-format is not installed. Set CLANG_FORMAT or install clang-format." >&2
    exit 1
fi

echo "Running clang-format style checks..."

FAIL=0
for file in "${FILES[@]}"; do
    if ! "${CLANG_FORMAT[@]}" -style=file -n --Werror "${file}"; then
        echo "clang-format reported issues in ${file}" >&2
        FAIL=1
    fi
done

if [[ "${FAIL}" -ne 0 ]]; then
    echo "clang-format style violations detected." >&2
    exit 1
fi

echo "clang-format check passed."
