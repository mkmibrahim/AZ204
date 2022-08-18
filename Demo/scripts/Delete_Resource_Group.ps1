$resourceGroupName= 'MsLearn'

Write-Host "Deleting Azure resource group" $resourceGroupName
az group delete --name $resourceGroupName --yes