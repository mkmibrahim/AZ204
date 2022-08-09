$resourceGroupName= 'MsLearn'
$appName= "az204staticwebapp123"
$githubRepository = "https://github.com/mkmibrahim/AZ204"
$githubbranch = "main"
$githubAppLocation = "Demo/vueapp/"
$azureappLocation ="westeurope"

az staticwebapp create -n $appName `
                        -g $resourceGroupName `
                        -s $githubRepository `
                        -l $azureappLocation `
                        -b $githubbranch `
                        --app-location $githubAppLocation `
                        --login-with-github
