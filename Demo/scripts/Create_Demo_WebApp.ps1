$resourceGroupName= 'MsLearn'
$appName= "az204DemoApp123"
$appServicePlanName= "az204DemoAppServicePlan123"
$backendAppName = "az204DemoBackendApp123"
$azureLocation ="westeurope"

$location = Get-Location

Set-Location ..\vueapp

Write-Host "Creating production build"
npm run build

Set-Location dist

Write-Host "Creating Azure resource group " $resourceGroupName
$resourceGroupCheck=(az group list --query "[?name=='$resourceGroupName']" -o tsv)
if($resourceGroupCheck){
    Write-Host "Resource group $resourceGroupName already exists."
} else {
    az group create -l westeurope  -n $resourceGroupName --location $azureLocation
    Write-Host "Resource Group $resourceGroupName created."
}

Write-Host "Creating AppService plan" $appServicePlanName
$AppservicePlanCheck=(az appservice plan list --query "[?name=='$appServicePlanName']" -o tsv)
if($AppservicePlanCheck){
    Write-Host "Appservice plan $appServicePlanName already exists."
} else {
    az appservice plan create --name $appServicePlanName --resource-group $resourceGroupName --sku FREE --location $azureLocation
    Write-Host "Resource Group $appServicePlanName created."
}


Write-Host "Publishing frontend WebApp to Azure"
az webapp up -g $resourceGroupName -n $appName --html --plan $appServicePlanName --location $azureLocation

Write-Host "Publishing backend WebApp to Azure"
az webapp create --resource-group $resourceGroupName --plan $appServicePlanName --name $backendAppName --deployment-local-git 

Set-Location ..\..\backend\

git push azure main:master
Set-Location $location