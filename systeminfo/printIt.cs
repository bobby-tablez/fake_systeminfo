using System;
using System.Net;
using System.Management;
using System.Threading;
using System.Globalization;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace systeminfo
{
    public class NetworkCard
    {
        public string Name { get; set; }
        public string ConnectionName { get; set; }
        public bool DhcpEnabled { get; set; }
        public string[] IpAddresses { get; set; }
        public string Status { get; set; }
    }
    internal class printIt
    {
        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        static void Main(string[] args)
        {
            // Begin spoofing the "information gathering" portion of systeminfo.exe
            Console.WriteLine("Loading Operating System Information ...");
            Thread.Sleep(600);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();

            Console.WriteLine("Loading Processor Information ...");
            Thread.Sleep(1880);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();

            Console.WriteLine("Loading Operating System Information ...");
            Thread.Sleep(560);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();

            Console.WriteLine("Loading Input Locale Information ...");
            Thread.Sleep(140);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();

            Console.WriteLine("Loading Hotfix Information ...");
            Thread.Sleep(1470);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();

            Console.WriteLine("Loading Network Card Information ...");
            Thread.Sleep(250);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();

            Console.WriteLine("Loading Hyper-V Information ...");
            Thread.Sleep(280);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            

            // Define all the dynamic strings
            string hostname = GetMachineHostName();
            string osVersionInfo = GetWindowsOSVersionInfo();
            string osInfo = GetWindowsOSInfo();
            string manufacturer = GetManufacturer();
            string osConfig = GetOSConfiguration();
            string registeredOwner = GetRegisteredOwner();
            string registeredOrganization = GetRegisteredOrganization();
            string productID = GetProductID();
            string systemModel = GetRandomDellSystemModel();
            string systemLocale = GetSystemLocale();
            string inputLocale = GetInputLocale();
            string timeZone = GetCurrentTimeZone();
            string totalMemory = GetTotalPhysicalMemory();
            string availableMemory = GetAvailablePhysicalMemory();
            string virtualMemoryMaxSize = GetVirtualMemoryMaxSize();
            string virtualMemoryAvailable = GetAvailableVirtualMemory();
            string virtualMemoryInUse = GetVirtualMemoryInUse();
            string pageFilePath = GetPageFilePath();
            string domainName = GetDomainName();
            string logonServer = GetLogonServer();

            DateTime adjustedInstallDate = GetAdjustedInstallDate();
            DateTime adjustedBootTime = GetAdjustedCurrentTime();

            List<string> hotfixes = GetInstalledHotfixes();
            List<NetworkCard> networkCards = GetNetworkCardsInfo();

            ClearCurrentConsoleLine(); // Needs to be below strings to account for slow functions loading NIC, hotfix data, etc.

            // Print the "table"
            Console.WriteLine(
                "Host Name:".PadRight(27) + hostname + "\n" +
                "OS Name:".PadRight(27) + osInfo + "\n" +
                "OS Version:".PadRight(27) + osVersionInfo + "\n" +
                "OS Manufacturer:".PadRight(27) + manufacturer + "\n" +
                "OS Configuration:".PadRight(27) + "Member Workstation\n" +
                "OS Build Type:".PadRight(27) + "Multiprocessor Free\n" +
                "Registered Owner:".PadRight(27) + registeredOwner + "\n" +
                "Registered Organization:".PadRight(27) + registeredOrganization + "\n" +
                "Product ID:".PadRight(27) + productID + "\n" +
                "Original Install Date:".PadRight(27) + adjustedInstallDate + "\n" +
                "System Boot Time:".PadRight(27) + adjustedBootTime + "\n" +
                "System Manufacturer:".PadRight(27) + "Dell, Inc.\n" +
                "System Model:".PadRight(27) + systemModel + "\n" +
                "System Type:".PadRight(27) + "x64 - based PC\n" +
                "Processor(s):".PadRight(27) + "1 Processor(s) Installed.\n" +
                "".PadRight(27) + "[01]: 11th Gen Intel Core i7-11700 4.90 GHz Turbo\n" + // Legit looking CPU, (no Xeons)
                "BIOS Version:".PadRight(27) + "DELL -1072009, 1.4.4, American Megatrends-5000B\n" + // Some legit looking bios
                "Windows Directory:".PadRight(27) + "C:\\Windows\n" +
                "System Directory:".PadRight(27) + "C:\\Windows\\system32\n" +
                "Boot Device:".PadRight(27) + "\\Device\\HarddiskVolume1\n" +
                "System Locale:".PadRight(27) + systemLocale + "\n" +
                "Input Locale:".PadRight(27) + inputLocale + "\n" +
                "Time Zone:".PadRight(27) + timeZone + "\n" +
                "Total Physical Memory:".PadRight(27) + totalMemory + " MB" + "\n" +
                "Available Physical Memory:".PadRight(27) + availableMemory + " MB" + "\n" +
                "Virtual Memory: Max Size:".PadRight(27) + virtualMemoryMaxSize + " MB" + "\n" +
                "Virtual Memory: Available:".PadRight(27) + virtualMemoryAvailable + " MB" + "\n" +
                "Virtual Memory: In Use:".PadRight(27) + virtualMemoryInUse + " MB" + "\n" +
                "Page File Location(s):".PadRight(27) + pageFilePath + "\n" +
                "Domain:".PadRight(27) + domainName + "\n" +
                "Logon Server:".PadRight(27) + logonServer);
            
            PrintHotfixesFormatted(hotfixes);
            PrintNetworkCardsFormatted(networkCards);

            // We'll just print this, shouldn't raise questions
            Console.WriteLine(
                "Hyper-V Requirements:".PadRight(27) + "VM Monitor Mode Extensions: Yes\n" +
                "".PadRight(27) + "Virtualization Enabled In Firmware: Yes\n" +
                "".PadRight(27) + "Second Level Address Translation: Yes\n" +
                "".PadRight(27) + "Data Execution Prevention Available: Yes\n");
        }

        // Get hostname
        static string GetMachineHostName()
        {
            return Dns.GetHostName();
        }

        // Get OS info
        static string GetWindowsOSInfo()
        {
            try
            {
                string query = "SELECT Caption FROM Win32_OperatingSystem";
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

                foreach (ManagementObject mo in searcher.Get())
                {
                    return mo["Caption"].ToString();
                }

                return "OS Information Not Found";
            }
            catch (Exception ex)
            {
                return $"Error retrieving OS information: {ex.Message}";
            }
        }

        //Get OS version details
        static string GetWindowsOSVersionInfo()
        {
            Version osVersion = Environment.OSVersion.Version;
            string versionInfo = $"{osVersion.Major}.{osVersion.Minor}.{osVersion.Build} N/A Build {osVersion.Build}";
            return versionInfo;
        }

        // Get system manufacturer
        static string GetManufacturer()
        {
            string query = "SELECT Caption, Manufacturer FROM Win32_OperatingSystem";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject mo in searcher.Get())
            {
                string manufacturer = mo["Manufacturer"].ToString();

                return $"{manufacturer}";
            }

            return "Manufacturer Not Found";
        }

        // Get workstation configuration
        static string GetOSConfiguration()
        {

            string query = "SELECT ProductType FROM Win32_OperatingSystem";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject mo in searcher.Get())
            {
                int productType = Convert.ToInt32(mo["ProductType"]);

                switch (productType)
                {
                    case 1:
                        return "Standalone Workstation";
                    case 2:
                        return "Member Workstation";
                    case 3:
                        return "Primary Domain Controller";
                    case 4:
                        return "Backup Domain Controller";
                    default:
                        return "Unknown Configuration";
                }
            }

            return "OS Configuration Not Found";
        }

        // Get registered owner information
        static string GetRegisteredOwner()
        {
            string query = "SELECT RegisteredUser FROM Win32_OperatingSystem";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject mo in searcher.Get())
            {
                return mo["RegisteredUser"].ToString();
            }

            return "Bobby Tables";

        }

        // Get registered domain org info
        static string GetRegisteredOrganization()
        {

            try
            {
                string query = "SELECT * FROM Win32_OperatingSystem";
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

                foreach (ManagementObject mo in searcher.Get())
                {
                    if (mo["RegisteredOrganization"] != null)
                    {
                        return mo["RegisteredOrganization"].ToString();
                    }
                    else
                    {
                        return "Registered Organization Not Set";
                    }
                }

                return "";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        // Get product ID
        static string GetProductID()
        {
            string query = "SELECT SerialNumber FROM Win32_OperatingSystem";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject mo in searcher.Get())
            {
                if (mo["SerialNumber"] != null)
                {
                    return mo["SerialNumber"].ToString();
                }
                else
                {
                    return "";
                }
            }
            return "";
        }

        // Get the Install date and subtract a year to make it seem more legit
        static DateTime GetAdjustedInstallDate()
        {
            try
            {
                string query = "SELECT InstallDate FROM Win32_OperatingSystem";
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

                foreach (ManagementObject mo in searcher.Get())
                {
                    if (mo["InstallDate"] != null)
                    {
                        string installDateStr = mo["InstallDate"].ToString();
                        DateTime installDate = ManagementDateTimeConverter.ToDateTime(installDateStr);

                        return installDate.AddYears(-1);
                    }
                    else
                    {
                        throw new Exception("Install Date Not Set");
                    }
                }

                throw new Exception("Install Date Not Found");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving Adjusted Install Date: {ex.Message}");
                return DateTime.MinValue; 
            }
        }

        // Used to spoof boot time. Take current system time and subtract between 3-7 hours and a random amount of minutes
        static DateTime GetAdjustedCurrentTime()
        {
            Random rnd = new Random();

            int hours = rnd.Next(3, 8);
            int minutes = rnd.Next(0, 60);

            DateTime currentTime = DateTime.Now;

            return currentTime.AddHours(-hours).AddMinutes(-minutes);
        }

        // Get random Dell laptops:
        static string GetRandomDellSystemModel()
        {
            string[] dellModels = new string[]
            {
            "Precision 3450 SFF BTX BASE,1",
            "XPS 13 9310,1",
            "Latitude 5420,1",
            "Inspiron 15 3000,1",
            "Alienware m15 R4,1",
            "Vostro 15 3500,1",
            "G5 15 Gaming Laptop,1",
            "Precision 3551,1",
            "Latitude 7320,1",
            "XPS 15 9500,1"
            };

            Random rnd = new Random();

            int index = rnd.Next(dellModels.Length);

            return dellModels[index];
        }

        // Get the system locale
        static string GetSystemLocale()
        {
            CultureInfo cultureInfo = CultureInfo.InstalledUICulture;
            string locale = $"{cultureInfo.Name}; {cultureInfo.EnglishName}";

            return locale;
        }

        // Get the keyboard locale
        static string GetInputLocale()
        {
            InputLanguage inputLang = InputLanguage.CurrentInputLanguage;
            CultureInfo cultureInfo = inputLang.Culture;
            string locale = $"{cultureInfo.Name}; {cultureInfo.EnglishName}";

            return locale;
        }

        // Get the time zone
        static string GetCurrentTimeZone()
        {
            TimeZoneInfo timeZoneInfo = TimeZoneInfo.Local;

            return timeZoneInfo.DisplayName;
        }

        // Get physical memory
        static string GetTotalPhysicalMemory()
        {
            string query = "SELECT TotalPhysicalMemory FROM Win32_ComputerSystem";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject mo in searcher.Get())
            {
                if (mo["TotalPhysicalMemory"] != null)
                {
                    // convert to MB
                    long memoryBytes = Convert.ToInt64(mo["TotalPhysicalMemory"]);
                    long memoryMB = memoryBytes / 1024 / 1024;

                    return memoryMB.ToString("N0"); // Format with thousand separators and no decimal places
                }
                else
                {
                    return "8,191"; // give something in case of error
                }
            }

            return "8,191"; // give something in case of error
        }

        // Available physical memory
        static string GetAvailablePhysicalMemory()
        {

            string query = "SELECT FreePhysicalMemory FROM Win32_OperatingSystem";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject mo in searcher.Get())
            {
                if (mo["FreePhysicalMemory"] != null)
                {
                    long memoryKB = Convert.ToInt64(mo["FreePhysicalMemory"]);
                    long memoryMB = memoryKB / 1024;

                    return memoryMB.ToString("N0");
                }
                else
                {
                    return "4,089";
                }
            }

            return "4,089";
           
        }

        // Available memory "max size"
        static string GetVirtualMemoryMaxSize()
        {
            string query = "SELECT TotalVirtualMemorySize FROM Win32_OperatingSystem";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject mo in searcher.Get())
            {
                if (mo["TotalVirtualMemorySize"] != null)
                {
                    long memoryKB = Convert.ToInt64(mo["TotalVirtualMemorySize"]);
                    long memoryMB = memoryKB / 1024;

                    return memoryMB.ToString("N0");
                }
                else
                {
                    return "9,471"; // Another arbitrary number if it fails...
                }
            }
            return "9,471"; // ...

        }

        // Available virtual memory
        static string GetAvailableVirtualMemory()
        {
            string query = "SELECT FreeVirtualMemory FROM Win32_OperatingSystem";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject mo in searcher.Get())
            {
                if (mo["FreeVirtualMemory"] != null)
                {
                    long memoryKB = Convert.ToInt64(mo["FreeVirtualMemory"]);
                    long memoryMB = memoryKB / 1024;

                    return memoryMB.ToString("N0");
                }
                else
                {
                    return "4,915"; // Another arbitrary number if it fails...
                }
            }

            return "4,915";// ...

        }

        // Get virtual memory used
        static string GetVirtualMemoryInUse()
        {
            string query = "SELECT TotalVirtualMemorySize, FreeVirtualMemory FROM Win32_OperatingSystem";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject mo in searcher.Get())
            {
                if (mo["TotalVirtualMemorySize"] != null && mo["FreeVirtualMemory"] != null)
                {
                    long totalMemoryKB = Convert.ToInt64(mo["TotalVirtualMemorySize"]);
                    long freeMemoryKB = Convert.ToInt64(mo["FreeVirtualMemory"]);
                    long usedMemoryKB = totalMemoryKB - freeMemoryKB;
                    long usedMemoryMB = usedMemoryKB / 1024;

                    return usedMemoryMB.ToString("N0");
                }
                else
                {
                    return "4,556";
                }
            }

            return "4,556";

        }

        // Get the page file
        static string GetPageFilePath()
        {
            string query = "SELECT Name FROM Win32_PageFileUsage";
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject mo in searcher.Get())
            {
                if (mo["Name"] != null)
                {
                    return mo["Name"].ToString();
                }
                else
                {
                    return "C:\\pagefile.sys"; // Give it something if it fails
                }
            }

            return "C:\\pagefile.sys"; // ...

        }

        // Get system domain name
        static string GetDomainName()
        {
                string query = "SELECT Domain FROM Win32_ComputerSystem";
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

                foreach (ManagementObject mo in searcher.Get())
                {
                    if (mo["Domain"] != null)
                    {
                        return mo["Domain"].ToString();
                    }
                    else
                    {
                        return "WORKGROUP";
                    }
                }
                return "WORKGROUP";

        }

        // Get the logon server
        static string GetLogonServer()
        {
            string logonServer = Environment.GetEnvironmentVariable("LOGONSERVER");

            if (!string.IsNullOrEmpty(logonServer))
            {
                return logonServer;
            }
            else
            {
                return "\\\\localhost";
            }
        }


        // Get hotfixes
        static List<string> GetInstalledHotfixes()
        {
            List<string> hotfixList = new List<string>();

            try
            {
                string query = "SELECT HotFixID FROM Win32_QuickFixEngineering";
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

                foreach (ManagementObject mo in searcher.Get())
                {
                    if (mo["HotFixID"] != null)
                    {
                        hotfixList.Add(mo["HotFixID"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving hotfixes: {ex.Message}");
            }

            return hotfixList;
        }

        static void PrintHotfixesFormatted(List<string> hotfixes)
        {
            Console.WriteLine($"Hotfix(s): {hotfixes.Count.ToString().PadLeft(18)} Hotfix(s) Installed.");

            int index = 1;
            foreach (string hotfix in hotfixes)
            {
                string indexString = $"[{index.ToString("D2")}]:";
                Console.WriteLine($"{indexString.PadLeft(32)} {hotfix}");
                index++;
            }
        }

        // Network cards...
        static List<NetworkCard> GetNetworkCardsInfo()
        {
            List<NetworkCard> nic = new List<NetworkCard>();

            ManagementObjectSearcher adapterSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter WHERE NetConnectionID IS NOT NULL");
            ManagementObjectCollection adapterObjects = adapterSearcher.Get();

            ManagementObjectSearcher configSearcher = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = TRUE");
            ManagementObjectCollection configObjects = configSearcher.Get();

            foreach (ManagementObject adapter in adapterObjects)
            {
                NetworkCard card = new NetworkCard
                {
                    Name = adapter["Name"]?.ToString(),
                    ConnectionName = adapter["NetConnectionID"]?.ToString(),
                    Status = adapter["Status"]?.ToString()
                };

                // Find corresponding NIC configuration
                var config = configObjects.Cast<ManagementObject>().FirstOrDefault(c => c["Index"].ToString() == adapter["Index"].ToString());
                if (config != null)
                {
                    card.DhcpEnabled = (bool)config["DHCPEnabled"];
                    card.IpAddresses = (string[])config["IPAddress"] ?? new string[0];
                }

                nic.Add(card);
            }

            return nic;
        }

        // Format network info
        static void PrintNetworkCardsFormatted(List<NetworkCard> cards)
        {
            string[] networkInterfaces = new string[]
            {
                "Intel(R) I211 Gigabit Network Connection",
                "Realtek PCIe GbE Family Controller",
                "Qualcomm Atheros AR956x Wireless Network Adapter",
                "Broadcom 802.11ac Network Adapter",
                "Microsoft Wi-Fi Direct Virtual Adapter",
                "Cisco AnyConnect Secure Mobility Client Virtual Miniport Adapter for Windows x64",
                "Intel(R) Ethernet Controller I226-V",
                "Realtek RTL8188EU Wireless LAN 802.11n USB 2.0 Network Adapter",
                "Intel(R) Ethernet Connection (2) I219-V",
                "TP-Link 10/100/1000Mbps Gigabit Ethernet Adapter"
            };

            Random rand = new Random();

            Console.WriteLine($"Network Card(s): {cards.Count.ToString().PadLeft(11)} NIC(s) Installed.");

            int cardIndex = 1;

            foreach (var card in cards)
            {      
                int randomIndex = rand.Next(networkInterfaces.Length); // randomize each NIC from networkinterfaces

                Console.WriteLine($"{($"[{cardIndex}]: ").PadLeft(32)}"+(networkInterfaces[randomIndex]));
                Console.WriteLine($"{"Connection Name:".PadLeft(48)} {card.ConnectionName}");
                Console.WriteLine($"{"DHCP Enabled:"} {(card.DhcpEnabled ? "Yes" : "No ")}".PadLeft(49));
                Console.WriteLine($"{"Status:"} {card.Status}".PadLeft(40));

                if (card.IpAddresses != null && card.IpAddresses.Length > 0)
                {
                    Console.WriteLine($"{"IP address(es)".PadLeft(46)}");
                    int ipIndex = 1;
                    foreach (var ip in card.IpAddresses)
                    {
                        Console.WriteLine($"{($"[{ipIndex}]").PadLeft(35)}: {ip}");
                        ipIndex++;
                    }
                }

                cardIndex++;
            }
        }
    }
}
