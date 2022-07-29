# Create a fake systeminfo.exe binary. Useful for spoofing system info to aid in legitamizing systems for honeypots or spam call VMs.

As Microsoft doesn't want you replacing the systeminfo binary in C:\windows\system32\, the built binary will need to be placed in C:\windows\ with the orignal deleted or renamed. Both locations are in env PATH so it should work, unless systeminfo.exe is called using its expected location. 

Before compiling, edit the system information fields as needed.

Run the following commands:
```
TAKEOWN /F C:\Windows\System32\systeminfo.exe
ICACLS C:\Windows\System32\systeminfo.exe /grant everyopne:F
MOVE C:\Windows\System32\systeminfo.exe C:\Windows\System32\systeminfo.exe.bak
```

Move fake system info binary:
```
COPY systeminfo.exe C:\Windows\
```
