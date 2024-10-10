#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using System.Collections.Generic;

namespace KillUnwantedProcesses.KillableProcesses;

public class AdobeCreativeCloudExperience: KillableProcess {

    public override string processName { get; } = "CCXProcess";

    public override string name { get; } = "Adobe Creative Cloud Experience";

    protected override IEnumerable<string> saviorProcesses { get; } = [new AdobeDesktopService().processName];

}