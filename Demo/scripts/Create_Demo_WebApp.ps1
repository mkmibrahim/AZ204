$resourceGroupName= 'MsLearn'
$appName= "az204DemoApp123"

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

Set-Location $location