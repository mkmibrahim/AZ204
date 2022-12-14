$resourceGroupName= 'MsLearn'
$azureLocation ="westeurope"
$appName= "az204DemoApp123"
$appServicePlanName= "az204DemoAppServicePlan123"
$backendAppName = "az204DemoBackendApp123"
$AzureFunctionAppName = "az204DemoAzureFunctionBackendApp123"
$AzureStorageAccountName = "az204demostorageaccount1"


$location = Get-Location

Set-Location ..\backend\FunctionProject\

#publish the code 
#dotnet publish -c Release
#$publishFolder = "Demo\backend\FunctionProject\bin\Release\net6.0\publish\"

#Write-Host "Creating AzureFunction Zipfile"
#$publishZip = "publish.zip"
#$publishZipPath = Join-Path -path $location ..\ $publishZip

#if(Test-Path $publishZipPath)
#{Remove-item $publishZipPath}

#Add-Type -Assembly "system.io.compression.filesystem"
#[io.compression.zipfile]::CreateFromDirectory($publishFolder,$publishZipPath)

Write-Host "Creating Storage Account for Azure Function"
az storage account create -n $AzureStorageAccountName -g $resourceGroupName --location $azureLocation --sku Standard_RAGRS --kind StorageV2

Write-Host "Creating Azure Function in Azure"
az functionapp create -g $resourceGroupName -n $AzureFunctionAppName  --runtime dotnet --storage-account $AzureStorageAccountName --consumption-plan-location $azureLocation --functions-version 4

Start-Sleep -Seconds 10

Write-Host "Publishing AzureFunction"
#az functionapp deployment source config-zip -g $resourceGroupName --name $AzureFunctionAppName --src $publishZipPath
func azure functionapp publish $AzureFunctionAppName --publish-local-settings 

Set-Location $location