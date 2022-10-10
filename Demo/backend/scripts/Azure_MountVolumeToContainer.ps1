$resourceGroupNamePers= "MsLearnPersistent"
$StorageAccountName="az204demoaccount123"
$FileShareName="az204demoshare123"
$customId = "12323423"
$cityweatherappContainerName = "az204DemoAzureCityWeatherApp123"

$resourceGroupName= "MsLearn"
$cityweatherappContainerName = "az204DemoAzureCityWeatherApp123"
$containerMountPath = "/Foo"

$StorageKey=(az storage account keys list --resource-group $resourceGroupNamePers --account-name $StorageAccountName --query "[0].value" --output tsv)
Write-Host "Storage key of file share is " $StorageKey
az webapp config storage-account add -g $resourceGroupName -n $cityweatherappContainerName --custom-id $customId --storage-type AzureFiles --account-name $StorageAccountName --share-name $FileShareName --access-key $StorageKey --mount-path $containerMountPath

az webapp config storage-account list --resource-group $resourceGroupName --name $cityweatherappContainerName
