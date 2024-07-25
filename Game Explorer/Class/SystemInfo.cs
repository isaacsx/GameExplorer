using System;
using System.Management;

namespace Game_Explorer.Class
{
    public static class SystemInfo
    {
        public static int GetCpuUsage()
        {
            using (var searcher =
                   new ManagementObjectSearcher(
                       "SELECT * FROM Win32_PerfFormattedData_PerfOS_Processor WHERE Name='_Total'"))
            {
                foreach (var o in searcher.Get())
                {
                    var obj = (ManagementObject)o;
                    if (obj["PercentProcessorTime"] != null)
                    {
                        return (int)float.Parse(obj["PercentProcessorTime"].ToString());
                    }
                }
            }

            return 0;
        }

        public static string GetCpuModel()
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            foreach (var o in searcher.Get())
            {
                var obj = (ManagementObject)o;
                return obj["Name"].ToString();
            }

            return "N/A";
        }

        public static int GetTotalRam()
        {
            long totalRamBytes = 0;
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
            foreach (var o in searcher.Get())
            {
                var obj = (ManagementObject)o;
                totalRamBytes = Convert.ToInt64(obj["TotalPhysicalMemory"]);
            }

            return (int)BytesToGigabytes(totalRamBytes);
        }

        public static int GetUsedRam()
        {
            long totalRamBytes = 0;
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
            foreach (var o in searcher.Get())
            {
                var obj = (ManagementObject)o;
                totalRamBytes = Convert.ToInt64(obj["TotalPhysicalMemory"]);
            }

            long freeRamBytes = 0;
            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach (var o in searcher.Get())
            {
                var obj = (ManagementObject)o;
                freeRamBytes = Convert.ToInt64(obj["FreePhysicalMemory"]) * 1024;
            }

            var usedRamBytes = totalRamBytes - freeRamBytes;

            return (int)BytesToGigabytes(usedRamBytes);
        }

        private static double BytesToGigabytes(long bytes)
        {
            return Math.Round((double)bytes / (1024 * 1024 * 1024), 2);
        }

        public static int GetUsedRamAsPercentage()
        {
            long totalRam = 0;
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem");
            foreach (var o in searcher.Get())
            {
                var obj = (ManagementObject)o;
                totalRam = Convert.ToInt64(obj["TotalPhysicalMemory"]);
            }

            long freeRam = 0;
            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach (var o in searcher.Get())
            {
                var obj = (ManagementObject)o;
                freeRam = Convert.ToInt64(obj["FreePhysicalMemory"]) * 1024;
            }

            var usedRam = totalRam - freeRam;
            var usedRamPercentage = (float)usedRam / totalRam * 100;

            return (int)usedRamPercentage;
        }
    }
}