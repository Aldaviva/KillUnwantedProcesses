#nullable enable

using System;
using System.Management;
using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses.Base {

    internal static class ServiceHelpers {

        internal static void stopService(string serviceName, ServiceStartMode? postKillServiceStartMode = null) {
            try {
                using var serviceController = new ServiceController(serviceName);
                Console.WriteLine($"Stopping service {serviceName}");
                serviceController.Stop();

                if (postKillServiceStartMode.HasValue) {
                    Console.WriteLine($"Setting start mode of service {serviceName} to {postKillServiceStartMode}");
                    setServiceStartMode(serviceName, postKillServiceStartMode.Value);
                }
            } catch (Exception e) when (e is InvalidOperationException || e is ArgumentException) {
                // Service does not exist, therefore it is not running and does not need to be stopped.
            }
        }

        internal static bool isServiceRunning(string serviceName) {
            try {
                using var serviceController = new ServiceController(serviceName);
                return serviceController.Status == ServiceControllerStatus.Running;
            } catch (Exception e) when (e is InvalidOperationException || e is ArgumentException) {
                // Service does not exist, therefore it is not running.
                return false;
            }
        }

        internal static ServiceStartMode getServiceStartMode(string serviceName) {
            using var serviceController = new ServiceController(serviceName);
            return serviceController.StartType;
        }

        private static void setServiceStartMode(string serviceName, ServiceStartMode startMode) {
            using ManagementObject wmiService = getWmiService(serviceName);
            ManagementBaseObject reqParams = wmiService.GetMethodParameters("ChangeStartMode");
            reqParams["StartMode"] = startMode.ToString();
            wmiService.InvokeMethod("ChangeStartMode", reqParams, null);
        }

        private static ManagementObject getWmiService(string serviceName) {
            using var searcher = new ManagementObjectSearcher(new SelectQuery("Win32_Service", $"Name = '{serviceName}'"));
            using ManagementObjectCollection results = searcher.Get();
            using ManagementObjectCollection.ManagementObjectEnumerator resultsEnumerator = results.GetEnumerator();
            resultsEnumerator.MoveNext();
            return (ManagementObject) resultsEnumerator.Current;
        }

    }

}