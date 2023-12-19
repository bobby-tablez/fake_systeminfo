#Requires -RunAsAdministrator
$origSysteminfo = "C:\Windows\System32\systeminfo.exe"
$backupSysinfo = "C:\Windows\System32\sysinfo.exe"

try {
    
    Invoke-Expression -Command "takeown /f $origSysteminfo" | Out-Null
    Invoke-Expression -Command "icacls $origSysteminfo /grant EVERYONE:F" | Out-Null

} catch {
    $errorOwn = $_.Exception.Message
    Write-Host "Error occurred taking ownership: $errorOwn"
    exit 1

}
Write-Host -f yellow "Took ownershipt of original sysinfo.exe"

try {
    Move-Item -Path $origSysteminfo -Destination $backupSysinfo -Force

} catch {
    $errorRename = $_.Exception.Message
    Write-Host "Error occurred renaming the file: $errorRename"
    exit 1

}
Write-Host -f yellow "Renamed systeminfo.exe to sysinfo.exe (for backup purposes)"

$fakeFile = Read-Host -Prompt 'Enter the full path of your systeminfo.exe'
try {
    Copy-Item -Path $fakeFile -Destination "C:\Windows\System32\systeminfo.exe"

} catch {
    $errorCopy = $_.Exception.Message
    Write-Host "Error occurred renaming the file: $errorCopy"
    exit 1

}
Write-Host -f yellow "Copied fake systeminfo.exe into C:\Windows\System32"

Write-Host -f green "`nDone!"
