#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using System.Collections.Generic;

namespace KillUnwantedProcesses.KillableProcesses;

public class AdobeCreativeCloudLibraries: KillableProcess {

    public override string processName { get; } = "CCLibrary";

    public override string name { get; } = "Adobe Creative Cloud Libraries";

    protected override IEnumerable<string> saviorProcesses { get; } = [new AdobeDesktopService().processName];

}