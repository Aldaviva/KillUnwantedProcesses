using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text.RegularExpressions;

namespace KillUnwantedProcesses {

    public abstract class BaseKillableProcess: KillableProcess {

        protected static void KillProcess(string processName, bool alsoKillChildren = false) {
            processName = StripExeSuffix(processName);
            Process[] processesToKill = Process.GetProcessesByName(processName);
            foreach (Process processToKill in processesToKill) {
                using (processToKill) {
                    if (alsoKillChildren) {
                        foreach (Process descendentToKill in ParentProcessUtilities.GetDescendentProcesses(processToKill)) {
                            try {
                                KillProcess(descendentToKill);
                            } catch (Exception) {
                                //probably already closed or can't be killed
                            }
                        }
                    }

                    try {
                        KillProcess(processToKill);
                    } catch (Exception) {
                        //probably already closed or can't be killed
                    }
                }
            }
        }

        private static void KillProcess(Process process) {
            Console.WriteLine($"Killing {process.ProcessName} ({process.Id})");
            process.Kill();
        }

        protected static void StopService(string serviceName, ServiceStartMode? postKillServiceStartMode = null) {
            try {
                using (var serviceController = new ServiceController(serviceName)) {
                    Console.WriteLine($"Stopping service {serviceName}");
                    serviceController.Stop();

                    if (postKillServiceStartMode is ServiceStartMode mode) {
                        SetServiceStartMode(serviceName, mode);
                    }
                }
            } catch (ArgumentException) {
                // Service does not exist, therefore it is not running and does not need to be stopped.
            }
        }

        protected static bool IsProcessRunning(string processName) {
            processName = StripExeSuffix(processName);
            using (Process process = Process.GetProcessesByName(processName).FirstOrDefault()) {
                return process != null;
            }
        }

        protected static bool IsServiceRunning(string serviceName) {
            try {
                using (var serviceController = new ServiceController(serviceName)) {
                    return serviceController.Status == ServiceControllerStatus.Running;
                }
            } catch (ArgumentException) {
                // Service does not exist, therefore it is not running.
                return false;
            }
        }

        private static string StripExeSuffix(string processName) {
            return Regex.Replace(processName, @"\.exe$", "");
        }

        private static void SetServiceStartMode(string serviceName, ServiceStartMode startMode) {
            using (ManagementObject wmiService = GetWmiService(serviceName)) {
                ManagementBaseObject reqParams = wmiService.GetMethodParameters("ChangeStartMode");
                reqParams["StartMode"] = startMode.ToString();
                wmiService.InvokeMethod("ChangeStartMode", reqParams, null);
            }
        }

        private static ManagementObject GetWmiService(string serviceName) {
            using (var searcher = new ManagementObjectSearcher(new SelectQuery("Win32_Service", $"Name = '{serviceName}'")))
            using (ManagementObjectCollection results = searcher.Get())
            using (ManagementObjectCollection.ManagementObjectEnumerator resultsEnumerator = results.GetEnumerator()) {
                resultsEnumerator.MoveNext();
                return (ManagementObject) resultsEnumerator.Current;
            }
        }

    }

}