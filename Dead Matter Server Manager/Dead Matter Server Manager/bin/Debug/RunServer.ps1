#Path to DeadMatter Dedicated Server Installation
$DMDediPath = "E:\DMServer"
$MaxMem = 20

#!!!!!---------DON'T TOUCH ANYTHING BELOW THIS LINE---------!!!!!

cls
#main loop
while($true)
{
$i++
$date = get-date
$text = "      Dead Matter Dedicated Server launched at $date     [¬º-°]¬"
$text2 = "      Dead Matter Dedicated Server launched at $date     [¬x-X]¬"
$Array = $text2.ToCharArray()
#Launching DM Server
Start-Process -FilePath "$DMDediPath\deadmatterServer.exe" -ArgumentList "-USEALLAVAILABLECORES -log"
start-sleep -s 2
$p = "deadmatterserver-win64-shipping"
while (Get-Process $P -ErrorAction SilentlyContinue){
Do
{
#Calculating and displaying DM Server Pageable Memory Use in GB
write-host -NoNewline "`r Dead Matter Dedicated Server is currently using:" $([math]::Round($(($DMRamUSE = Get-Process $P -ErrorAction SilentlyContinue | select -ExpandProperty PM)/1Gb),2))"GB of Memory...  "
$proc = Get-Process $p -ErrorAction SilentlyContinue
start-sleep -s 2
} While ((Get-Process $P -ErrorAction SilentlyContinue) -and ($proc.PM/1Gb) -lt $MaxMem)
#Killing DM Server if Pageable Memory use exceeds $MaxMem
kill -processname $p -ErrorAction SilentlyContinue}
write-host "`n`n Dead Matter Server exceeded set Max Memory Use or the server was shut down, restarting...`n" -ForegroundColor Red
start-sleep -s 2
Select-String -Path "$DMDediPath\deadmatter\Saved\Logs\deadmatter.log" -Pattern 'LogCore'-AllMatches | Foreach {$_.Line}
write-host "`n`n Server closed at: $(Get-Date)`n" -ForeGroundColor White
start-sleep -s 5
}