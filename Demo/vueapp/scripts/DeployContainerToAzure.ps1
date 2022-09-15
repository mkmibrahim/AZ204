$resourceGroupName= "MsLearn"
$azureLocation ="westeurope"
$appName= "az204DemoApp123"
$appServicePlanName= "az204DemoAppServicePlan123"
$backendAppName = "az204DemoBackendApp123"
$AzureFunctionAppName = "az204DemoAzureFunctionBackendApp123"
$AzureStorageAccountName = "az204demostorageaccount1"
$AzureContainerRegistry = "az204containerregistry123"
$AzureContainerImageName = "demo_dockerize-vuejs-app"
$appServicePlanContainerName= "az204DemoAppServicePlanContainer123"
$appContainerName = "az204DemoContainerApp123"

$location = Get-Location

cd..
Write-Host "Creating Docker Image"
docker build -t $AzureContainerImageName .

Write-Host "Creating Azure resource group " $resourceGroupName
$resourceGroupCheck=(az group list --query "[?name=='$resourceGroupName']" -o tsv)
if($resourceGroupCheck){
    Write-Host "Resource group $resourceGroupName already exists."
} else {
    az group create -l westeurope  -n $resourceGroupName --location $azureLocation
    Write-Host "Resource Group $resourceGroupName created."
}

$AzureContainerRegistryCheck=(az acr list --query "[?name=='$AzureContainerRegistry']" -o tsv)
if($AzureContainerRegistryCheck){
    Write-Host "Azure Container Reigstry $AzureContainerRegistry already exists"
} else {
    Write-Host "Creating Azure Container Reigstry " $AzureContainerRegistry
    az acr create --resource-group $resourceGroupName --name $AzureContainerRegistry --sku Basic  
}

Write-Host "Logging in to Azure Container Registry " $AzureContainerRegistry
az acr login --name $AzureContainerRegistry

#az acr show --name $AzureContainerRegistry --query loginServer --output table

docker tag demo_dockerize-vuejs-app az204containerregistry123.azurecr.io/demo_dockerize-vuejs-app:latest
#docker tag $AzureContainerImageName $AzureContainerRegistry.azurecr.io/$AzureContainerImageName:latest

docker push az204containerregistry123.azurecr.io/demo_dockerize-vuejs-app:latest

az appservice plan create --name $appServicePlanContainerName --resource-group $resourceGroupName --is-linux

az webapp create --resource-group $resourceGroupName --plan $appServicePlanContainerName --name $appContainerName --deployment-container-image-name az204containerregistry123.azurecr.io/demo_dockerize-vuejs-app:latest

az webapp config appsettings set --resource-group $resourceGroupName --name $appContainerName --settings WEBSITES_PORT=8000

Set-Location $location