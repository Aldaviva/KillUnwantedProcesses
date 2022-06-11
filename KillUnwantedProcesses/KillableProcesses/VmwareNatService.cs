using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses; 

public class VmwareNatService: KillableService {

    protected override string serviceName { get; } = "VMware NAT Service";

    public override string name { get; } = "VMware NAT Service";

}