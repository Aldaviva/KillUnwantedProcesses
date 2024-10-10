#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using System.Collections.Generic;

namespace KillUnwantedProcesses.KillableProcesses;

public class AdobeCoreSync: KillableProcess {

    public override string processName { get; } = "CoreSync";

    public override string name { get; } = "Adobe Sync";

    protected override IEnumerable<string> saviorProcesses { get; } = [new AdobeDesktopService().processName];

}