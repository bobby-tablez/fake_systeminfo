# Create a fake systeminfo.exe binary. Useful for spoofing system info to aid in legitamizing systems for honeypots or spam call VMs.

As Microsoft doesn't want you replacing the systeminfo binary in C:\windows\system32\, the built binary will need to be placed in C:\windows\ with the orignal deleted or renamed. Both locations are in env PATH so it should work, unless systeminfo.exe is called using its expected location. 

Before compiling, edit the system information fields as needed.

Run the following commands:
```
TAKEOWN /F C:\Windows\System32\systeminfo.exe
ICACLS C:\Windows\System32\systeminfo.exe /grant everyopne:F
MOVE C:\Windows\System32\systeminfo.exe C:\Windows\System32\systeminfo.exe.bak
```

Gain Access to naitive systeminfo.exe and move built binary into common PATH directory
```
Start-Process -FilePath "C:\Windows\system32\takeown.exe" -NoNewWindow -ArgumentList "/F C:\Windows\System32\systeminfo.exe" -wait
Start-Process -FilePath "C:\Windows\system32\icacls.exe" -NoNewWindow -ArgumentList "C:\Windows\System32\systeminfo.exe /grant everyone:F" -wait
Move-Item -Path "C:\Windows\System32\systeminfo.exe" -Destination "C:\Windows\System32\old_systeminfo.exe"
Move-Item -Path "C:\path\to\build\systeminfo.exe" -Destination "C:\Windows\systeminfo.exe" # will not execute from System32

New-Item -ItemType HardLink -Path "C:\Windows\System32\systeminfo.exe.lnk" -Target "C:\Windows\systeminfo.exe"
```
