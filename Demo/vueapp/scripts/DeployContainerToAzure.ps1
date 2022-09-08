$resourceGroupName= "MsLearn"
$azureLocation ="westeurope"
$appName= "az204DemoApp123"
$appServicePlanName= "az204DemoAppServicePlan123"
$backendAppName = "az204DemoBackendApp123"
#$apiServiceName ="az204api123"
$AzureFunctionAppName = "az204DemoAzureFunctionBackendApp123"
$AzureStorageAccountName = "az204demostorageaccount1"
$AzureContainerRegistry = "az204containerregistry123"
$AzureContainerImageName = "demo_dockerize-vuejs-app"

$location = Get-Location

cd..
Write-Host "Creating Docker Image"
#docker build -t $AzureContainerImageName .

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

az acr show --name $AzureContainerRegistry --query loginServer --output table

docker tag $AzureContainerImageName "$AzureContainerRegistry.azurecr.io"/$AzureContainerImageName:v1


docker images

Set-Location $location