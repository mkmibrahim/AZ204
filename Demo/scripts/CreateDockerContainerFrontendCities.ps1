$location = Get-Location

cd ..\Frontend-Cities
Write-Host "Creating Docker Image"
docker build -t demo_dockerize-frontendcities-app .

Write-Host "Running container"
docker run -d -p 8092:80 --add-host host.docker.internal:host-gateway --name dockerize_backend_frontendcities_app demo_dockerize-frontendcities-app 

Set-Location $location