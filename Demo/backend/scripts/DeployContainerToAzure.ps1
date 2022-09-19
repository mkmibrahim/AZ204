$resourceGroupName= "MsLearn"
$azureLocation ="westeurope"
$appName= "az204DemoApp123"
#$appServicePlanName= "az204DemoAppServicePlan123"
#$backendAppName = "az204DemoBackendApp123"
#$AzureFunctionAppName = "az204DemoAzureFunctionBackendApp123"
#$AzureStorageAccountName = "az204demostorageaccount1"
$AzureContainerRegistry = "az204containerregistry123"
$AzureContainerBackendImageName = "demo_dockerize-backend-app"
$appServicePlanContainerName= "az204DemoAppServicePlanContainer123"
$backendappContainerName = "az204DemoContainerBackendApp123"

$location = Get-Location

cd ..\backend
Write-Host "Creating Docker Image Backend app"
docker build -t $AzureContainerBackendImageName .

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
    Write-Host "Azure Container Registry $AzureContainerRegistry already exists"
} else {
    Write-Host "Creating Azure Container Registry " $AzureContainerRegistry
    az acr create --resource-group $resourceGroupName --name $AzureContainerRegistry --sku Basic  
}

Write-Host "Logging in to Azure Container Registry " $AzureContainerRegistry
az acr login --name $AzureContainerRegistry


Write-Host "Tagging image"
docker tag demo_dockerize-backend-app az204containerregistry123.azurecr.io/demo_dockerize-backend-app:latest
#docker tag $AzureContainerBackendImageName $AzureContainerRegistry.azurecr.io/$AzureContainerBackendImageName:latest

docker push az204containerregistry123.azurecr.io/demo_dockerize-backend-app:latest

az appservice plan create --name $appServicePlanContainerName --resource-group $resourceGroupName --is-linux

az webapp create --resource-group $resourceGroupName --plan $appServicePlanContainerName --name $backendappContainerName --deployment-container-image-name az204containerregistry123.azurecr.io/demo_dockerize-backend-app:latest

az webapp config appsettings set --resource-group $resourceGroupName --name $backendappContainerName --settings WEBSITES_PORT=80

$AzurePrincipalId = (az webapp identity assign --resource-group $resourceGroupName --name $backendappContainerName --query principalId --output tsv)

Write-Host "Azure PrincipalId is " $AzurePrincipalId

$AzureSubscriptionId = (az account show --query id --output tsv)

Write-Host "Azure subscriptionId is " $AzureSubscriptionId

Write-Host "Granting the managed identity permission to access the container registry"
az role assignment create --assignee $AzurePrincipalId --scope /subscriptions/$AzureSubscriptionId/resourceGroups/$resourceGroupName/providers/Microsoft.ContainerRegistry/registries/az204containerregistry123 --role "AcrPull"

Write-Host "Configuring app to use the managed identity to pull from Azure Container Reg"
az resource update --ids /subscriptions/$AzureSubscriptionId/resourceGroups/$resourceGroupName/providers/Microsoft.Web/sites/$appName/config/web --set properties.acrUseManagedIdentityCreds=True

Write-Host "Deploying the image"
az webapp config container set --name $backendappContainerName --resource-group $resourceGroupName --docker-custom-image-name az204containerregistry123.azurecr.io/demo_dockerize-backend-app:latest --docker-registry-server-url https://az204containerregistry123.azurecr.io

Set-Location $location