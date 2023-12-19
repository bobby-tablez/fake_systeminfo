# Fake systeminfo.exe

## Create a fake systeminfo.exe binary. Useful for spoofing system information to aid in legitamizing systems for honeypots or spam call VMs.

The systeminfo.exe displays detailed info related to your system. When trying to pass a virtual machine as a legitimate "silicone" system can be a challenge as systeminfo.exe will display telling details such as BIOS, network card, or manufacturer information that may relate to the hypervisor or host system such as Vmware, or Virtualbox. 

Systeminfo is often used by threat actors in a honeypot, or tech support scammers to verify the legitimacy of the system. This project builds a fake "systeminfo.exe" which replaces all of the telling fields with fields that would appear on a legitimate silicone system. To legitimize some of the non-telling fields, the binary will function similiar to the original systeminfo.exe and will pull and display accurate information such as OS details, username, Hotfixes, etc. 

There are many other ways to determine the system type such as registery values or using WMI queries, however this addresses one common method of determining the system type. 

### Replaced systeminfo.exe fields (spoofed):
* System Minufacturer: (set to Dell, Inc.)
* Processors(s): (Set to "1 Processor(s) Installed"
		"[01]: 11th Gen Intel Core i7-11700 4.90 GHz Turbo\n")
* BIOS Version: (Set to: "DELL -1072009, 1.4.4, American Megatrends-5000B")
* Original Install Date (Modified to original +1 year)
* System Boot Time (Modified to +3-7 hours, random minutes)
* Network Cards (this one was tricky, modified only the NIC "name" field. Rest of the info is scraped)

### Generated systeminfo.exe fields (scraped/legit):
* Hostname
* OS Name
* OS Version
* OS Manufacturer
* Registered Owner
* Registered Organization
* Product ID
* System Locale
* Input Locale
* Time Zone
* Total Physical Memory
* Available Physical Memory
* Virtual Memory: Max Size
* Virtual Memory: Available
* Virtual Memory: In Use
* Page File Location(s)
* Domain
* Logon Server
* Hotfix(s)

### Fields left alone:
* OS Configuration
* OS Build Type
* Windows Directory: (set to C:\Windows)
* System Directory: (set to C:\Windows\System32)
* Boot Device: (set to \Device\HarddiskVolume1)
* Hyper-V Requirements

As Microsoft doesn't want you replacing the systeminfo binary in C:\windows\system32\, you'll likely run into permission errors. These can be overcome issueing the following commands:

```batch
TAKEOWN /F "C:\Windows\System32\systeminfo.exe"
ICACLS "C:\Windows\System32\systeminfo.exe" /grant everyopne:F
MOVE "C:\Windows\System32\systeminfo.exe" "C:\Windows\System32\__systeminfo.exe.bak"
```
Next drop your compiled systeminfo.exe binary into System32. 

Alternatively use the included powershell script "[replace_systeminfo.ps1](https://github.com/bobby-tablez/fake_systeminfo/blob/main/replace_systeminfo.ps1)" which wil

* Modify the systeminfo.exe ownership and permissions
* Rename the original systeminfo.exe to sysinfo.exe
* Prompt the user for the location of a replacement systeminfo.exe
* Copy it into C:\Windows\System32

**DISCLAIMER**: Probably not advisable modify system executables on hosts you care about. Use at your own risk!
