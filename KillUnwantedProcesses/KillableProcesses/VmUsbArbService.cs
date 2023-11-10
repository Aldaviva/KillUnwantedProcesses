#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;

namespace KillUnwantedProcesses.KillableProcesses;

public class VmUsbArbService: KillableService {

    protected override string serviceName { get; } = "VMUSBArbService";

    public override string name { get; } = "VMware USB Arbitration Service";

}