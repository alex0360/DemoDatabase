# --------------------------------------------------------------------------------------------------------------------
# <copyright file="build.ps1" company="Tygertec">
#   Copyright © 2016 Ty Walls.
#   All rights reserved.
# </copyright>
# <summary>
#   Script for kicking off the command line build.
# </summary>
# --------------------------------------------------------------------------------------------------------------------

param([Parameter(Position=0, Mandatory=$false)] [string[]]$taskList=@())

# Get path base
$solutionDirectory = Split-Path -Parent $MyInvocation.MyCommand.Path

$nugetDirectory = Join-Path $solutionDirectory ".nuget"
$packageDirectory = Join-Path $solutionDirectory "packages"

# Make sure we have a version of the nuget executable
$nuget = Get-Command NuGet -ErrorAction SilentlyContinue

if ($nuget -eq $null) {    
	$nuget = ".\nuget.exe"
    echo " >> Downloading nuget to this directory: $($nuget)"
    iwr https://www.nuget.org/nuget.exe -OutFile $nuget
}

# Restore nuget packages required by the build script
# Write-Host "Restoring nuget packages required by the build"
# & $nuget restore -OutputDirectory $packageDirectory -ConfigFile $nugetDirectory\packages.config -Verbosity quiet

Remove-Module [p]sake

# Find path to psake
$psake_path = (Get-ChildItem $solutionDirectory\packages\psake.* | Sort-Object Name | Select-Object -First 1).FullName

Import-Module $psake_path\tools\psake\psake.psm1

Invoke-psake `
	-buildFile $solutionDirectory\scripts\command\build\default.ps1 `
	-taskList $taskList `
	-framework 4.7.2 `
	-properties @{
		"buildConfiguration" = "Release"
		"buildPlatform" = "Any CPU"} `
	-parameters @{ 
		"solutionFile" = ".\DemoDatabase.sln"}

Write-Output ("`r`nBuild finished with code: {0}" -f $LASTEXITCODE)
exit $LASTEXITCODE