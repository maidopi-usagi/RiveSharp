#!/usr/bin/env bash

set -euo pipefail

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
ROOT_DIR="$(cd "${SCRIPT_DIR}/.." && pwd)"

log() {
  printf '%s\n' "$*" >&2
}

check_command() {
  local cmd="$1"
  local hint="${2:-}"
  if ! command -v "${cmd}" >/dev/null 2>&1; then
    log "error: required command '${cmd}' not found${hint:+ (${hint})}"
    return 1
  fi
}

check_file() {
  local path="$1"
  local description="${2:-file}"
  if [[ ! -e "$path" ]]; then
    log "error: missing ${description} at ${path}"
    return 1
  fi
}

validate_macos() {
  log "Validating macOS toolchain..."
  check_command xcode-select "install Xcode command line tools via 'xcode-select --install'" || return 1
  xcode-select -p >/dev/null

  check_command clang "install via Xcode command line tools" || return 1
  log "  clang: $(clang --version | head -n 1)"

  check_command cmake || return 1
  log "  cmake: $(cmake --version | head -n 1)"

  check_command ninja || return 1
  log "  ninja: $(ninja --version)"

  if command -v xcrun >/dev/null 2>&1 && xcrun --find metal >/dev/null 2>&1; then
    log "  Metal SDK detected at $(xcrun --find metal)."
  else
    log "warning: 'metal' compiler not found; install Xcode for full GPU builds." 
  fi
}

validate_linux() {
  log "Validating Linux toolchain..."
  check_command clang || return 1
  log "  clang: $(clang --version | head -n 1)"

  check_command gcc || return 1
  log "  gcc: $(gcc --version | head -n 1)"

  check_command cmake || return 1
  log "  cmake: $(cmake --version | head -n 1)"

  check_command ninja || return 1
  log "  ninja: $(ninja --version)"

  if command -v vulkaninfo >/dev/null 2>&1; then
    vulkaninfo --summary || log "warning: vulkaninfo present but returned non-zero status"
  else
    log "warning: 'vulkaninfo' not found; install the Vulkan SDK for GPU backends."
  fi
}

validate_windows() {
  log "Validating Windows toolchain via PowerShell script..."
  if [[ ! -f "${SCRIPT_DIR}/validate-toolchains.ps1" ]]; then
    log "error: missing validate-toolchains.ps1 script for Windows checks."
    return 1
  fi

  if ! command -v pwsh >/dev/null 2>&1; then
    log "error: PowerShell (pwsh) is required to run Windows validation."
    return 1
  fi

  pwsh -File "${SCRIPT_DIR}/validate-toolchains.ps1"
}

main() {
  local uname_out
  uname_out="$(uname -s || echo "")"
  case "${uname_out}" in
    Darwin)
      validate_macos
      ;;
    Linux)
      validate_linux
      ;;
    MINGW*|MSYS*|CYGWIN*|Windows_NT)
      validate_windows
      ;;
    *)
      log "warning: unrecognized platform '${uname_out}'. Skipping validation."
      ;;
  esac

  log "Toolchain validation completed."
}

main "$@"
