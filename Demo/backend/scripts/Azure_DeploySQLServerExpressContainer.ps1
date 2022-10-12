Write-Host "Usage: Azure_DeploySQLServerExpressContainer.ps1 <MSSQL_SA_PASSWORD>"
$SQL_SA_Password=$args[0]
if($SQL_SA_Password){
    write-host "Provided password is " $SQL_SA_Password
} else {
    Write-Host "No password provided. Please provide password."
    Exit 1
}

$resourceGroupName= "MsLearnPersistent"
$azureLocation ="northeurope"
$SQLServerAppname = "az204demosqlserverexpressapp"


Write-Host "Creating Azure resource group " $resourceGroupName
$resourceGroupCheck=(az group list --query "[?name=='$resourceGroupName']" -o tsv)
if($resourceGroupCheck){
    Write-Host "Resource group $resourceGroupName already exists."
} else {
    az group create -l westeurope  -n $resourceGroupName --location $azureLocation
    Write-Host "Resource Group $resourceGroupName created."
}

Write-Host "Creating Azure container instance " $SQLServerAppname
az container create --resource-group $resourceGroupName --name $SQLServerAppname --image mcr.microsoft.com/mssql/server:latest --environment-variables ACCEPT_EULA=Y MSSQL_SA_PASSWORD=$SQL_SA_Password MSSQL_PID=Express --ip-address public --dns-name-label mysqlserverexpresslinux --cpu 1 --memory 2 --port 1433

#docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=yourStrong(!)Password" -e "MSSQL_PID=Express" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest 