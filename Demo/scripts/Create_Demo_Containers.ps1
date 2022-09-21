$location = Get-Location

cd ..\vueapp\scripts
Write-Host "Path: " $PSScriptRoot
$scriptToRun = $PSScriptRoot+"\..\vueapp\scripts\DeployContainerVueAppToAzure.ps1"
& $scriptToRun
Set-Location $location

cd ..\backend\scripts
$scriptToRun = $PSScriptRoot+"\..\backend\scripts\DeployContainerbackendToAzure.ps1"
& $scriptToRun
$scriptToRun = $PSScriptRoot+"\..\backend\scripts\DeployContainerCityImagesToAzure.ps1"
& $scriptToRun
Set-Location $location