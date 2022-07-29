$resourceGroupName= 'MsLearn'
$appName= "az204app123"

$location = Get-Location

$Folder = '.\htmlapp'
if (-Not (Test-Path -Path $Folder)) {
    mkdir $Folder
} 

Set-Location htmlapp

git clone https://github.com/Azure-Samples/html-docs-hello-world.git

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

Set-Location html-docs-hello-world

az webapp up -g $resourceGroupName -n $appName --html

Set-Location $location