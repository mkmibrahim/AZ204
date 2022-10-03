$location = Get-Location

cd ..\CityWeather
Write-Host "Creating Docker Image"
docker build -t demo_dockerize_cityweather_app .

Write-Host "Running container"
docker run -d -p 8091:80 --add-host host.docker.internal:host-gateway --name dockerize_backend_cityimages_app demo_dockerize_cityweather_app 

Set-Location $location