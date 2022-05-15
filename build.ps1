param([Parameter(Position=0, Mandatory=$false)] [string[]]$taskList=@())

# Get path base
$solutionDirectory = Split-Path -Parent $MyInvocation.MyCommand.Path

$projectDirectory = Join-Path $solutionDirectory "TestDataBase"

$nugetDirectory = Join-Path $solutionDirectory ".nuget"
$packageDirectory = Join-Path $solutionDirectory "packages"

# Make sure we have a version of the nuget executable
$nuget = Get-Command NuGet -ErrorAction SilentlyContinue | Select-Object -Last 1

#if ($nuget -eq $null) {    
#	$nuget = ".\nuget.exe"
#    echo " >> Downloading nuget to this directory: $($nuget)"
#    iwr https://www.nuget.org/nuget.exe -OutFile $nuget
#}

# Restore nuget packages required by the build script
# Write-Host "Restoring nuget packages required by the build"
# & $nuget restore -OutputDirectory $packageDirectory -ConfigFile $nugetDirectory\packages.config -Verbosity quiet

Remove-Module [p]sake

# Find path to psake
$psake_path = (Get-ChildItem $solutionDirectory\packages\psake.* | Sort-Object Name | Select-Object -First 1).FullName

Import-Module $psake_path\tools\psake\psake.psm1

$invoke = Invoke-psake `
	-buildFile $solutionDirectory\scripts\command\build\default.ps1 `
	-taskList $taskList `
	-parameters @{ 
		"solutionFile" = $(Get-ChildItem -Path $solutionDirectory -Filter *.sln | Select -First 1).FullName
        "testProject" = "TestDataBase"
        "buildConfiguration" = "Release"
        "net" = "net472"
        "buildPlatform" = "Any CPU"}


$result = $invoke | ConvertTo-Json

$isFail = $result.ToString() -match '[E|e](rror|xception)'

if ($isFail)
{
    $LASTEXITCODE = 1

    Write-Output ("`r`nMessage: {0}" -f $result.ToString())

    exit $LASTEXITCODE
}

Write-Output ("`r`nBuild finished with code: {0}" -f $LASTEXITCODE)

exit $LASTEXITCODE
