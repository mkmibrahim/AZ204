$resourceGroupName= 'MsLearn'
$azureLocation ="westeurope"
$appName= "az204DemoApp123"
$appServicePlanName= "az204DemoAppServicePlan123"
$backendAppName = "az204DemoBackendApp123"
#$apiServiceName ="az204api123"

$location = Get-Location

Set-Location ..\vueapp

Write-Host "Creating production build"
npm run build

Set-Location dist

Write-Host "Creating Azure resource group " $resourceGroupName
$resourceGroupCheck=(az group list --query "[?name=='$resourceGroupName']" -o tsv)
if($resourceGroupCheck){
    Write-Host "Resource group $resourceGroupName already exists."
} else {
    az group create -l westeurope  -n $resourceGroupName --location $azureLocation
    Write-Host "Resource Group $resourceGroupName created."
}

Write-Host "Creating AppService plan" $appServicePlanName
$AppservicePlanCheck=(az appservice plan list --query "[?name=='$appServicePlanName']" -o tsv)
if($AppservicePlanCheck){
    Write-Host "Appservice plan $appServicePlanName already exists."
} else {
    az appservice plan create --name $appServicePlanName --resource-group $resourceGroupName --sku FREE --location $azureLocation
    Write-Host "Resource Group $appServicePlanName created."
}


Write-Host "Publishing frontend WebApp to Azure"
az webapp up -g $resourceGroupName -n $appName --html --plan $appServicePlanName --location $azureLocation

#Write-Host "Create api management service"
#az apim create --name $apiServiceName --resource-group $resourceGroupName --publisher-name ContosoKsol --publisher-email admin@test.com --no-wait
# te check status use: az apim show --name "az204api123" --resource-group MsLearn --output table

Write-Host "Create backend WebApp"
az webapp create --resource-group $resourceGroupName --plan $appServicePlanName --name $backendAppName --deployment-local-git 

Set-Location ..\..\backend\

Write-Host "Publishing backend WebApp to Azure using Branch AddAzureBackendWebApp"
git push azure AddAzureBackendWebApp:master
# Get FTP publishing profile and query for publish URL and credentials
#$creds=(az webapp deployment list-publishing-profiles --name $backendAppName --resource-group $resourceGroupName --query "[?contains(publishMethod, 'FTP')].[publishUrl,userName,userPWD]" --output tsv)

#$credsplit = $creds -split '\s+'
# Use cURL to perform FTP upload. You can use any FTP tool to do this instead. 
#curl.exe -T index.html -u $credsplit[1]:$credsplit[2] $credsplit[0]
Set-Location $location