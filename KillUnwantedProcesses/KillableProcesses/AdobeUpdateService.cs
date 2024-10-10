#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using System.Collections.Generic;

namespace KillUnwantedProcesses.KillableProcesses;

public class AdobeUpdateService: KillableService {

    protected override string serviceName { get; } = "AdobeUpdateService";

    public override string name { get; } = "Adobe Update Service";

    protected override IEnumerable<string> saviorProcesses { get; } = [new AdobeDesktopService().processName];

}