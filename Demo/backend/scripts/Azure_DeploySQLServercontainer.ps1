Write-Host "Usage: Azure_DeploySQLServercontainer.ps1 <MSSQL_SA_PASSWORD>"

$SQL_SA_Password=$args[0]
write-host "Provided password is " $SQL_SA_Password

$resourceGroupName= "MsLearnPersistent"
$azureLocation ="westeurope"
$SQLServerAppname = "az204demosqlserverapp"


Write-Host "Creating Azure resource group " $resourceGroupName
$resourceGroupCheck=(az group list --query "[?name=='$resourceGroupName']" -o tsv)
if($resourceGroupCheck){
    Write-Host "Resource group $resourceGroupName already exists."
} else {
    az group create -l westeurope  -n $resourceGroupName --location $azureLocation
    Write-Host "Resource Group $resourceGroupName created."
}

az container create --resource-group $resourceGroupName --name $SQLServerAppname --image mcr.microsoft.com/mssql/server:2019-CU12-ubuntu-20.04 --environment-variables ACCEPT_EULA=Y MSSQL_SA_PASSWORD=$SQL_SA_Password --ip-address public --dns-name-label mysqlserverlinux --cpu 2 --memory 2 --port 1433