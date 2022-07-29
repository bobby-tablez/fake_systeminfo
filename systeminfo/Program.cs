using System;
using System.Threading;

namespace systeminfo
{
    internal class Program
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
            Console.WriteLine("Loading Operating System Information ...");
            Thread.Sleep(200);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();

            Console.WriteLine("Loading Processor Information ...");
            Thread.Sleep(480);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();

            Console.WriteLine("Loading Operating System Information ...");
            Thread.Sleep(160);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();

            Console.WriteLine("Loading Input Locale Information ...");
            Thread.Sleep(40);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();

            Console.WriteLine("Loading Hotfix Information ...");
            Thread.Sleep(70);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();

            Console.WriteLine("Loading Network Card Information ...");
            Thread.Sleep(50);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();

            Console.WriteLine("Loading Hyper-V Information ...");
            Thread.Sleep(180);
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ClearCurrentConsoleLine();

            Console.WriteLine(
                "Host Name:                 DS0-BT01SVS\n" +
                "OS Name:                   Microsoft Windows 10 Enterprise\n" +
                "OS Version:                10.0.19044 N/A Build 19044\n" +
                "OS Manufacturer:           Microsoft Corporation\n" +
                "OS Configuration:          Member Workstation\n" +
                "OS Build Type:             Multiprocessor Free\n" +
                "Registered Owner:          NigelFran\n" +
                "Registered Organization:   FakeORG.org\n" +
                "Product ID:                00323-00000-00002-AA078\n" +
                "Original Install Date:     12/4/2021, 10:10:38 PM\n" +
                "System Boot Time:          7/15/2022, 6:54:51 AM\n" +
                "System Manufacturer:       Dell, Inc.\n" +
                "System Model:              Precision 3450 SFF BTX BASE,1\n" +
                "System Type:               x64 - based PC\n" +
                "Processor(s):              1 Processor(s) Installed.\n" +
                "                           [01]: 11th Gen Intel® Core™ i7-11700 4.90 GHz Turbo\n" +
                "BIOS Version:              DELL-1072009, 1.4.4, American Megatrends-5000B\n" +
                "Windows Directory:         C:\\Windows\n" +
                "System Directory:          C:\\Windows\\system32\n" +
                "Boot Device:               \\Device\\HarddiskVolume1\n" +
                "System Locale:             en-us; English(United States)\n" +
                "Input Locale:              en-us; English(United States)\n" +
                "Time Zone:                 (UTC-08:00) Pacific Time(US &Canada)\n" +
                "Total Physical Memory:     8,191 MB\n" +
                "Available Physical Memory: 4,089 MB\n" +
                "Virtual Memory: Max Size:  9,471 MB\n" +
                "Virtual Memory: Available: 4,915 MB\n" +
                "Virtual Memory: In Use:    4,556 MB\n" +
                "Page File Location(s):     C:\\pagefile.sys\n" +
                "Domain:                    DOMAIN.LOCAL\n" +
                "Logon Server:              N/A\n" +
                "Hotfix(s):                 8 Hotfix(s) Installed.\n" +
                "                           [01]: KB5013887\n" +
                "                           [02]: KB5003791\n" +
                "                           [03]: KB5015807\n" +
                "                           [04]: KB5011651\n" +
                "                           [05]: KB5014032\n" +
                "                           [06]: KB5014035\n" +
                "                           [07]: KB5014671\n" +
                "                           [08]: KB5005699\n" +
                "Network Card(s):           2 NIC(s) Installed.\n" +
                "                           [01]: Intel(R) I211 Gigabit Network Connection\n" +
                "                                 Connection Name: Ethernet0\n" +
                "                                 DHCP Enabled: No\n" +
                "                                 IP address(es)\n" +
                "                                 [01]: 192.168.99.164\n" +
                "                           [02]: Realtek 8822BE Wireless LAN 802.11ac PCI-E NIC\n" +
                "                                 Connection Name: Wi - Fi\n" +
                "                                 Status: Media disconnected\n" +
                "                           [03]: Cisco AnyConnect Secure Mobility Client Virtual Miniport Adapter for Windows x64\n" +
                "                                 Connection Name: Ethernet\n" +
                "                                 Status:          Hardware not present\n" +
                "Hyper-V Requirements:      VM Monitor Mode Extensions: Yes\n" +
                "                           Virtualization Enabled In Firmware: Yes\n" +
                "                           Second Level Address Translation: Yes\n" +
                "                           Data Execution Prevention Available: Yes\n");
        }
    }
}
