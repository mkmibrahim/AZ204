$location = Get-Location

cd..
Write-Host "Creating Docker Image"
docker build -t demo/dockerize-vuejs-app .

Write-Host "Running container "
#docker run -it -p 8080:8080 --rm --name dockerize-vuejs-app demo/dockerize-vuejs-app 
docker run -p 8080:8080 --name dockerize-vuejs-app demo/dockerize-vuejs-app 

Set-Location $location