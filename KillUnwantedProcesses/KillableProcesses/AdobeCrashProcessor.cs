#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using System.Collections.Generic;

namespace KillUnwantedProcesses.KillableProcesses;

public class AdobeCrashProcessor: KillableProcess {

    public override string processName { get; } = "Adobe Crash Processor";

    public override string name { get; } = "Adobe Crash Processor";

    protected override IEnumerable<string> saviorProcesses { get; } = [new AdobeDesktopService().processName];

}