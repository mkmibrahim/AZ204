$resourceGroupName= 'MsLearn'
$azureLocation ="westeurope"
$appName= "az204DemoApp123"
$appServicePlanName= "az204DemoAppServicePlan123"
$backendAppName = "az204DemoBackendApp123"
#$apiServiceName ="az204api123"

$location = Get-Location

Set-Location ..\backend\backend\

Write-Host "Publishing backend WebApp to Azure using Branch Demo"
az webapp up -g $resourceGroupName --name $backendAppName --plan $appServicePlanName --sku FREE --location $azureLocation

Set-Location $location