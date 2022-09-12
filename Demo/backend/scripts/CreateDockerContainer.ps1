$location = Get-Location

cd ..\backend
Write-Host "Creating Docker Image"
docker build -t demo_dockerize_backend_app .

Write-Host "Running container "
docker run -d -p 8080:80 --add-host host.docker.internal:host-gateway --name dockerize_backend_app demo_dockerize_backend_app 

Set-Location $location