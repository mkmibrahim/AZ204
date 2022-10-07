$resourceGroupName= "MsLearnPersistent"
$azureLocation ="westeurope"
$StorageAccountName="az204demoaccount123"
$FileShareName="az204demoshare123"

Write-Host "Creating Azure resource group " $resourceGroupName
$resourceGroupCheck=(az group list --query "[?name=='$resourceGroupName']" -o tsv)
if($resourceGroupCheck){
    Write-Host "Resource group $resourceGroupName already exists."
} else {
    az group create -l westeurope  -n $resourceGroupName --location $azureLocation
    Write-Host "Resource Group $resourceGroupName created."
}

Write-Host "Creating Azure storage account " $StorageAccountName
az storage account create --resource-group $resourceGroupName --name $StorageAccountName --location $azureLocation --sku Standard_LRS

Write-Host "Creating Azure file share " $FileShareName
az storage share create --name $FileShareName --account-name $StorageAccountName

$StorageKey=(az storage account keys list --resource-group $resourceGroupName --account-name $StorageAccountName --query "[0].value" --output tsv)
Write-Host "Storage key of file share is " $StorageKey