#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses;

public class FlexNetLicensingService: KillableService {

    public override string name { get; } = "FlexNet Licensing Service";

    protected override string serviceName { get; } = "FlexNet Licensing Service";

    protected override ServiceStartMode? desiredServiceStartMode { get; } = null;

}