if($null -eq $args[0] -Or $null -eq $args[1]) {
  Write-Host "Usage: CreateVM.bar <username> <userpassword>"
  Return
}

$azureLocation ="westeurope"
$resourceGroupName= "VM_ResourceGroup"
$VMName = "VMVSAzure"


Write-Host "Creating Azure resource group " $resourceGroupName
$resourceGroupCheck=(az group list --query "[?name=='$resourceGroupName']" -o tsv)
if($resourceGroupCheck){
    Write-Host "Resource group $resourceGroupName already exists."
} else {
    az group create -n $resourceGroupName --location $azureLocation
    Write-Host "Resource Group $resourceGroupName created."
}


Write-Host "Creating virtual machine " $VMName
$VMNameCheck=(az vm list --query "[?name=='$VMName']" -o tsv)
if($VMNameCheck){
    Write-Host "Virtual machine $VMName already exists."
} else {
    az vm create -g $resourceGroupName -n $VMName -l $azureLocation --admin-username $args[0] --admin-password $args[1] --image MicrosoftWindowsDesktop:Windows-10:win10-21h2-entn-g2:19044.2006.220909 --public-ip-sku Standard
    Write-Host "Virtual machine $VMName created."
}

