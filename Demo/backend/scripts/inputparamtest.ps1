$param1=$args[0]
#write-host $param1 
if($param1){
    Write-Host "Parameter provided is $param1"
} else {
    Write-Host "No parameter provided."
}