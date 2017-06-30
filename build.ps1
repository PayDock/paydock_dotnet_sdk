Param(
	$configuration = "debug",
	$version = $null
)

function Get-EffectiveVersion {	
	if (($version -eq $null) -and ($env:PIPELINE_VERSION -ne $null)){        
        $version = $env:PIPELINE_VERSION
    }
	$version
}

$nunitrunners = "tools\NUnit.Runners"
$fxcopbuildtools = "tools\FxCop.BuildTools"
$faketools = "tools\FAKE\tools"
$nowutc = Get-Date
$timestamp = '{0:yyyyMMdd-HHmmss}' -f $nowutc
if(!(Test-Path -Path $nunitrunners )){
    & "tools\nuget\nuget.exe" "install" "NUnit.Runners" "-Version" "3.6.1" "-OutputDirectory" "tools" "-ExcludeVersion" "-source" "https://www.nuget.org/api/v2;https://api.nuget.org/v3/index.json"
}

if(!(Test-Path -Path $fxcopbuildtools )){
    & "tools\nuget\nuget.exe" "install" "FxCop.BuildTools" "-OutputDirectory" "tools" "-ExcludeVersion" "-source" "https://www.nuget.org/api/v2;https://api.nuget.org/v3/index.json"
}

if(!(Test-Path -Path $faketools )){
	Write-Output "FAKE is not detected thus installing..."
    & "tools\nuget\nuget.exe" "install" "FAKE" "-OutputDirectory" "tools" "-ExcludeVersion" "-source" "https://www.nuget.org/api/v2;https://api.nuget.org/v3/index.json"
}

$effectiveVersion = Get-EffectiveVersion
Write-Output "Evaluated effectiveVersion is: $effectiveVersion"

if(Test-Path -Path $faketools ){
	Write-Output "Running FAKE..."
	Write-Output "Started: $timestamp"
	Write-Output "Configuration: $($configuration)"
	Write-Output "BuildVersion: $($effectiveVersion)"
    & "tools\FAKE\tools\Fake.exe" build.fsx configuration=$($configuration) version=$($effectiveVersion)
	exit $LastExitCode
}