Param(
    [ValidateSet('Debug','Release')]
    [string]$Configuration = 'Release',

    [switch]$SkipManaged
)

$ErrorActionPreference = 'Stop'
$ScriptDir = Split-Path -Parent $MyInvocation.MyCommand.Path

& "$ScriptDir/build-windows.ps1" -Configuration $Configuration -SkipManaged:$SkipManaged

if (-not $SkipManaged) {
    & "$ScriptDir/build-managed.sh" -c $Configuration
}
