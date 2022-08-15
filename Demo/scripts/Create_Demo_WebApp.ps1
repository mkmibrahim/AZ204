$resourceGroupName= 'MsLearn'
$appName= "az204DemoApp123"
$appServicePlanName= "az204DemoAppServicePlan123"
$backendAppName = "az204DemoBackendApp123"

$location = Get-Location

Set-Location ..\vueapp

Write-Host "Creating production build"
npm run build

Set-Location dist

Write-Host "Checking Azure resource group"
$resourceGroup=(az group list --query "[?name=='MsLearn']" -o tsv)
#$resourceGroup=(az group list --query "[?name=='$resourceGroupName']" -o tsv)
if($resourceGroup){
    "Resource group $resourceGroupName already exists."
    #Write-Host "The variable is not null."
} else {
    az group create -l westeurope  -n $resourceGroupName
    "Resource Group $resourceGroupName created."
    #Write-Host "The variable is null."
}

az webapp up -g $resourceGroupName -n $appName --html



az appservice plan create --name $appServicePlanName --resource-group $resourceGroupName --sku FREE

az webapp create --resource-group $resourceGroupName --plan $appServicePlanName --name $backendAppName --deployment-local-git

Set-Location ..\backend\

//git push azure main
git push azure main:master
Set-Location $location