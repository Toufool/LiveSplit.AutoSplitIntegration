#!/usr/bin/env pwsh
# Builds and launches the settings-UI preview (tools/PreviewSettings), so visual changes
# can be checked without a running LiveSplit. On Windows the built exe runs directly; on
# Linux/macOS it runs under Mono (WinForms via libgdiplus). See CONTRIBUTING.md.
$ErrorActionPreference = 'Stop'

$repoRoot = Split-Path -Parent $PSScriptRoot
$project = Join-Path $repoRoot 'tools/PreviewSettings/PreviewSettings.csproj'
$outDir = Join-Path $repoRoot 'tools/PreviewSettings/bin/Release/net481'
$exe = Join-Path $outDir 'PreviewSettings.exe'

dotnet build $project --configuration Release

if ($IsWindows) {
    & $exe
}
else {
    if (-not (Get-Command mono -ErrorAction SilentlyContinue)) {
        throw "mono not found. Install Mono (with libgdiplus) to run the preview on this OS."
    }
    if (-not $env:DISPLAY) {
        throw "No X DISPLAY set. Run from a graphical session (or set DISPLAY)."
    }
    & mono $exe
}
