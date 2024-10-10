#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using System;
using System.Collections.Generic;

namespace KillUnwantedProcesses.KillableProcesses;

public class VmAuthdService: KillableService {

    protected override string serviceName { get; } = "VMAuthdService";

    public override string name { get; } = "VMware Authorization Service";

    protected override IEnumerable<string> saviorProcesses { get; } = ["vmware-vmx"];

    /// <summary>
    /// On Windows 7, VMware Workstation can start a guest when this service is disabled. On Windows 10, it cannot, and fails with an error message telling the user to enable this service.
    /// </summary>
    /// <returns></returns>
    public override bool shouldKill() => Environment.OSVersion.Version.Major <= 7 && base.shouldKill();

}