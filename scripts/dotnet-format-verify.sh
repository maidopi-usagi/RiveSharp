#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd "${SCRIPT_DIR}/.." && pwd)"

PROJECTS=()
while IFS= read -r -d '' proj; do
    PROJECTS+=("${proj}")
done < <(find "${ROOT_DIR}" \( -path '*/bin/*' -o -path '*/obj/*' -o -path '*/extern/*' -o -path '*/.git/*' \) -prune -o -name '*.csproj' -print0)

if [[ "${#PROJECTS[@]}" -eq 0 ]]; then
    echo "No .csproj files found for dotnet format."
    exit 0
fi

for proj in "${PROJECTS[@]}"; do
    echo "Verifying formatting for ${proj#${ROOT_DIR}/}"
    dotnet format --verify-no-changes "${proj}"
done

echo "dotnet format verification passed."
