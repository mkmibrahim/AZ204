$location = Get-Location

$scriptToRun = $PSScriptRoot+".\Azure_DeployContainerFrontendCities.ps1"
& $scriptToRun

cd ..\vueapp\scripts
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
& $scriptToRun
Set-Location $location