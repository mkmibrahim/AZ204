$location = Get-Location

cd ..\CityImages
Write-Host "Creating Docker Image"
docker build -t demo_dockerize_cityimages_app .

Write-Host "Running container "
docker run -d -p 8090:80 --add-host host.docker.internal:host-gateway --name dockerize_backend_cityimages_app demo_dockerize_cityimages_app 

Set-Location $location