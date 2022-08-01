$location = Get-Location
$appName= "az204app123"
$resourceGroupName= "MsLearn"
#$RandomNumber = Get-Random -Minimum 10 -Maximum 100
Write-Host "Creating deployment slot with name staging"
az webapp deployment slot create -n $appName -g $resourceGroupName --slot staging

$Folder = '.\htmlapp'

Set-Location $Folder

#Create a ZIP archive
Write-Host "Creating staging zip source file"
Compress-Archive -Path html-docs-hello-world/* -DestinationPath webapp.zip -Force

# Deploy sample code to "staging" slot from GitHub.
Write-Host "Deploying zip file to staging slot"
az webapp deployment source config-zip --name az204app123 --resource-group MsLearn --src webapp.zip --slot staging 

Set-Location $location