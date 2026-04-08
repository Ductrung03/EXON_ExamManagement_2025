param(
    [string]$Configuration = "Release",
    [string]$OutputRoot = "artifacts\publish",
    [string]$TxTextControlInstallDir = ""
)

$ErrorActionPreference = "Stop"
Set-StrictMode -Version Latest

$repoRoot = Split-Path -Parent $PSScriptRoot
$solutionPath = Join-Path $repoRoot "EXON_ExamManagement.sln"
$toolsDir = Join-Path $repoRoot "tools"
$nugetExe = Join-Path $toolsDir "nuget.exe"
$outputRootPath = Join-Path $repoRoot $OutputRoot

$projects = @(
    @{ Name = "EXON.MONITOR"; Path = Join-Path $repoRoot "EXON.MONITOR\EXON.Monitor.csproj" },
    @{ Name = "EXON.GradedEssay"; Path = Join-Path $repoRoot "EXON.GradedEssay\EXON.GradedEssay.csproj" },
    @{ Name = "QuanLyHoiDongThiVer2"; Path = Join-Path $repoRoot "QuanLyHoiDongThiVer2\QuanLyHoiDongThiVer2.csproj" }
)

function Get-MSBuildCommand {
    $vsWhere = Join-Path ${env:ProgramFiles(x86)} "Microsoft Visual Studio\Installer\vswhere.exe"
    if (Test-Path $vsWhere) {
        $installationPath = & $vsWhere -latest -requires Microsoft.Component.MSBuild -find "MSBuild\**\Bin\MSBuild.exe" | Select-Object -First 1
        if ($installationPath) {
            return @{ FilePath = $installationPath; Prefix = @() }
        }
    }

    throw "MSBuild.exe not found. Install Visual Studio Build Tools with .NET Framework desktop build support."
}

if (-not (Test-Path $toolsDir)) {
    New-Item -ItemType Directory -Path $toolsDir | Out-Null
}

if (-not (Test-Path $nugetExe)) {
    Invoke-WebRequest -Uri "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe" -OutFile $nugetExe
}

& $nugetExe restore $solutionPath | Out-Host

$msbuild = Get-MSBuildCommand

foreach ($project in $projects) {
    $projectOutput = Join-Path $outputRootPath $project.Name
    if (Test-Path $projectOutput) {
        Remove-Item -Recurse -Force $projectOutput
    }

    $arguments = @()
    $arguments += $msbuild.Prefix
    $arguments += $project.Path
    $arguments += "/p:Configuration=$Configuration"
    $arguments += "/p:Platform=AnyCPU"
    $arguments += "/p:OutDir=$projectOutput\"
    $arguments += "/verbosity:minimal"

    if ($TxTextControlInstallDir) {
        $arguments += "/p:TxTextControlInstallDir=$TxTextControlInstallDir"
    }

    & $msbuild.FilePath @arguments | Out-Host
}

Write-Host "Portable outputs created under $outputRootPath"
