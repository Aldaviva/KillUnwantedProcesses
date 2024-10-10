#nullable enable

using KillUnwantedProcesses.KillableProcesses.Base;
using System.Collections.Generic;

namespace KillUnwantedProcesses.KillableProcesses;

public class AdobeCollabSync: KillableProcess {

    public override string processName { get; } = "AdobeCollabSync";

    public override string name { get; } = "Adobe Collaboration Synchronizer";

    protected override IEnumerable<string> saviorProcesses { get; } = ["Acrobat"];

}