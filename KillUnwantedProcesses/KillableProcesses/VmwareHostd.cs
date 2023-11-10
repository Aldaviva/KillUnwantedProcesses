#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses;

public class VmwareHostd: KillableService {

    protected override string serviceName { get; } = "VMwareHostd";

    public override string name { get; } = "VMware Workstation Server";

}