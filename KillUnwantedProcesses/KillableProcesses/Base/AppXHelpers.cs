﻿#nullable enable

using System.Collections.ObjectModel;
using System.Management.Automation;

namespace KillUnwantedProcesses.KillableProcesses.Base;

public static class AppXHelpers {

    public static bool isAppxPackageInstalled(string packageName) {
        PowerShell pipeline = PowerShell.Create();
        pipeline.AddCommand("Get-AppxPackage")
            .AddParameter("Name", packageName);
        Collection<PSObject> results = pipeline.Invoke();
        return results.Count != 0;
    }

    public static void uninstallAppxPackage(string packageName) {
        PowerShell pipeline = PowerShell.Create();
        pipeline.AddCommand("Get-AppxPackage")
            .AddParameter("Name", packageName);
        pipeline.AddCommand("Remove-AppxPackage");
        pipeline.Invoke();
    }

}