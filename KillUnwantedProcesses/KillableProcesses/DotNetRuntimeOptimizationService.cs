#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses;

public class DotNetRuntimeOptimizationService: KillableBase {

    private const string SERVICE_NAME_PATTERN = "clr_optimization_%";

    public override string name { get; } = ".NET Runtime Optimization Service";

    public override bool shouldKill() => findServiceNames(SERVICE_NAME_PATTERN).Any(isServiceRunning);

    public override void kill() {
        IEnumerable<string> runningServiceNames = findServiceNames(SERVICE_NAME_PATTERN).Where(isServiceRunning);
        foreach (string serviceName in runningServiceNames) {
            stopService(serviceName, ServiceStartMode.Disabled);
        }
    }

    private static IEnumerable<string> findServiceNames(string nameQuery) {
        using ManagementObjectSearcher searcher = new(new SelectQuery("Win32_Service", $"Name LIKE '{nameQuery}'"));

        using ManagementObjectCollection results = searcher.Get();

        ISet<string> serviceNameResults = new HashSet<string>(results.Count);

        using ManagementObjectCollection.ManagementObjectEnumerator resultsEnumerator = results.GetEnumerator();

        while (resultsEnumerator.MoveNext()) {
            serviceNameResults.Add((string) resultsEnumerator.Current.GetPropertyValue("Name"));
        }

        return serviceNameResults;
    }

}