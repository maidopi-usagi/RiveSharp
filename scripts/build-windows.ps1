Param(
    [ValidateSet('Debug','Release')]
    [string[]]$Configurations = @('Release'),

    [ValidateSet('x64','ARM64')]
    [string[]]$Architectures = @('x64'),

    [switch]$SkipNative,
    [switch]$SkipManaged
)

$ErrorActionPreference = 'Stop'
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path
$RootDir = Resolve-Path (Join-Path $ScriptDir '..')

function Invoke-RiverRendererBuild {
    param(
        [string]$Arch,
        [string]$Config
    )

    Push-Location (Join-Path $RootDir 'extern/river-renderer')
    try {
        $env:RIVE_PREMAKE_ARGS = '--with_rive_text'
        ./build/build_rive.sh $Config -- rive_pls_renderer | Write-Output
    }
    finally {
        Pop-Location
    }

    $buildDir = Join-Path $RootDir "renderer_ffi/build-$Arch-$Config"
    $generator = 'Ninja'
    & cmake -S (Join-Path $RootDir 'renderer_ffi') -B $buildDir -G $generator -DCMAKE_BUILD_TYPE=$Config -DCMAKE_MSVC_RUNTIME_LIBRARY="MultiThreaded$($Config -eq 'Debug' ? 'Debug' : '')" | Write-Output
    & cmake --build $buildDir --config $Config | Write-Output

    $outputDir = Join-Path $buildDir "out/$Config"
    $ffiBinary = Join-Path $outputDir 'rive_renderer_ffi.dll'
    if (-not (Test-Path $ffiBinary)) {
        throw "Native build did not produce $ffiBinary"
    }

    $runtimeRoot = Join-Path $RootDir "dotnet/RiveRenderer/runtimes/win-$Arch/native/$Config"
    $artifactRoot = Join-Path $RootDir "artifacts/native/win-$Arch/$Config"
    New-Item -ItemType Directory -Force -Path $runtimeRoot | Out-Null
    New-Item -ItemType Directory -Force -Path $artifactRoot | Out-Null
    Copy-Item $ffiBinary $runtimeRoot -Force

    $nativeLibs = @('rive.lib','rive_harfbuzz.lib','rive_sheenbidi.lib','rive_yoga.lib','rive_pls_renderer.lib','miniaudio.lib')
    foreach ($lib in $nativeLibs) {
        $source = Join-Path $RootDir "extern/river-renderer/out/${Arch.ToLower()}_$($Config.ToLower())/$lib"
        if (Test-Path $source) {
            Copy-Item $source $runtimeRoot -Force
        }
    }

    Copy-Item (Join-Path $runtimeRoot '*') $artifactRoot -Force
    (Get-ChildItem -Path $artifactRoot -File | Sort-Object Name | Select-Object -ExpandProperty Name) | Out-File (Join-Path $artifactRoot 'manifest.txt') -Encoding utf8
}

if (-not $SkipNative) {
    foreach ($config in $Configurations) {
        foreach ($arch in $Architectures) {
            Invoke-RiverRendererBuild -Arch $arch -Config $config
        }
    }
}

if (-not $SkipManaged) {
    foreach ($config in $Configurations) {
        Push-Location (Join-Path $RootDir 'dotnet')
        try {
            dotnet build RiveRenderer.sln -c $config | Write-Output
        }
        finally {
            Pop-Location
        }
    }
}
