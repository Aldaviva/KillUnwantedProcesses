﻿#nullable enable

using System.Collections.ObjectModel;
using System.Management.Automation;

namespace KillUnwantedProcesses.TaskHelpers {

    public static class UwpHelpers {

        public static bool isUwpAppxPackageInstalled(string packageName) {
            var pipeline = PowerShell.Create();
            pipeline.AddCommand("Get-AppxPackage")
                .AddParameter("Name", packageName);
            Collection<PSObject> results = pipeline.Invoke();
            return results.Count != 0;
        }

        public static void uninstallUwpAppxPackage(string packageName) {
            var pipeline = PowerShell.Create();
            pipeline.AddCommand("Get-AppxPackage")
                .AddParameter("Name", packageName);
            pipeline.AddCommand("Remove-AppxPackage");
            pipeline.Invoke();
        }

    }

}