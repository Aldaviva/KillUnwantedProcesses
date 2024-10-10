#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using System.Collections.Generic;

namespace KillUnwantedProcesses.KillableProcesses;

public class AdobeDesktopService: KillableProcess {

    public override string processName { get; } = "Adobe Desktop Service";

    public override string name { get; } = "Adobe Desktop Service";

    protected override IEnumerable<string> saviorProcesses { get; } = ["Creative Cloud"];

}