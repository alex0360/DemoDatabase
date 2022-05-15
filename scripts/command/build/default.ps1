Include .\psake_helpers.ps1

$here = Split-Path -Parent $MyInvocation.MyCommand.Path

$solutionDirectory = Split-Path -Parent $here
$solutionDirectory = Split-Path -Parent $solutionDirectory
$solutionDirectory = Split-Path -Parent $solutionDirectory

properties {

    $outputDirectory = Join-Path $solutionDirectory ".build"
    
    #$nunit = Join-Path $(Find-PackagePath $packageDirectory "NUnit.ConsoleRunner") "Tools\nunit3-console.exe"

    $nunit = Join-Path $(Find-PackagePath $packageDirectory "NUnit.ConsoleRunner") "Tools\nunit3-console.exe"

	$git = Get-Command git -ErrorAction SilentlyContinue | Select-Object -Last 1
}

FormatTaskName ">>>-- Executing {0} Task -->"

Task ? -description "List tasks" -alias "Help" { WriteDocumentation }

Task Default -depends BuildAndRunAllTests -description "Default task"

Task BuildAndRunAllTests `
	-alias "BakeAndShake" `
	-description "Build solution and run all tests" `
	-depends Unit.Tests

Task Check-Environment `
	-description "Verify parameters and build tools" `
	-requiredVariables $solutionFile, $buildConfiguration, $buildPlatform, $outputDirectory `
{
	Assert ("Debug", "Release" -contains $buildConfiguration) `
		"Invalid build configuration '$buildConfiguration'. Valid values are 'Debug' or 'Release'"

	Assert ("x86", "x64", "Any CPU" -contains $buildPlatform) `
		"Invalid build platform '$buildPlatform'. Valid values are 'x86', 'x64', or 'Any CPU'"

	Assert (Test-Path $nunit) `
		"NUnit console test runner could not be found"

    Assert ($git -ne $null) `
		"Git is not in your command path. Install Git."
}

formatTaskName { 
    param($taskName) 

    $divider = "-" * 70
    return "`r`n$divider`r`n$taskName`r`n$divider"
}

Task Requires.MSBuild {

    $script:build = resolve-path "C:\Program Files (x86)\Microsoft Visual Studio\*\*\MSBuild\*\Bin\MSBuild.exe" | Select-Object -Last 1

    if ($build -eq $null)
    {
        throw "Failed to find MSBuild"
    }

    Write-Host "Found MSBuild here: $build"
}

Task Requires.BuildDir {
    if (!(test-path $outputDirectory))
    {
        Write-Host "Creating build folder $outputDirectory"
        mkdir $outputDirectory > $null
    }
}

Task Compile.Assembly -Depends Requires.MSBuild, Requires.BuildDir {
    exec { 
        & $build $solutionFile "/p:Configuration=$buildConfiguration;OutDir=$outputDirectory" /m /v:M /fl /nr:false
    }
}

Task Unit.Tests -Depends Compile.Assembly {
    exec {
        & $nunit $outputDirectory\$testProject.dll
    }
}