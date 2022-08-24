$resourceGroupName= 'MsLearn'
$azureLocation ="westeurope"
$appName= "az204DemoApp123"
$appServicePlanName= "az204DemoAppServicePlan123"
$backendAppName = "az204DemoBackendApp123"
#$apiServiceName ="az204api123"

$location = Get-Location
Set-Location ..\backend\

Write-Host "Publishing backend WebApp to Azure"
#git push azure AddAzureBackendWebApp:master
# Get FTP publishing profile and query for publish URL and credentials
$creds=(az webapp deployment list-publishing-profiles --name $backendAppName --resource-group $resourceGroupName --query "[?contains(publishMethod, 'FTP')].[publishUrl,userName,userPWD]" --output tsv)

$credsplit = $creds -split '\s+'
#Write-Host $creds
Write-Host $credsplit[0]
Write-Host $credsplit[1]
Write-Host $credsplit[2]
# Use cURL to perform FTP upload. You can use any FTP tool to do this instead. 
curl.exe -T index.html -u $credsplit[1]:$credsplit[2] $credsplit[0]
Set-Location $location