Param(
    [switch]$VerboseOutput
)

$ErrorActionPreference = 'Stop'

function Write-Info($message) {
    Write-Host $message
}

function Ensure-Command($name, $hint) {
    if (-not (Get-Command $name -ErrorAction SilentlyContinue)) {
        throw "Required command '$name' not found. $hint"
    }
}

Write-Info "Validating Windows toolchain..."

Ensure-Command 'vswhere' "Install Visual Studio 2022 with Desktop development with C++ workload."

$vsInstall = & vswhere -latest -requires Microsoft.Component.MSBuild -property installationPath
if (-not $vsInstall) {
    throw "Visual Studio installation not found."
}

Write-Info "  Visual Studio: $vsInstall"

$vcVarsPath = Join-Path $vsInstall 'VC\Auxiliary\Build\vcvars64.bat'
if (-not (Test-Path $vcVarsPath)) {
    throw "MSVC vcvars64.bat not found. Ensure C++ workloads are installed."
}
Write-Info "  MSVC vcvars64.bat located."

Ensure-Command 'cmake' "Install CMake and ensure it is in PATH."
if ($VerboseOutput) {
    cmake --version
}

Ensure-Command 'ninja' "Install Ninja and ensure it is in PATH."
if ($VerboseOutput) {
    ninja --version
}

Ensure-Command 'python' "Python 3 is required for Premake bootstrap scripts."

if (Test-Path 'C:\VulkanSDK') {
    $latestSdk = Get-ChildItem 'C:\VulkanSDK' | Sort-Object Name -Descending | Select-Object -First 1
    if ($latestSdk) {
        Write-Info "  Vulkan SDK detected: $($latestSdk.Name)"
    }
} else {
    Write-Info "warning: Vulkan SDK not detected under C:\VulkanSDK. Install from https://vulkan.lunarg.com/sdk/home#windows"
}

Write-Info "Windows toolchain validation completed."
