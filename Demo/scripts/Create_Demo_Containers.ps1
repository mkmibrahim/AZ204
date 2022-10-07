$location = Get-Location

cd ..\vueapp\scripts
Write-Host "Path: " $PSScriptRoot
$scriptToRun = $PSScriptRoot+"\..\vueapp\scripts\Azure_DeployContainerVueApp.ps1"
& $scriptToRun
Set-Location $location

cd ..\backend\scripts
$scriptToRun = $PSScriptRoot+"\..\backend\scripts\Azure_DeployContainerbackend.ps1"
& $scriptToRun
$scriptToRun = $PSScriptRoot+"\..\backend\scripts\Azure_DeployContainerCityImages.ps1"
& $scriptToRun
$scriptToRun = $PSScriptRoot+"\..\backend\scripts\Azure_DeployContainerCityWeather.ps1"
& $scriptToRun
Set-Location $location