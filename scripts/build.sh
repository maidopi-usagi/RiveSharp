#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"

CONFIG="release"
MANAGED=true

usage() {
  echo "Usage: $0 [-c|--configuration <Debug|Release>] [--skip-managed]" >&2
}

while [[ $# -gt 0 ]]; do
  case "$1" in
    -c|--configuration)
      CONFIG="$(printf '%s' "$2" | tr '[:upper:]' '[:lower:]')"
      shift 2
      ;;
    --skip-managed)
      MANAGED=false
      shift
      ;;
    -h|--help)
      usage
      exit 0
      ;;
    *)
      echo "Unknown argument: $1" >&2
      usage
      exit 1
      ;;
  esac
done

case "$(uname -s)" in
  Darwin*)
    chmod +x "${SCRIPT_DIR}/build-macos.sh"
    "${SCRIPT_DIR}/build-macos.sh" "$CONFIG"
    ;;
  Linux*)
    chmod +x "${SCRIPT_DIR}/build-linux.sh"
    "${SCRIPT_DIR}/build-linux.sh" "$CONFIG"
    ;;
  *)
    echo "Unsupported platform for build.sh" >&2
    exit 1
    ;;
esac

if [[ "$MANAGED" == true ]]; then
  chmod +x "${SCRIPT_DIR}/build-managed.sh"
  "${SCRIPT_DIR}/build-managed.sh" -c "${CONFIG^}"
fi
