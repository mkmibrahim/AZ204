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
    Write-Host "Azure Container Registry $AzureContainerRegistry already exists"
} else {
    Write-Host "Creating Azure Container Registry " $AzureContainerRegistry
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

az webapp config appsettings set --resource-group $resourceGroupName --name $appContainerName --settings WEBSITES_PORT=8080

$AzurePrincipalId = (az webapp identity assign --resource-group $resourceGroupName --name $appContainerName --query principalId --output tsv)

Write-Host "Azure PrincipalId is " $AzurePrincipalId

$AzureSubscriptionId = (az account show --query id --output tsv)

Write-Host "Azure subscriptionId is " $AzureSubscriptionId

Write-Host "Granting the managed identity permission to access the container registry"
az role assignment create --assignee $AzurePrincipalId --scope /subscriptions/$AzureSubscriptionId/resourceGroups/$resourceGroupName/providers/Microsoft.ContainerRegistry/registries/az204containerregistry123 --role "AcrPull"

Write-Host "Configuring app to use the managed identity to pull from Azure Container Reg"
az resource update --ids /subscriptions/$AzureSubscriptionId/resourceGroups/$resourceGroupName/providers/Microsoft.Web/sites/$appContainerName/config/web --set properties.acrUseManagedIdentityCreds=True

Write-Host "Deploying the image"
az webapp config container set --name $appContainerName --resource-group $resourceGroupName --docker-custom-image-name az204containerregistry123.azurecr.io/demo_dockerize-vuejs-app:latest --docker-registry-server-url https://az204containerregistry123.azurecr.io

Set-Location $location