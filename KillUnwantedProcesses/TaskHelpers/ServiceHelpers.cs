using System;
using System.Management;
using System.ServiceProcess;

namespace KillUnwantedProcesses.TaskHelpers {

    internal static class ServiceHelpers {

        internal static void StopService(string serviceName, ServiceStartMode? postKillServiceStartMode = null) {
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

        internal static bool IsServiceRunning(string serviceName) {
            try {
                using (var serviceController = new ServiceController(serviceName)) {
                    return serviceController.Status == ServiceControllerStatus.Running;
                }
            } catch (ArgumentException) {
                // Service does not exist, therefore it is not running.
                return false;
            }
        }

        internal static void SetServiceStartMode(string serviceName, ServiceStartMode startMode) {
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