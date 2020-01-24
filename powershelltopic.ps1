$endpoint =  (Get-AzureRmEventGridtopic -Name cartopic -ResourceGroupName acrmclassjan).Endpoint

$keys =  (Get-AzureRmEventGridTopicKey -Name cartopic -ResourceGroupName acrmclassjan).Key1
$msg = @{
    id = "001"
    eventType = "CarTyreFlat"
    eventTime = [DateTime]::Now
    Subject = "School Car Event"
    data = @{
        carnumber = "AP 28 ts 007"
        Drivername = "ABCD"
    }
}
$jsonBody = "[" + $(ConvertTo-Json -InputObject $msg) + "]"

Invoke-WebRequest -Uri $endpoint -Method Post -Body $jsonBody -Headers @{ "aeg-sas-key" = $keys }
