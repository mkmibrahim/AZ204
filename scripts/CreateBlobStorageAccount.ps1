$azureLocation ="westeurope"
$resourceGroupName= "StorageAccount_ResourceGroup"
$BlobStorageAccountName = "blobazureaccount202"
  
  
Write-Host "Creating Azure resource group " $resourceGroupName
$resourceGroupCheck=(az group list --query "[?name=='$resourceGroupName']" -o tsv)
if($resourceGroupCheck){
    Write-Host "Resource group $resourceGroupName already exists."
} else {
    az group create -n $resourceGroupName --location $azureLocation
    Write-Host "Resource Group $resourceGroupName created."
}

Write-Host "Creating block blob storage account " $VMName
az storage account create --resource-group $resourceGroupName --name $BlobStorageAccountName --location $azureLocation --kind BlockBlobStorage --sku Premium_LRS