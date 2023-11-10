#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using System;

namespace KillUnwantedProcesses.KillableProcesses;

public class VmAuthdService: KillableService {

    protected override string serviceName { get; } = "VMAuthdService";

    public override string name { get; } = "VMware Authorization Service";

    /// <summary>
    /// On Windows 7, a VMware Workstation can start a guest when this service is disabled. On Windows 10, it cannot, and fails with an error message telling the user to enable this service.
    /// </summary>
    /// <returns></returns>
    public override bool shouldKill() {
        return base.shouldKill() && Environment.OSVersion.Version.Major <= 7 && !isProcessRunning("vmware-vmx");
    }

}