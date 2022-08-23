$resourceGroupName= 'MsLearn'
$azureLocation ="westeurope"
$storageAccountName = "az204storage123"
$appName= "az204functionApp123"
#$appServicePlanName= "az204DemoAppServicePlan123"
#$backendAppName = "az204DemoBackendApp123"

$location = Get-Location

Write-Host "Creating Azure resource group " $resourceGroupName
$resourceGroupCheck=(az group list --query "[?name=='$resourceGroupName']" -o tsv)
if($resourceGroupCheck){
    Write-Host "Resource group $resourceGroupName already exists."
} else {
    az group create -l westeurope  -n $resourceGroupName --location $azureLocation
    Write-Host "Resource Group $resourceGroupName created."
}

Write-Host "Creating Azure storage account " $storageAccountName
$storageAccountCheck=(az storage account list --query "[?name=='$storageAccountName']" -o tsv)
if($storageAccountCheck){
    Write-Host "Storage account $storageAccountName already exists."
} else {
    az storage account create --name $storageAccountName --location $azureLocation --resource-group $resourceGroupName --sku Standard_LRS
    Write-Host "Storage account $storageAccountName created."
}

Write-Host "Creating Azure Function App " $appName
$appCheck=(az functionapp list --query "[?name=='$appName']" -o tsv)
if($appCheck){
    Write-Host "Azure function app $appName already exists."
} else {
    az functionapp create --resource-group $resourceGroupName --consumption-plan-location $azureLocation --runtime dotnet --functions-version 3 --name $appName --storage-account $storageAccountName
    Write-Host "Azure function app $appName created."
}

Write-Host "Deploying " $appName
Set-Location ..\FunctionProject
func azure functionapp publish $appName --force

Set-Location $location