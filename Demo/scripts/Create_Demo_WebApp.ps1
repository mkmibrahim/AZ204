$resourceGroupName= "MsLearn"
$azureLocation ="westeurope"
$appName= "az204DemoApp123"
$appServicePlanName= "az204DemoAppServicePlan123"
$backendAppName = "az204DemoBackendApp123"
#$apiServiceName ="az204api123"
$AzureFunctionAppName = "az204DemoAzureFunctionBackendApp123"
$AzureStorageAccountName = "az204demostorageaccount1"

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

#Write-Host "Create api management service"
#az apim create --name $apiServiceName --resource-group $resourceGroupName --publisher-name ContosoKsol --publisher-email admin@test.com --no-wait
# te check status use: az apim show --name "az204api123" --resource-group MsLearn --output table

Set-Location ..\..\backend\backend\

Write-Host "Publishing backend WebApp to Azure using Branch Demo"
az webapp up -g $resourceGroupName --name $backendAppName --plan $appServicePlanName --sku FREE --location $azureLocation

Set-Location $location
Set-Location ..\backend\FunctionProject\

Write-Host "Creating Storage Account for Azure Function"
az storage account create -n $AzureStorageAccountName -g $resourceGroupName --location $azureLocation --sku Standard_RAGRS --kind StorageV2

Write-Host "Creating Azure Function in Azure"
az functionapp create -g $resourceGroupName -n $AzureFunctionAppName  --runtime dotnet --storage-account $AzureStorageAccountName --consumption-plan-location $azureLocation --functions-version 4

Start-Sleep -Seconds 10

Write-Host "Publishing AzureFunction"
#az functionapp deployment source config-zip -g $resourceGroupName --name $AzureFunctionAppName --src $publishZipPath
func azure functionapp publish $AzureFunctionAppName --publish-local-settings 

Set-Location $location