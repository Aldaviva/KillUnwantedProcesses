#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using System.Collections.Generic;
using System.ServiceProcess;

namespace KillUnwantedProcesses.KillableProcesses;

public class LogitechGHub: KillableService {

    protected override string serviceName { get; } = "LGHUBUpdaterService";

    public override string name { get; } = "Logitech G Hub";

    protected override ServiceStartMode? desiredServiceStartMode { get; } = ServiceStartMode.Disabled;

    protected override IEnumerable<string> saviorProcesses { get; } = ["lghub"];

}